using System;
using System.Collections.Generic;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	internal class OwnHomeDataMessage : Message
	{
		public ClientAvatar Player
		{
			get;
			set;
		}

		public Client PlayerClient
		{
			get;
			set;
		}

		public OwnHomeDataMessage(Client client, Level level) : base(client)
		{
			base.SetMessageType(24101);
			this.Player = level.GetPlayerAvatar();
			this.PlayerClient = client;
		}

		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(this.Player.Encode());
			list.AddInt32(this.PlayerClient.ClientSeed.ToString().Length);
			list.AddRange(BitConverter.GetBytes(this.PlayerClient.ClientSeed));
			base.Encrypt(list.ToArray());
		}
	}
}
