using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using UCS.Core;
using UCS.Helpers;
using UCS.PacketProcessing;

namespace UCS.Logic
{
	internal class ClientAvatar : Avatar
	{
		public class CrownChests
		{
			public byte ressources
			{
				get
				{
					return 5;
				}
			}

			public byte stars
			{
				get
				{
					return 4;
				}
			}

			public byte crown
			{
				get
				{
					return 16;
				}
			}
		}

		private long m_vAllianceId;

		private int m_vAvatarLevel;

		private string m_vAvatarName;

		private int m_vCurrentGems;

		private long m_vCurrentHomeId;

		private int m_vExperience;

		private long m_vId;

		private int m_vArenaId;

		private byte m_vNameChangingLeft;

		private byte m_vnameChosenByUser;

		private int m_vScore;

		private string m_vToken;

		public List<DataSlot> Achievements
		{
			get;
			set;
		}

		public List<DataSlot> AchievementsUnlocked
		{
			get;
			set;
		}

		public List<DataSlot> AllianceUnits
		{
			get;
			set;
		}

		public List<ClientAvatar.CrownChests> Chests
		{
			get;
			set;
		}

		public int EndShieldTime
		{
			get;
			set;
		}

		public int LastUpdate
		{
			get;
			set;
		}

		public string Login
		{
			get;
			set;
		}

		public List<DataSlot> NpcLootedElixir
		{
			get;
			set;
		}

		public List<DataSlot> NpcLootedGold
		{
			get;
			set;
		}

		public List<DataSlot> NpcStars
		{
			get;
			set;
		}

