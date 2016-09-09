using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
	internal class SessionSuccess : Message
	{
		public byte[] SessionKey;

		public SessionSuccess(Client client, SessionRequest cka) : base(client)
		{
			base.SetMessageType(20100);
			this.SessionKey = Client.GenerateSessionKey();
		}

		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(this.SessionKey.Length);
			list.AddRange(this.SessionKey);
			base.SetData(list.ToArray());
		}
	}
}
