using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.Logic
{
	internal class StreamEntry
	{
		private long m_vHomeId;

		private int m_vId;

		private DateTime m_vMessageTime;

		private long m_vSenderId;

		private int m_vSenderLeagueId;

		private int m_vSenderLevel;

		private string m_vSenderName;

		private int m_vSenderRole;

		private int m_vType = -1;

		public StreamEntry()
		{
			this.m_vMessageTime = DateTime.UtcNow;
		}

		public virtual byte[] Encode()
		{
			List<byte> expr_05 = new List<byte>();
			expr_05.AddInt32(this.GetStreamEntryType());
			expr_05.AddInt32(0);
			expr_05.AddInt32(this.m_vId);
			expr_05.Add(3);
			expr_05.AddInt64(this.m_vSenderId);
			expr_05.AddInt64(this.m_vHomeId);
			expr_05.AddString(this.m_vSenderName);
			expr_05.AddInt32(this.m_vSenderLevel);
			expr_05.AddInt32(this.m_vSenderLeagueId);
			expr_05.AddInt32(this.m_vSenderRole);
			expr_05.AddInt32(this.GetAgeSeconds());
			return expr_05.ToArray();
		}

		public int GetAgeSeconds()
		{
			return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - (int)this.m_vMessageTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
		}

		public long GetHomeId()
		{
			return this.m_vHomeId;
		}

		public int GetId()
		{
			return this.m_vId;
		}

		public long GetSenderId()
		{
			return this.m_vSenderId;
		}

		public int GetSenderLeagueId()
		{
			return this.m_vSenderLeagueId;
		}

		public int GetSenderLevel()
		{
			return this.m_vSenderLevel;
		}

		public string GetSenderName()
		{
			return this.m_vSenderName;
		}

		public int GetSenderRole()
		{
			return this.m_vSenderRole;
		}

		public virtual int GetStreamEntryType()
		{
			return this.m_vType;
		}

		public virtual void Load(JObject jsonObject)
		{
			this.m_vType = jsonObject["type"].ToObject<int>();
			this.m_vId = jsonObject["id"].ToObject<int>();
			this.m_vSenderId = jsonObject["sender_id"].ToObject<long>();
			this.m_vHomeId = jsonObject["home_id"].ToObject<long>();
			this.m_vSenderLevel = jsonObject["sender_level"].ToObject<int>();
			this.m_vSenderName = jsonObject["sender_name"].ToObject<string>();
			this.m_vSenderLeagueId = jsonObject["sender_leagueId"].ToObject<int>();
			this.m_vSenderRole = jsonObject["sender_role"].ToObject<int>();
			this.m_vMessageTime = jsonObject["message_time"].ToObject<DateTime>();
		}

		public virtual JObject Save(JObject jsonObject)
		{
			jsonObject.Add("type", this.GetStreamEntryType());
			jsonObject.Add("id", this.m_vId);
			jsonObject.Add("sender_id", this.m_vSenderId);
			jsonObject.Add("home_id", this.m_vHomeId);
			jsonObject.Add("sender_level", this.m_vSenderLevel);
			jsonObject.Add("sender_name", this.m_vSenderName);
			jsonObject.Add("sender_leagueId", this.m_vSenderLeagueId);
			jsonObject.Add("sender_role", this.m_vSenderRole);
			jsonObject.Add("message_time", this.m_vMessageTime);
			return jsonObject;
		}

		public void SetAvatar(ClientAvatar avatar)
		{
			this.m_vSenderId = avatar.GetId();
			this.m_vHomeId = avatar.GetId();
			this.m_vSenderName = avatar.GetAvatarName();
			this.m_vSenderLevel = avatar.GetAvatarLevel();
			this.m_vSenderRole = avatar.GetAllianceRole();
		}

		public void SetHomeId(long id)
		{
			this.m_vHomeId = id;
		}

		public void SetId(int id)
		{
			this.m_vId = id;
		}

		public void SetSenderId(long id)
		{
			this.m_vSenderId = id;
		}

		public void SetSenderLeagueId(int leagueId)
		{
			this.m_vSenderLeagueId = leagueId;
		}

		public void SetSenderLevel(int level)
		{
			this.m_vSenderLevel = level;
		}

		public void SetSenderName(string name)
		{
			this.m_vSenderName = name;
		}

		public void SetSenderRole(int role)
		{
			this.m_vSenderRole = role;
		}

		public void SetType(int type)
		{
			this.m_vType = type;
		}
	}
}
