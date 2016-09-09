using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	internal class AskForBattleReplayStreamMessage : Message
	{
		public AskForBattleReplayStreamMessage(Client client, BinaryReader br) : base(client, br)
		{
			base.Decrypt();
		}

		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new CancelAttackMessage(base.Client));
		}
	}
}
