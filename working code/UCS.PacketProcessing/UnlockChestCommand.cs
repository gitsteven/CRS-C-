using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	internal class UnlockChestCommand : Command
	{
		public UnlockChestCommand(BinaryReader br)
		{
		}

		public override void Execute(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new ChestDataMessage(level.GetClient()));
		}
	}
}
