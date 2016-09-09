using System;
using System.Collections.Generic;

namespace UCS.PacketProcessing
{
	internal class KeepAliveOkMessage : Message
	{
		public KeepAliveOkMessage(Client client, KeepAliveMessage cka) : base(client)
		{
			base.SetMessageType(20108);
		}

		public override void Encode()
		{
			List<byte> list = new List<byte>();
			base.Encrypt(list.ToArray());
		}
	}
}
