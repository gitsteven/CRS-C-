using System;
using System.Collections.Generic;

namespace UCS.PacketProcessing
{
	internal class CancelAttackMessage : Message
	{
		public CancelAttackMessage(Client client) : base(client)
		{
			base.SetMessageType(24125);
		}

		public override void Encode()
		{
			base.Encrypt(new List<byte>
			{
				1
			}.ToArray());
		}
	}
}
