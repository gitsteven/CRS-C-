using System;
using System.Collections.Generic;

namespace UCS.PacketProcessing
{
	internal class StopHomeLogicMessage : Message
	{
		public StopHomeLogicMessage(Client client) : base(client)
		{
			base.SetMessageType(24106);
		}

		public override void Encode()
		{
			List<byte> list = new List<byte>();
			base.Encrypt(list.ToArray());
		}
	}
}
