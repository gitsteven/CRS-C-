using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
	internal class FriendListMessage : Message
	{
		public FriendListMessage(Client client) : base(client)
		{
			base.SetMessageType(20105);
		}

		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(1);
			list.AddInt32(0);
			base.Encrypt(list.ToArray());
		}
	}
}
