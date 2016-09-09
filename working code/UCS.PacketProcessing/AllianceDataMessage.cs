using System;
using System.Collections.Generic;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	internal class AllianceDataMessage : Message
	{
		public AllianceDataMessage(Client client, Level level) : base(client)
		{
			base.SetMessageType(24302);
		}

		public override void Encode()
		{
			List<byte> list = new List<byte>();
			base.Encrypt(list.ToArray());
		}
	}
}
