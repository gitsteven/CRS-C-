using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	internal class HomeLogicStoppedMessage : Message
	{
		public HomeLogicStoppedMessage(Client client, BinaryReader br) : base(client, br)
		{
			base.Decrypt();
		}

		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new UdpConnectionInfoMessage(base.Client));
			PacketManager.ProcessOutgoingPacket(new SectorStateMessage(base.Client));
		}
	}
}
