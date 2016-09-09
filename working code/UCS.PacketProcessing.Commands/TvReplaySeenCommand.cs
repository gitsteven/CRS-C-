using System;
using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing.Commands
{
	internal class TvReplaySeenCommand : Command
	{
		public TvReplaySeenCommand(BinaryReader br)
		{
			br.ReadInt32();
			br.ReadInt32();
			br.ReadByte();
			br.ReadByte();
		}

		public override void Execute(Level level)
		{
		}
	}
}
