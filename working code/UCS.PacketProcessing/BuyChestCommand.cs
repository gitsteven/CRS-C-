using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	internal class BuyChestCommand : Command
	{
		public static int Unknown1
		{
			get;
			set;
		}

		public static int Tick
		{
			get;
			set;
		}

		public static byte[] Packet
		{
			get;
			set;
		}

		public BuyChestCommand(BinaryReader br)
		{
		}

		public override void Execute(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new ChestDataMessage(level.GetClient()));
		}
	}
}
