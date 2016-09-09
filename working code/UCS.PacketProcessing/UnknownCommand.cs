using System;
using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	internal class UnknownCommand : Command
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

		public UnknownCommand(BinaryReader br)
		{
		}

		public override void Execute(Level level)
		{
		}
	}
}
