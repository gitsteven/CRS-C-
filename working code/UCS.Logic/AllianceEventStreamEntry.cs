using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.Logic
{
	internal class AllianceEventStreamEntry : StreamEntry
	{
		private long m_vAvatarId;

		private string m_vAvatarName;

		private int m_vEventType;

		public override byte[] Encode()
		{
			List<byte> expr_05 = new List<byte>();
			expr_05.AddRange(base.Encode());
			expr_05.AddInt32(this.m_vEventType);
			expr_05.AddInt64(this.m_vAvatarId);
			expr_05.AddString(this.m_vAvatarName);
			return expr_05.ToArray();
		}

		public override int GetStreamEntryType()
		{
			return 4;
		}

		public override void Load(JObject jsonObject)
		{
			base.Load(jsonObject);
			jsonObject["avatar_name"].ToObject<string>();
			jsonObject["event_type"].ToObject<int>();
			jsonObject["avatar_id"].ToObject<long>();
		}

		public override JObject Save(JObject jsonObject)
		{
			jsonObject = base.Save(jsonObject);
			jsonObject.Add("avatar_name", this.m_vAvatarName);
			jsonObject.Add("event_type", this.m_vEventType);
			jsonObject.Add("avatar_id", this.m_vAvatarId);
			return jsonObject;
		}

		public void SetAvatarId(long id)
		{
			this.m_vAvatarId = id;
		}

		public void SetAvatarName(string name)
		{
			this.m_vAvatarName = name;
		}

		public void SetEventType(int type)
		{
			this.m_vEventType = type;
		}
	}
}
