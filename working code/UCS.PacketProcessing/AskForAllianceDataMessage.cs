using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	internal class AskForAllianceDataMessage : Message
	{
		public AskForAllianceDataMessage(Client client, BinaryReader br) : base(client, br)
		{
			base.Decrypt();
		}

		public override void Decode()
		{
		}

		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new AllianceDataMessage(base.Client, level));
		}
	}
}