		public int RemainingShieldTime
		{
			get
			{
				int num = this.EndShieldTime - (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
				if (num <= 0)
				{
					return 0;
				}
				return num;
			}
		}

		public uint TutorialStepsCount
		{
			get;
			set;
		}

		public uint Region
		{
			get;
			set;
		}

		public ClientAvatar()
		{
			this.Achievements = new List<DataSlot>();
			this.AchievementsUnlocked = new List<DataSlot>();
			this.AllianceUnits = new List<DataSlot>();
			this.NpcStars = new List<DataSlot>();
			this.NpcLootedGold = new List<DataSlot>();
			this.NpcLootedElixir = new List<DataSlot>();
			this.Chests = new List<ClientAvatar.CrownChests>();
		}

        public ClientAvatar(long id) : this()
        {
            Random random = new Random();
            this.LastUpdate = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            this.Login = id.ToString() + (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            this.m_vId = id;
            this.m_vToken = null;
            this.m_vCurrentHomeId = id;
            this.m_vnameChosenByUser = 0;
            this.m_vNameChangingLeft = 2;
            this.m_vAvatarLevel = 1;
            this.m_vAllianceId = 0L;
            this.m_vExperience = 0;
            this.EndShieldTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + (double)Convert.ToInt32(ConfigurationManager.AppSettings["startingShieldTime"]));
            this.m_vCurrentGems = Convert.ToInt32(ConfigurationManager.AppSettings["startingGems"]);
            if (ConfigurationManager.AppSettings["startingTrophies"] == "random")
			{
				this.m_vScore = random.Next(1500, 4800);
			}
			else
			{
				this.m_vScore = Convert.ToInt32(ConfigurationManager.AppSettings["startingTrophies"]);
			}
			this.TutorialStepsCount = 10u;
			this.m_vAvatarName = "";
		}

		public byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt64(this.GetId());
			list.AddRange(new byte[]
			{
				1,
				1,
				132,
				165,
				29,
				145,
				159,
				42,
				155,
				158,
				235,
				241,
				10,
				0,
				3,
				8,
				128,
				234,
				229,
				24,
				129,
				234,
				229,
				24,
				141,
				234,
				229,
				24,
				129,
				252,
				217,
				26,
				128,
				252,
				217,
				26,
				131,
				234,
				229,
				24,
				142,
				234,
				229,
				24,
				140,
				234,
				229,
				24,
				8,
				128,
				234,
				229,
				24,
				129,
				234,
				229,
				24,
				141,
				234,
				229,
				24,
				129,
				252,
				217,
				26,
				128,
				252,
				217,
				26,
				131,
				234,
				229,
				24,
				0,
				0,
				8,
				128,
				234,
				229,
				24,
				129,
				234,
				229,
				24,
				141,
				234,
				229,
				24,
				129,
				252,
				217,
				26,
				128,
				252,
				217,
				26,
				131,
				234,
				229,
				24,
				0,
				0,
				255
			});
			list.AddRange(new byte[]
			{
				26,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				26,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				26,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				26,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				26,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				26,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				26,
				0,
				0,
				134,
				173,
				155,
				0,
				0,
				0,
				0,
				0,
				26,
				0,
				0,
				183,
				173,
				155,
				0,
				1,
				0,
				0,
				0,
				1,
				26,
				0,
				12,
				156,
				174,
				155,
				23,
				1
			});
			list.AddRange(new byte[]
			{
				0,
				0,
				2,
				0,
				0,
				4,
				7,
				19,
				6,
				0,
				1,
				6,
				0,
				0,
				152,
				137,
				35,
				128,
				148,
				35,
				156,
				195,
				235,
				240,
				10,
				0,
				0,
				127,
				0,
				0,
				0,
				0,
				0,
				127,
				0,
				0,
				0,
				127,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				2,
				2,
				54,
				1,
				251,
				241,
				245
			});
			list.AddRange(new byte[]
			{
				193,
				3,
				3,
				3,
				148,
				186,
				34,
				148,
				186,
				34,
				249,
				191,
				236,
				240,
				10,
				3
			});
			list.AddRange(new byte[]
			{
				28,
				2,
				0,
				28,
				0,
				0,
				26,
				27,
				0
			});
			list.AddRange(new byte[]
			{
				0,
				0,
				127,
				0,
				0,
				127,
				1,
				0,
				0,
				2,
				163,
				23,
				12,
				1,
				170,
				2,
				0,
				0,
				0,
				0,
				0,
				38,
				128,
				215,
				176,
				1,
				38,
				128,
				215,
				176,
				1,
				38,
				128,
				215,
				177,
				1
			});
			list.AddString(this.GetAvatarName());
			list.Add(this.GetNameSet());
			list.AddRange(new byte[]
			{
				0,
				54,
				2
			});
			list.AddRange(Message.AddVInt(this.GetScore()));
			list.Add(1);
			list.Add(0);
			list.Add(0);
			list.Add(0);
			list.Add(0);
			list.AddRange(Message.AddVInt(this.GetAvatarLevel()));
			list.AddRange(Message.AddVInt(this.GetScore()));
			list.AddRange(Message.AddVInt(this.GetScore()));
			list.Add(4);
			list.Add(6);
			list.Add(5);
			list.Add(5);
			list.Add(1);
			list.AddRange(Message.AddVInt(500000));
			list.Add(5);
			list.Add(2);
			list.Add(7);
			list.Add(5);
			list.Add(3);
			list.Add(10);
			List<ClientAvatar.CrownChests> chests = this.GetChests();
			if (chests.Count > 0)
			{
				using (List<ClientAvatar.CrownChests>.Enumerator enumerator = chests.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ClientAvatar.CrownChests current = enumerator.Current;
						list.Add(current.ressources);
						list.Add(current.stars);
						list.Add(current.crown);
					}
					goto IL_207;
				}
			}
			list.Add(5);
			list.Add(4);
			list.Add(0);
			IL_207:
			list.Add(5);
			list.Add(5);
			list.AddRange(Message.AddVInt(500000));
			list.AddRange(new byte[]
			{
				0,
				7,
				60,
				7,
				9,
				60,
				8,
				9,
				60,
				9,
				9,
				60,
				4,
				1,
				60,
				5,
				1,
				60,
				6,
				1,
				60,
				10,
				1,
				1,
				60,
				10,
				1,
				1,
				5,
				8,
				9,
				7
			});
			list.AddRange(new byte[]
			{
				26,
				0,
				11,
				26,
				1,
				8,
				26,
				3,
				9,
				26,
				13,
				13,
				26,
				14,
				6,
				28,
				0,
				2,
				26,
				12,
				4
			});
			list.AddRange(Message.AddVInt(this.GetDiamonds()));
			list.Add(10);
			list.AddRange(Message.AddVInt(this.GetExperience()));
			list.Add((byte)this.GetAvatarLevel());
			list.Add(1);
			if (this.GetAllianceId() != 0L)
			{
				Alliance alliance = ObjectManager.GetAlliance(this.GetAllianceId());
				list.Add(1);
				list.AddRange(Message.AddVInt(1));
				list.AddRange(Message.AddVInt(1));
				list.AddString(alliance.GetAllianceName());
				list.Add(0);
				list.Add(1);
			}
			else
			{
				list.Add(0);
			}
			return list.ToArray();
		}

		public void AddDiamonds(int diamondCount)
		{
			this.m_vCurrentGems += diamondCount;
		}

		public List<ClientAvatar.CrownChests> GetChests()
		{
			return this.Chests;
		}

		public long GetAllianceId()
		{
			return this.m_vAllianceId;
		}

		public AllianceMemberEntry GetAllianceMemberEntry()
		{
			Alliance alliance = ObjectManager.GetAlliance(this.m_vAllianceId);
			if (alliance != null)
			{
				return alliance.GetAllianceMember(this.m_vId);
			}
			return null;
		}

		public int GetAllianceRole()
		{
			AllianceMemberEntry allianceMemberEntry = this.GetAllianceMemberEntry();
			if (allianceMemberEntry != null)
			{
				return allianceMemberEntry.GetRole();
			}
			return -1;
		}

		public int GetAvatarLevel()
		{
			return this.m_vAvatarLevel;
		}

		public string GetAvatarName()
		{
			return this.m_vAvatarName;
		}

		public int GetExperience()
		{
			return this.m_vExperience;
		}

		public long GetCurrentHomeId()
		{
			return this.m_vCurrentHomeId;
		}

		public int GetDiamonds()
		{
			return this.m_vCurrentGems;
		}

		public long GetId()
		{
			return this.m_vId;
		}

