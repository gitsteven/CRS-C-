using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
	internal class LoginOkMessage : Message
	{
		private readonly string m_vFacebookAppID = "297484437009394";

		private string m_vAccountCreatedDate;

		private long m_vAccountId;

		private int m_vContentVersion;

		private string m_vCountryCode;

		private int m_vDaysSinceStartedPlaying;

		private string m_vFacebookId;

		private string m_vGamecenterId;

		private int m_vGoogleID;

		private string m_vPassToken;

		private int m_vPlayTimeSeconds;

		private int m_vServerBuild;

		private string m_vServerEnvironment;

		private int m_vServerMajorVersion;

		private string m_vServerTime;

		private int m_vSessionCount;

		private int m_vStartupCooldownSeconds;

		public string Unknown11
		{
			get;
			set;
		}

		public string Unknown9
		{
			get;
			set;
		}

		public LoginOkMessage(Client client) : base(client)
		{
			base.SetMessageType(20104);
		}

		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt64(this.m_vAccountId);
			list.AddInt64(this.m_vAccountId);
			list.AddString(this.m_vPassToken);
			list.AddString(this.m_vFacebookId);
			list.AddString(this.m_vGamecenterId);
			list.AddInt32(this.m_vServerMajorVersion);
			list.AddInt32(this.m_vServerBuild);
			list.AddInt32(this.m_vContentVersion);
			list.AddString(this.m_vServerEnvironment);
			list.AddInt32(this.m_vSessionCount);
			list.AddInt32(this.m_vPlayTimeSeconds);
			list.AddInt32(0);
			list.AddString(this.m_vFacebookAppID);
			list.AddString(this.m_vStartupCooldownSeconds.ToString());
			list.AddString(this.m_vAccountCreatedDate);
			list.AddInt32(0);
			list.AddString(this.m_vGoogleID.ToString());
			list.AddString(null);
			list.AddString(this.m_vCountryCode);
			list.AddString("someid2");
			base.Encrypt(list.ToArray());
		}

		public void SetAccountCreatedDate(string date)
		{
			this.m_vAccountCreatedDate = date;
		}

		public void SetAccountId(long id)
		{
			this.m_vAccountId = id;
		}

		public void SetContentVersion(int version)
		{
			this.m_vContentVersion = version;
		}

		public void SetCountryCode(string code)
		{
			this.m_vCountryCode = code;
		}

		public void SetDaysSinceStartedPlaying(int days)
		{
			this.m_vDaysSinceStartedPlaying = days;
		}

		public void SetFacebookId(string id)
		{
			this.m_vFacebookId = id;
		}

		public void SetGamecenterId(string id)
		{
			this.m_vGamecenterId = id;
		}

		public void SetPassToken(string token)
		{
			this.m_vPassToken = token;
		}

		public void SetPlayTimeSeconds(int seconds)
		{
			this.m_vPlayTimeSeconds = seconds;
		}

		public void SetServerBuild(int build)
		{
			this.m_vServerBuild = build;
		}

		public void SetServerEnvironment(string env)
		{
			this.m_vServerEnvironment = env;
		}

		public void SetServerMajorVersion(int version)
		{
			this.m_vServerMajorVersion = version;
		}

		public void SetServerTime(string time)
		{
			this.m_vServerTime = time;
		}

		public void SetSessionCount(int count)
		{
			this.m_vSessionCount = count;
		}

		public void SetStartupCooldownSeconds(int seconds)
		{
			this.m_vStartupCooldownSeconds = seconds;
		}
	}
}
