using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	internal class GetDeviceTokenMessage : Message
	{
		public GetDeviceTokenMessage(Client client, BinaryReader br) : base(client, br)
		{
			base.Decrypt();
		}

		public override void Decode()
		{
		}

		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new SetDeviceTokenMessage(base.Client, level));
		}
	}
}
