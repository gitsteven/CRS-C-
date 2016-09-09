using System;
using System.Collections.Generic;
using System.Text;

namespace UCS.PacketProcessing
{
	internal class AvatarNameChangeOkMessage : Message
	{
		private string m_vAvatarName;

		public AvatarNameChangeOkMessage(Client client) : base(client)
		{
			base.SetMessageType(24111);
			this.m_vAvatarName = "";
		}

		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.Add(137);
			list.Add(3);
			list.Add(0);
			list.Add(0);
			list.Add(0);
			list.Add((byte)this.m_vAvatarName.Length);
			list.AddRange(Encoding.Default.GetBytes(this.m_vAvatarName));
			list.Add(0);
			list.Add(0);
			list.Add(0);
			list.Add(0);
			list.Add(1);
			list.Add(7);
			list.Add(127);
			list.Add(127);
			list.Add(0);
			list.Add(0);
			base.Encrypt(list.ToArray());
		}

		public string GetAvatarName()
		{
			return this.m_vAvatarName;
		}

		public void SetAvatarName(string name)
		{
			this.m_vAvatarName = name;
		}
	}
}
