using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UCS.Helpers;
using UCS.Logic;
using UCS.PacketProcessing;

namespace UCS.Core
{
	internal class ResourcesManager : IDisposable
	{
		private static readonly object m_vOnlinePlayersLock = new object();

		private static ConcurrentDictionary<long, Client> m_vClients;

		private static DatabaseManager m_vDatabase;

		private static ConcurrentDictionary<long, Level> m_vInMemoryLevels;

		private static List<Level> m_vOnlinePlayers;

		private readonly bool m_vTimerCanceled;

		private readonly System.Threading.Timer TimerReference;

		public ResourcesManager()
		{
			ResourcesManager.m_vDatabase = new DatabaseManager();
			ResourcesManager.m_vClients = new ConcurrentDictionary<long, Client>();
			ResourcesManager.m_vOnlinePlayers = new List<Level>();
			ResourcesManager.m_vInMemoryLevels = new ConcurrentDictionary<long, Level>();
			this.m_vTimerCanceled = false;
			System.Threading.Timer timerReference = new System.Threading.Timer(new TimerCallback(this.ReleaseOrphans), null, 40000, 30000);
			this.TimerReference = timerReference;
		}

		public void Dispose()
		{
			if (this.m_vTimerCanceled)
			{
				this.TimerReference.Dispose();
			}
		}

		public static void AddClient(Client c, string IP)
		{
			long key = c.Socket.Handle.ToInt64();
			c.CIPAddress = IP;
			if (!ResourcesManager.m_vClients.ContainsKey(key))
			{
				ResourcesManager.m_vClients.TryAdd(key, c);
			}
		}

		public static void DropClient(long socketHandle)
		{
			try
			{
				Client client;
				ResourcesManager.m_vClients.TryRemove(socketHandle, out client);
				if (client.GetLevel() != null)
				{
					ResourcesManager.LogPlayerOut(client.GetLevel());
				}
			}
			catch (Exception ex)
			{
				Debugger.WriteLine("[CRS]    Error dropping client: ", ex, 4);
			}
		}

		public static Client GetClient(long socketHandle)
		{
			return ResourcesManager.m_vClients[socketHandle];
		}

		public static List<long> GetAllPlayerIds()
		{
			return ResourcesManager.m_vDatabase.GetAllPlayerIds();
		}

		public static List<Client> GetConnectedClients()
		{
			List<Client> expr_05 = new List<Client>();
			expr_05.AddRange(ResourcesManager.m_vClients.Values);
			return expr_05;
		}

		public static List<Level> GetInMemoryLevels()
		{
			List<Level> list = new List<Level>();
			object vOnlinePlayersLock = ResourcesManager.m_vOnlinePlayersLock;
			lock (vOnlinePlayersLock)
			{
				list.AddRange(ResourcesManager.m_vInMemoryLevels.Values);
			}
			return list;
		}

		public static List<Level> GetOnlinePlayers()
		{
			List<Level> result = new List<Level>();
			object vOnlinePlayersLock = ResourcesManager.m_vOnlinePlayersLock;
			lock (vOnlinePlayersLock)
			{
				result = ResourcesManager.m_vOnlinePlayers.ToList<Level>();
			}
			return result;
		}

		public static Level GetPlayer(long id, bool persistent = false)
		{
			Level level = ResourcesManager.GetInMemoryPlayer(id);
			if (level == null)
			{
				level = ResourcesManager.m_vDatabase.GetAccount(id);
				if (persistent)
				{
					ResourcesManager.LoadLevel(level);
				}
			}
			return level;
		}

		public static bool IsClientConnected(long socketHandle)
		{
			return ResourcesManager.m_vClients.ContainsKey(socketHandle);
		}

		public static bool IsPlayerOnline(Level l)
		{
			return ResourcesManager.m_vOnlinePlayers.Contains(l);
		}

		public static void LoadLevel(Level level)
		{
			long id = level.GetPlayerAvatar().GetId();
			if (!ResourcesManager.m_vInMemoryLevels.ContainsKey(id))
			{
				ResourcesManager.m_vInMemoryLevels.TryAdd(id, level);
			}
		}

		public static void LogPlayerIn(Level level, Client client)
		{
			level.SetClient(client);
			client.SetLevel(level);
			level.SetIPAddress(client.CIPAddress);
			object vOnlinePlayersLock = ResourcesManager.m_vOnlinePlayersLock;
			lock (vOnlinePlayersLock)
			{
				if (!ResourcesManager.m_vOnlinePlayers.Contains(level))
				{
					ResourcesManager.m_vOnlinePlayers.Add(level);
					ResourcesManager.LoadLevel(level);
				}
			}
		}

		public static void LogPlayerOut(Level level)
		{
			object vOnlinePlayersLock = ResourcesManager.m_vOnlinePlayersLock;
			lock (vOnlinePlayersLock)
			{
				ResourcesManager.m_vOnlinePlayers.Remove(level);
			}
			DatabaseManager.Singelton.Save(level);
			ResourcesManager.m_vInMemoryLevels.TryRemove(level.GetPlayerAvatar().GetId());
		}

		private static Level GetInMemoryPlayer(long id)
		{
			Level result = null;
			object vOnlinePlayersLock = ResourcesManager.m_vOnlinePlayersLock;
			lock (vOnlinePlayersLock)
			{
				if (ResourcesManager.m_vInMemoryLevels.ContainsKey(id))
				{
					result = ResourcesManager.m_vInMemoryLevels[id];
				}
			}
			return result;
		}

		private void ReleaseOrphans(object state)
		{
			if (this.m_vTimerCanceled)
			{
				this.TimerReference.Dispose();
			}
		}
	}
}
