using System;
using System.IO;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
	internal class SessionRequest : Message
	{
		public string Hash;

		public int MajorVersion;

		public int MinorVersion;

		public int Unknown1;

		public int Unknown2;

		public int Unknown4;

		public int Unknown6;

		public int Unknown7;

		public SessionRequest(Client client, BinaryReader br) : base(client, br)
		{
		}

		public override void Decode()
		{
			using (CoCSharpPacketReader coCSharpPacketReader = new CoCSharpPacketReader(new MemoryStream(base.GetData())))
			{
				this.Unknown1 = coCSharpPacketReader.ReadInt32();
				this.Unknown2 = coCSharpPacketReader.ReadInt32();
				this.MajorVersion = coCSharpPacketReader.ReadInt32();
				this.Unknown4 = coCSharpPacketReader.ReadInt32();
				this.MinorVersion = coCSharpPacketReader.ReadInt32();
				this.Hash = coCSharpPacketReader.ReadString();
				this.Unknown6 = coCSharpPacketReader.ReadInt32();
				this.Unknown7 = coCSharpPacketReader.ReadInt32();
			}
			if (this.MajorVersion == 2 && this.MinorVersion == 1507)
			{
				base.Client.CState = 1;
				return;
			}
			base.Client.CState = 0;
		}

		public override void Process(Level level)
		{
			PacketManager.ProcessOutgoingPacket(new SessionSuccess(base.Client, this));
		}
	}
}
