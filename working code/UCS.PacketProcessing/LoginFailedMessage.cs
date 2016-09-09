using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
	internal class LoginFailedMessage : Message
	{
		private string m_vContentURL;

		private byte m_vErrorCode;

		private string m_vReason;

		private string m_vRedirectDomain;

		private int m_vRemainingTime;

		private string m_vResourceFingerprintData = "9bb57e3688e6df1e1e70ba4f927163bb8cbf7cef";

		private string m_vUpdateURL;

		public LoginFailedMessage(Client client) : base(client)
		{
			base.SetMessageType(20103);
			this.SetReason("UCS Developement Team");
		}

		public override void Encode()
		{
			List<byte> list = new List<byte>();
			if (base.Client.CState == 0)
			{
				list.Add(this.m_vErrorCode);
				list.AddString(this.m_vResourceFingerprintData);
				list.AddString(this.m_vRedirectDomain);
				list.AddString(this.m_vContentURL);
				list.AddString(this.m_vUpdateURL);
				list.AddString(this.m_vReason);
				list.AddInt32(this.m_vRemainingTime);
				list.AddInt32(-1);
				list.Add(0);
				list.AddString(string.Empty);
				list.AddInt32(-1);
				list.AddInt32(2);
				base.SetData(list.ToArray());
				return;
			}
			list.Add(this.m_vErrorCode);
			list.AddString(this.m_vResourceFingerprintData);
			list.AddString(this.m_vRedirectDomain);
			list.AddString(this.m_vContentURL);
			list.AddString(this.m_vUpdateURL);
			list.AddString(this.m_vReason);
			list.AddInt32(this.m_vRemainingTime);
			list.AddInt32(-1);
			list.Add(0);
			list.AddString(string.Empty);
			list.AddInt32(-1);
			list.AddInt32(2);
			base.Encrypt(list.ToArray());
		}

		public void RemainingTime(int code)
		{
			this.m_vRemainingTime = code;
		}

		public void SetContentURL(string url)
		{
			this.m_vContentURL = url;
		}

		public void SetErrorCode(byte code)
		{
			this.m_vErrorCode = code;
		}

		public void SetReason(string reason)
		{
			this.m_vReason = reason;
		}

		public void SetRedirectDomain(string domain)
		{
			this.m_vRedirectDomain = domain;
		}

		public void SetResourceFingerprintData(string data)
		{
			this.m_vResourceFingerprintData = data;
		}

		public void SetUpdateURL(string url)
		{
			this.m_vUpdateURL = url;
		}
	}
}
