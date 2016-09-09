using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using UCS.GameFiles;
using UCS.Logic;

namespace UCS.Core
{
	internal class ObjectManager : IDisposable
	{
		private static Dictionary<long, Alliance> m_vAlliances;

		private static long m_vAllianceSeed;

		private static long m_vAvatarSeed;

		private static DatabaseManager m_vDatabase;

		private static string m_vHomeDefault;

		private static Random m_vRandomSeed;

		public bool m_vTimerCanceled;

		public System.Threading.Timer TimerReference;

		private static readonly object m_vDatabaseLock = new object();

		public static DataTables DataTables
		{
			get;
			set;
		}

		public static FingerPrint FingerPrint
		{
			get;
			set;
		}

		public static Dictionary<int, string> NpcLevels
		{
			get;
			set;
		}

		public ObjectManager()
		{
			this.m_vTimerCanceled = false;
			ObjectManager.m_vDatabase = new DatabaseManager();
			ObjectManager.NpcLevels = new Dictionary<int, string>();
			ObjectManager.DataTables = new DataTables();
			ObjectManager.m_vAlliances = new Dictionary<long, Alliance>();
			ObjectManager.LoadFingerPrint();
			using (StreamReader streamReader = new StreamReader("Gamefiles/starting_home.json"))
			{
				ObjectManager.m_vHomeDefault = streamReader.ReadToEnd();
			}
			ObjectManager.m_vAvatarSeed = ObjectManager.m_vDatabase.GetMaxPlayerId() + 1L;
			ObjectManager.m_vAllianceSeed = ObjectManager.m_vDatabase.GetMaxAllianceId() + 1L;
			ObjectManager.LoadGameFiles();
			ObjectManager.GetAllAlliancesFromDB();
			System.Threading.Timer timerReference = new System.Threading.Timer(new TimerCallback(this.Save), null, 30000, 15000);
			this.TimerReference = timerReference;
			Console.WriteLine("[CRS]    Database Sync started successfully");
			ObjectManager.m_vRandomSeed = new Random();
		}

		private void Save(object state)
		{
			ObjectManager.m_vDatabase.Save(ResourcesManager.GetInMemoryLevels());
			ObjectManager.m_vDatabase.Save(ObjectManager.m_vAlliances.Values.ToList<Alliance>());
			if (this.m_vTimerCanceled)
			{
				this.TimerReference.Dispose();
			}
		}

		public static Alliance CreateAlliance(long seed)
		{
			object vDatabaseLock = ObjectManager.m_vDatabaseLock;
			Alliance alliance;
			lock (vDatabaseLock)
			{
				if (seed == 0L)
				{
					seed = ObjectManager.m_vAllianceSeed;
				}
				alliance = new Alliance(seed);
				ObjectManager.m_vAllianceSeed += 1L;
			}
			ObjectManager.m_vDatabase.CreateAlliance(alliance);
			ObjectManager.m_vAlliances.Add(alliance.GetAllianceId(), alliance);
			return alliance;
		}

		public static Level CreateAvatar(long seed)
		{
			object vDatabaseLock = ObjectManager.m_vDatabaseLock;
			Level level;
			lock (vDatabaseLock)
			{
				if (seed == 0L)
				{
					seed = ObjectManager.m_vAvatarSeed;
				}
				level = new Level(seed);
				ObjectManager.m_vAvatarSeed += 1L;
			}
			level.LoadFromJSON(ObjectManager.m_vHomeDefault);
			ObjectManager.m_vDatabase.CreateAccount(level);
			return level;
		}

		public void Dispose()
		{
			if (this.TimerReference != null)
			{
				this.TimerReference.Dispose();
				this.TimerReference = null;
			}
		}

		public static void GetAllAlliancesFromDB()
		{
			for (int i = 0; i < ObjectManager.m_vDatabase.GetAllAlliances().Count; i++)
			{
				if (!ObjectManager.m_vAlliances.ContainsKey(ObjectManager.m_vDatabase.GetAllAlliances()[i].GetAllianceId()))
				{
					ObjectManager.m_vAlliances.Add(ObjectManager.m_vDatabase.GetAllAlliances()[i].GetAllianceId(), ObjectManager.m_vDatabase.GetAllAlliances()[i]);
				}
			}
		}

		public static Alliance GetAlliance(long allianceId)
		{
			Alliance alliance;
			if (ObjectManager.m_vAlliances.ContainsKey(allianceId))
			{
				alliance = ObjectManager.m_vAlliances[allianceId];
			}
			else
			{
				alliance = ObjectManager.m_vDatabase.GetAlliance(allianceId);
				if (alliance != null)
				{
					ObjectManager.m_vAlliances.Add(alliance.GetAllianceId(), alliance);
				}
			}
			return alliance;
		}

		public static List<Alliance> GetInMemoryAlliances()
		{
			List<Alliance> expr_05 = new List<Alliance>();
			expr_05.AddRange(ObjectManager.m_vAlliances.Values);
			return expr_05;
		}

		public static Level GetRandomOnlinePlayer()
		{
			int index = ObjectManager.m_vRandomSeed.Next(0, ResourcesManager.GetInMemoryLevels().Count);
			return ResourcesManager.GetInMemoryLevels().ElementAt(index);
		}

		public static Level GetRandomPlayerFromAll()
		{
			int index = ObjectManager.m_vRandomSeed.Next(0, ResourcesManager.GetAllPlayerIds().Count);
			return ResourcesManager.GetPlayer(ResourcesManager.GetAllPlayerIds()[index], false);
		}

		public static void LoadFingerPrint()
		{
			if (Convert.ToBoolean(ConfigurationManager.AppSettings["useCustomPatch"]))
			{
				ObjectManager.FingerPrint = new FingerPrint("Gamefiles/fingerprint.json");
			}
		}

		public static void LoadGameFiles()
		{
			List<Tuple<string, string, int>> list = new List<Tuple<string, string, int>>();
			Console.WriteLine("[CRS]    Loading server gamefiles & data...");
			for (int i = 0; i < list.Count; i++)
			{
				Console.Write("             ->  " + list[i].Item1);
				ObjectManager.DataTables.InitDataTable(new CSVTable(list[i].Item2), list[i].Item3);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(" done");
				Console.ResetColor();
			}
		}

		public static void RemoveInMemoryAlliance(long id)
		{
			ObjectManager.m_vAlliances.Remove(id);
		}
	}
}
