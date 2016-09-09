using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UCS.Helpers;

namespace UCS.Logic
{
	internal class Alliance
	{
		private const int m_vMaxAllianceMembers = 50;

		private const int m_vMaxChatMessagesNumber = 30;

		private readonly Dictionary<long, AllianceMemberEntry> m_vAllianceMembers;

		private readonly List<StreamEntry> m_vChatMessages;

		private int m_vAllianceBadgeData;

		private string m_vAllianceDescription;

		private int m_vAllianceExperience;

		private long m_vAllianceId;

		private int m_vAllianceLevel;

		private string m_vAllianceName;

		private int m_vAllianceOrigin;

		private int m_vAllianceType;

		private int m_vDrawWars;

		private int m_vLostWars;

		private int m_vRequiredScore;

		private int m_vScore;

		private int m_vWarFrequency;

		private int m_vWonWars;

		public Alliance()
		{
			this.m_vChatMessages = new List<StreamEntry>();
			this.m_vAllianceMembers = new Dictionary<long, AllianceMemberEntry>();
		}

		public Alliance(long id)
		{
			Random random = new Random();
			this.m_vAllianceId = id;
			this.m_vAllianceName = "Default";
			this.m_vAllianceDescription = "Default";
			this.m_vAllianceBadgeData = 0;
			this.m_vAllianceType = 0;
			this.m_vRequiredScore = 0;
			this.m_vWarFrequency = 0;
			this.m_vAllianceOrigin = 32000006;
			this.m_vScore = 0;
			this.m_vAllianceExperience = random.Next(1, 5000);
			this.m_vAllianceLevel = random.Next(1, 10);
			this.m_vWonWars = random.Next(0, 300);
			this.m_vLostWars = random.Next(0, 300);
			this.m_vDrawWars = random.Next(0, 300);
			this.m_vChatMessages = new List<StreamEntry>();
			this.m_vAllianceMembers = new Dictionary<long, AllianceMemberEntry>();
		}

		public void AddAllianceMember(AllianceMemberEntry entry)
		{
			this.m_vAllianceMembers.Add(entry.GetAvatarId(), entry);
		}

		public void AddChatMessage(StreamEntry message)
		{
			while (this.m_vChatMessages.Count >= 30)
			{
				this.m_vChatMessages.RemoveAt(0);
			}
			this.m_vChatMessages.Add(message);
		}

		public byte[] EncodeFullEntry()
		{
			List<byte> expr_05 = new List<byte>();
			expr_05.AddInt64(this.m_vAllianceId);
			expr_05.AddString(this.m_vAllianceName);
			expr_05.AddInt32(this.m_vAllianceBadgeData);
			expr_05.AddInt32(this.m_vAllianceType);
			expr_05.AddInt32(this.m_vAllianceMembers.Count);
			expr_05.AddInt32(this.m_vScore);
			expr_05.AddInt32(this.m_vRequiredScore);
			expr_05.AddInt32(this.m_vWonWars);
			expr_05.AddInt32(this.m_vLostWars);
			expr_05.AddInt32(this.m_vDrawWars);
			expr_05.AddInt32(2000001);
			expr_05.AddInt32(this.m_vWarFrequency);
			expr_05.AddInt32(this.m_vAllianceOrigin);
			expr_05.AddInt32(this.m_vAllianceExperience);
			expr_05.AddInt32(this.m_vAllianceLevel);
			expr_05.AddInt32(0);
			return expr_05.ToArray();
		}

		public byte[] EncodeHeader()
		{
			List<byte> expr_05 = new List<byte>();
			expr_05.AddInt64(this.m_vAllianceId);
			expr_05.AddString(this.m_vAllianceName);
			expr_05.AddInt32(this.m_vAllianceBadgeData);
			expr_05.Add(0);
			expr_05.AddInt32(this.m_vAllianceLevel);
			expr_05.AddInt32(1);
			expr_05.AddInt32(-1);
			return expr_05.ToArray();
		}

		public static byte[] EncodeMembers()
		{
			return new List<byte>().ToArray();
		}

		public int GetAllianceBadgeData()
		{
			return this.m_vAllianceBadgeData;
		}

		public string GetAllianceDescription()
		{
			return this.m_vAllianceDescription;
		}

		public int GetAllianceExperience()
		{
			return this.m_vAllianceExperience;
		}

		public long GetAllianceId()
		{
			return this.m_vAllianceId;
		}

		public int GetAllianceLevel()
		{
			return this.m_vAllianceLevel;
		}

		public AllianceMemberEntry GetAllianceMember(long avatarId)
		{
			return this.m_vAllianceMembers[avatarId];
		}

		public List<AllianceMemberEntry> GetAllianceMembers()
		{
			return this.m_vAllianceMembers.Values.ToList<AllianceMemberEntry>();
		}

		public string GetAllianceName()
		{
			return this.m_vAllianceName;
		}

		public int GetAllianceOrigin()
		{
			return this.m_vAllianceOrigin;
		}

		public int GetAllianceType()
		{
			return this.m_vAllianceType;
		}

		public List<StreamEntry> GetChatMessages()
		{
			return this.m_vChatMessages;
		}

		public int GetRequiredScore()
		{
			return this.m_vRequiredScore;
		}

		public int GetScore()
		{
			return this.m_vScore;
		}

		public int GetWarFrequency()
		{
			return this.m_vWarFrequency;
		}

		public int GetWarScore()
		{
			return this.m_vWonWars;
		}

		public bool IsAllianceFull()
		{
			return this.m_vAllianceMembers.Count >= 50;
		}

