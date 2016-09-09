using System;

namespace UCS.Database
{
	public class player
	{
		public long PlayerId
		{
			get;
			set;
		}

		public byte AccountStatus
		{
			get;
			set;
		}

		public byte AccountPrivileges
		{
			get;
			set;
		}

		public DateTime LastUpdateTime
		{
			get;
			set;
		}

		public string IPAddress
		{
			get;
			set;
		}

		public string Avatar
		{
			get;
			set;
		}

		public string GameObjects
		{
			get;
			set;
		}
	}
}
