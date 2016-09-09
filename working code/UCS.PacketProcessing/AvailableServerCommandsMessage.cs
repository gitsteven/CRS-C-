using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
	internal class AvailableServerCommandsMessage : Message
	{
		public AvailableServerCommandsMessage(Client client) : base(client)
		{
			base.SetMessageType(24111);
		}

		public override void Encode()
		{
			List<byte> list = new List<byte>();
			///list.AddRange(Helpers.Helpers.HexaToBytes("950301031A02008CAA9D17010000001A0D008CAA9D17010000001C01008CAA9D1701000000180200020E7F7F0000"));
			base.Encrypt(list.ToArray());
		}
	}
}