		public int GetArenaId()
		{
			return this.m_vArenaId;
		}

		public int GetScore()
		{
			return this.m_vScore;
		}

		public int GetSecondsFromLastUpdate()
		{
			return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - this.LastUpdate;
		}

		public string GetUserToken()
		{
			return this.m_vToken;
		}

		public bool HasEnoughDiamonds(int diamondCount)
		{
			return this.m_vCurrentGems >= diamondCount;
		}

		public void LoadFromJSON(string jsonString)
		{
			JObject jObject = JObject.Parse(jsonString);
			this.m_vId = jObject["avatar_id"].ToObject<long>();
			this.m_vToken = jObject["token"].ToObject<string>();
			this.m_vCurrentHomeId = jObject["current_home_id"].ToObject<long>();
			this.m_vAllianceId = jObject["alliance_id"].ToObject<long>();
			this.m_vAvatarName = jObject["avatar_name"].ToObject<string>();
			this.m_vAvatarLevel = jObject["avatar_level"].ToObject<int>();
			this.m_vExperience = jObject["experience"].ToObject<int>();
			this.m_vCurrentGems = jObject["current_gems"].ToObject<int>();
			this.SetScore(jObject["score"].ToObject<int>());
			this.m_vNameChangingLeft = jObject["nameChangesLeft"].ToObject<byte>();
			this.m_vnameChosenByUser = jObject["nameChosenByUser"].ToObject<byte>();
			using (IEnumerator<JToken> enumerator = ((JArray)jObject["resources"]).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JObject jsonObject = (JObject)enumerator.Current;
					DataSlot dataSlot = new DataSlot(null, 0);
					dataSlot.Load(jsonObject);
					base.GetResources().Add(dataSlot);
				}
			}
			using (IEnumerator<JToken> enumerator = ((JArray)jObject["decks"]).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JObject jsonObject2 = (JObject)enumerator.Current;
					DataSlot dataSlot2 = new DataSlot(null, 0);
					dataSlot2.Load(jsonObject2);
					this.m_vUnitCount.Add(dataSlot2);
				}
			}
			this.TutorialStepsCount = jObject["tutorial_step"].ToObject<uint>();
			using (IEnumerator<JToken> enumerator = ((JArray)jObject["achievements_progress"]).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JObject jsonObject3 = (JObject)enumerator.Current;
					DataSlot dataSlot3 = new DataSlot(null, 0);
					dataSlot3.Load(jsonObject3);
					this.Achievements.Add(dataSlot3);
				}
			}
		}

		public void SetAllianceRole(int a)
		{
			AllianceMemberEntry allianceMemberEntry = this.GetAllianceMemberEntry();
			if (allianceMemberEntry != null)
			{
				allianceMemberEntry.SetRole(a);
			}
		}

		public string SaveToJSON()
		{
			JObject jObject = new JObject();
			jObject.Add("avatar_id", this.m_vId);
			jObject.Add("token", this.m_vToken);
			jObject.Add("current_home_id", this.m_vCurrentHomeId);
			jObject.Add("alliance_id", this.m_vAllianceId);
			jObject.Add("avatar_name", this.m_vAvatarName);
			jObject.Add("avatar_level", this.m_vAvatarLevel);
			jObject.Add("experience", this.m_vExperience);
			jObject.Add("current_gems", this.m_vCurrentGems);
			jObject.Add("score", this.m_vScore);
			jObject.Add("nameChangesLeft", this.m_vNameChangingLeft);
			jObject.Add("nameChosenByUser", (ushort)this.m_vnameChosenByUser);
			JArray jArray = new JArray();
			foreach (DataSlot current in base.GetResources())
			{
				jArray.Add(current.Save(new JObject()));
			}
			jObject.Add("resources", jArray);
			JArray jArray2 = new JArray();
			foreach (DataSlot current2 in base.GetUnits())
			{
				jArray2.Add(current2.Save(new JObject()));
			}
			jObject.Add("decks", jArray2);
			jObject.Add("tutorial_step", this.TutorialStepsCount);
			JArray jArray3 = new JArray();
			foreach (DataSlot current3 in this.Achievements)
			{
				jArray3.Add(current3.Save(new JObject()));
			}
			jObject.Add("achievements_progress", jArray3);
			return JsonConvert.SerializeObject(jObject);
		}

		public void SetAllianceId(long id)
		{
			this.m_vAllianceId = id;
		}

		public void SetDiamonds(int count)
		{
			this.m_vCurrentGems = count;
		}

		public void SetArenaId(int id)
		{
			this.m_vArenaId = id;
		}

		public void SetScore(int newScore)
		{
			this.m_vScore = newScore;
		}

		public void SetName(string name)
		{
			this.m_vAvatarName = name;
			this.m_vnameChosenByUser = 1;
			this.m_vNameChangingLeft = 1;
			this.TutorialStepsCount = 13u;
		}

		public byte GetNameSet()
		{
			return this.m_vnameChosenByUser;
		}

		public void SetToken(string token)
		{
			this.m_vToken = token;
		}

		public void UseDiamonds(int diamondCount)
		{
			this.m_vCurrentGems -= diamondCount;
		}
	}
}
