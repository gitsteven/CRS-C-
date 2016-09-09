using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.Logic
{
	internal class AllianceMailStreamEntry : AvatarStreamEntry
	{
		private int m_vAllianceBadgeData;

		private long m_vAllianceId;

		private string m_vAllianceName;

		private string m_vMessage;

		private long m_vSenderId;

		public override byte[] Encode()
		{
			List<byte> expr_05 = new List<byte>();
			expr_05.AddRange(base.Encode());
			expr_05.AddInt32(2);
			expr_05.AddString(this.m_vMessage);
			expr_05.Add(1);
			expr_05.AddInt64(this.m_vSenderId);
			expr_05.AddInt64(this.m_vAllianceId);
			expr_05.AddString(this.m_vAllianceName);
			expr_05.AddInt32(this.m_vAllianceBadgeData);
			expr_05.AddInt32(-1);
			return expr_05.ToArray();
		}

		public string GetMessage()
		{
			return this.m_vMessage;
		}

		public override int GetStreamEntryType()
		{
			return 6;
		}

		public void SetAllianceBadgeData(int data)
		{
			this.m_vAllianceBadgeData = data;
		}

		public void SetAllianceId(long id)
		{
			this.m_vAllianceId = id;
		}

		public void SetAllianceName(string name)
		{
			this.m_vAllianceName = name;
		}

		public void SetMessage(string message)
		{
			this.m_vMessage = message;
		}

		public void SetSenderId(long id)
		{
			this.m_vSenderId = id;
		}
	}
}
