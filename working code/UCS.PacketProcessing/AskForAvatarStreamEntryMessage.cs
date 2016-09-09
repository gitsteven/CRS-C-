using System;
using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	internal class AskForAvatarStreamEntryMessage : Message
	{
		public AskForAvatarStreamEntryMessage(Client client, BinaryReader br) : base(client, br)
		{
			base.Decrypt();
		}

		public override void Process(Level level)
		{
		}
	}
}
