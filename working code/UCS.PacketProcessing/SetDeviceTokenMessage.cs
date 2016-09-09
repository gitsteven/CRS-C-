using System;
using System.Collections.Generic;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	internal class SetDeviceTokenMessage : Message
	{
		public SetDeviceTokenMessage(Client client, Level level) : base(client)
		{
			base.SetMessageType(20113);
		}

		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddString("12345678910112548950");
			base.Encrypt(list.ToArray());
		}
	}
}
