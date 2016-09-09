using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
	internal class MatchmakeInfoMessage : Message
	{
		public MatchmakeInfoMessage(Client client) : base(client)
		{
			base.SetMessageType(24107);
		}

		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(0);
			base.Encrypt(list.ToArray());
		}
	}
}
