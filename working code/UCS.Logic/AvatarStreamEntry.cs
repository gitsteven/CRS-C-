using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.Logic
{
	internal class AvatarStreamEntry
	{
		private DateTime m_vCreationTime;

		private int m_vId;

		private byte m_vIsNew;

		private long m_vSenderId;

		private int m_vSenderLeagueId;

		private int m_vSenderLevel;

		private string m_vSenderName;

		public AvatarStreamEntry()
		{
			this.m_vCreationTime = DateTime.UtcNow;
		}

		public virtual byte[] Encode()
		{
			List<byte> expr_05 = new List<byte>();
			expr_05.AddInt32(this.GetStreamEntryType());
			expr_05.AddInt32(0);
			expr_05.AddInt32(this.m_vId);
			expr_05.AddInt64(this.m_vSenderId);
			expr_05.AddString(this.m_vSenderName);
			expr_05.AddInt32(this.m_vSenderLevel);
			expr_05.AddInt32(this.m_vSenderLeagueId);
			return expr_05.ToArray();
		}

		public int GetAgeSeconds()
		{
			return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - (int)this.m_vCreationTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
		}

		public int GetId()
		{
			return this.m_vId;
		}

		public long GetSenderAvatarId()
		{
			return this.m_vSenderId;
		}

		public int GetSenderLevel()
		{
			return this.m_vSenderLevel;
		}

		public string GetSenderName()
		{
			return this.m_vSenderName;
		}

		public virtual int GetStreamEntryType()
		{
			return -1;
		}

		public byte IsNew()
		{
			return this.m_vIsNew;
		}

		public void SetAvatar(ClientAvatar avatar)
		{
			this.m_vSenderId = avatar.GetId();
			this.m_vSenderName = avatar.GetAvatarName();
			this.m_vSenderLevel = avatar.GetAvatarLevel();
		}

		public void SetId(int id)
		{
			this.m_vId = id;
		}

		public void SetIsNew(byte isNew)
		{
			this.m_vIsNew = isNew;
		}

		public void SetSenderAvatarId(long id)
		{
			this.m_vSenderId = id;
		}

		public void SetSenderLeagueId(int id)
		{
			this.m_vSenderLeagueId = id;
		}

		public void SetSenderLevel(int level)
		{
			this.m_vSenderLevel = level;
		}

		public void SetSenderName(string name)
		{
			this.m_vSenderName = name;
		}
	}
}
