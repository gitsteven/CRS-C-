using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	internal class GoHomeMessage : Message
	{
		public GoHomeMessage(Client client, BinaryReader br) : base(client, br)
		{
			base.Decrypt();
		}

		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new OwnHomeDataMessage(base.Client, level));
		}
	}
}
