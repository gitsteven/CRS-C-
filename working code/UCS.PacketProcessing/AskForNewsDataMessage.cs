using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	internal class AskForNewsDataMessage : Message
	{
		public AskForNewsDataMessage(Client client, BinaryReader br) : base(client, br)
		{
			base.Decrypt();
		}

		public override void Decode()
		{
		}

		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new NewsDataMessage(base.Client, level));
		}
	}
}
