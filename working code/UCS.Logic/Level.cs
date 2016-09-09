using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UCS.PacketProcessing;

namespace UCS.Logic
{
	internal class Level
	{
		private readonly ClientAvatar m_vClientAvatar;

		public GameObjectManager GameObjectManager;

		private byte m_vAccountPrivileges;

		private byte m_vAccountStatus;

		private Client m_vClient;

		private string m_vIPAddress;

		private DateTime m_vTime;

		public Level()
		{
			this.GameObjectManager = new GameObjectManager(this);
			this.m_vClientAvatar = new ClientAvatar();
			this.m_vAccountPrivileges = 0;
			this.m_vAccountStatus = 0;
			this.m_vIPAddress = "0.0.0.0";
		}

		public Level(long id)
		{
			this.GameObjectManager = new GameObjectManager(this);
			this.m_vClientAvatar = new ClientAvatar(id);
			this.m_vTime = DateTime.UtcNow;
			this.m_vAccountPrivileges = 0;
			this.m_vAccountStatus = 0;
			this.m_vIPAddress = "0.0.0.0";
		}

		public byte GetAccountPrivileges()
		{
			return this.m_vAccountPrivileges;
		}

		public string GetIPAddress()
		{
			return this.m_vIPAddress;
		}

		public void SetIPAddress(string IP)
		{
			this.m_vIPAddress = IP;
		}

		public byte GetAccountStatus()
		{
			return this.m_vAccountStatus;
		}

		public Client GetClient()
		{
			return this.m_vClient;
		}

		public ClientAvatar GetHomeOwnerAvatar()
		{
			return this.m_vClientAvatar;
		}

		public ClientAvatar GetPlayerAvatar()
		{
			return this.m_vClientAvatar;
		}

		public DateTime GetTime()
		{
			return this.m_vTime;
		}

		public void LoadFromJSON(string jsonString)
		{
			JObject jsonObject = JObject.Parse(jsonString);
			this.GameObjectManager.Load(jsonObject);
		}

		public string SaveToJSON()
		{
			return JsonConvert.SerializeObject(this.GameObjectManager.Save());
		}

		public void SetAccountPrivileges(byte privileges)
		{
			this.m_vAccountPrivileges = privileges;
		}

		public void SetAccountStatus(byte status)
		{
			this.m_vAccountStatus = status;
		}

		public void SetClient(Client client)
		{
			this.m_vClient = client;
		}

		public void SetHome(string jsonHome)
		{
			this.GameObjectManager.Load(JObject.Parse(jsonHome));
		}

		public void SetTime(DateTime t)
		{
			this.m_vTime = t;
		}

		public void Tick()
		{
			this.SetTime(DateTime.UtcNow);
		}
	}
}
