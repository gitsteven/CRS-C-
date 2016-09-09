using System;
using System.IO;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	internal class ChangeAvatarNameMessage : Message
	{
		public string PlayerName
		{
			get;
			set;
		}

		public int PlayerNameLength
		{
			get;
			set;
		}

		public byte Unknown1
		{
			get;
			set;
		}

		public ChangeAvatarNameMessage(Client client, BinaryReader br) : base(client, br)
		{
			base.Decrypt();
		}

		public override void Decode()
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
			{
				this.PlayerName = binaryReader.ReadScString();
				this.Unknown1 = binaryReader.ReadByte();
			}
		}

		public override void Process(Level level)
		{
			level.GetPlayerAvatar().SetName(this.PlayerName);
			AvatarNameChangeOkMessage expr_1C = new AvatarNameChangeOkMessage(base.Client);
			expr_1C.SetAvatarName(this.PlayerName);
			PacketManager.ProcessOutgoingPacket(expr_1C);
		}
	}
}
