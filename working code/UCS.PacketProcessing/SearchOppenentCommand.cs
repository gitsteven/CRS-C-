using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	internal class SearchOppenentCommand : Command
	{
		private ulong Unknown;

		public SearchOppenentCommand(BinaryReader br)
		{
		}

		public override void Execute(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new MatchmakeInfoMessage(level.GetClient()));
			PacketManager.ProcessOutgoingPacket(new StopHomeLogicMessage(level.GetClient()));
		}
	}
}