		public void LoadFromJSON(string jsonString)
		{
			JObject jObject = JObject.Parse(jsonString);
			this.m_vAllianceId = jObject["alliance_id"].ToObject<long>();
			this.m_vAllianceName = jObject["alliance_name"].ToObject<string>();
			this.m_vAllianceBadgeData = jObject["alliance_badge"].ToObject<int>();
			this.m_vAllianceType = jObject["alliance_type"].ToObject<int>();
			if (jObject["required_score"] != null)
			{
				this.m_vRequiredScore = jObject["required_score"].ToObject<int>();
			}
			this.m_vAllianceDescription = jObject["description"].ToObject<string>();
			this.m_vAllianceExperience = jObject["alliance_experience"].ToObject<int>();
			this.m_vAllianceLevel = jObject["alliance_level"].ToObject<int>();
			if (jObject["won_wars"] != null)
			{
				this.m_vWonWars = jObject["won_wars"].ToObject<int>();
			}
			if (jObject["lost_wars"] != null)
			{
				this.m_vLostWars = jObject["lost_wars"].ToObject<int>();
			}
			if (jObject["draw_wars"] != null)
			{
				this.m_vDrawWars = jObject["draw_wars"].ToObject<int>();
			}
			if (jObject["war_frequency"] != null)
			{
				this.m_vWarFrequency = jObject["war_frequency"].ToObject<int>();
			}
			if (jObject["alliance_origin"] != null)
			{
				this.m_vAllianceOrigin = jObject["alliance_origin"].ToObject<int>();
			}
			using (IEnumerator<JToken> enumerator = ((JArray)jObject["members"]).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JObject jObject2 = (JObject)enumerator.Current;
					long num = jObject2["avatar_id"].ToObject<long>();
					AllianceMemberEntry allianceMemberEntry = new AllianceMemberEntry(num);
					Level level = new Level(num);
					this.m_vScore += level.GetPlayerAvatar().GetScore();
					allianceMemberEntry.Load(jObject2);
					this.m_vAllianceMembers.Add(num, allianceMemberEntry);
				}
			}
			this.m_vScore /= 2;
			JArray jArray = (JArray)jObject["chatMessages"];
			if (jArray != null)
			{
				using (IEnumerator<JToken> enumerator = jArray.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JObject jObject3 = (JObject)enumerator.Current;
						StreamEntry streamEntry = new StreamEntry();
						switch (jObject3["type"].ToObject<int>())
						{
						case 1:
							streamEntry = new TroopRequestStreamEntry();
							break;
						case 2:
							streamEntry = new ChatStreamEntry();
							break;
						case 3:
							streamEntry = new InvitationStreamEntry();
							break;
						case 4:
							streamEntry = new AllianceEventStreamEntry();
							break;
						case 5:
							streamEntry = new ShareStreamEntry();
							break;
						}
						streamEntry.Load(jObject3);
						this.m_vChatMessages.Add(streamEntry);
					}
				}
			}
		}

		public void RemoveMember(long avatarId)
		{
			this.m_vAllianceMembers.Remove(avatarId);
		}

		public string SaveToJSON()
		{
			JObject jObject = new JObject();
			jObject.Add("alliance_id", this.m_vAllianceId);
			jObject.Add("alliance_name", this.m_vAllianceName);
			jObject.Add("alliance_badge", this.m_vAllianceBadgeData);
			jObject.Add("alliance_type", this.m_vAllianceType);
			jObject.Add("score", this.m_vScore);
			jObject.Add("required_score", this.m_vRequiredScore);
			jObject.Add("description", this.m_vAllianceDescription);
			jObject.Add("alliance_experience", this.m_vAllianceExperience);
			jObject.Add("alliance_level", this.m_vAllianceLevel);
			jObject.Add("won_wars", this.m_vWonWars);
			jObject.Add("lost_wars", this.m_vLostWars);
			jObject.Add("draw_wars", this.m_vDrawWars);
			jObject.Add("war_frequency", this.m_vWarFrequency);
			jObject.Add("alliance_origin", this.m_vAllianceOrigin);
			JArray jArray = new JArray();
			foreach (AllianceMemberEntry arg_163_0 in this.m_vAllianceMembers.Values)
			{
				JObject jObject2 = new JObject();
				arg_163_0.Save(jObject2);
				jArray.Add(jObject2);
			}
			jObject.Add("members", jArray);
			JArray jArray2 = new JArray();
			foreach (StreamEntry arg_1BB_0 in this.m_vChatMessages)
			{
				JObject jObject3 = new JObject();
				arg_1BB_0.Save(jObject3);
				jArray2.Add(jObject3);
			}
			jObject.Add("chatMessages", jArray2);
			return JsonConvert.SerializeObject(jObject);
		}

		public void SetAllianceBadgeData(int data)
		{
			this.m_vAllianceBadgeData = data;
		}

		public void SetAllianceDescription(string description)
		{
			this.m_vAllianceDescription = description;
		}

		public void SetAllianceLevel(int level)
		{
			this.m_vAllianceLevel = level;
		}

		public void SetAllianceName(string name)
		{
			this.m_vAllianceName = name;
		}

		public void SetAllianceOrigin(int origin)
		{
			this.m_vAllianceOrigin = origin;
		}

		public void SetAllianceType(int status)
		{
			this.m_vAllianceType = status;
		}

		public void SetRequiredScore(int score)
		{
			this.m_vRequiredScore = score;
		}

		public void SetWarFrequency(int frequency)
		{
			this.m_vWarFrequency = frequency;
		}
	}
}
