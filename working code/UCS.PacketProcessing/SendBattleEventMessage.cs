using System;
using System.Collections.Generic;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class SendBattleEventMessage : Message
    {
        public SendBattleEventMessage(Client client, BinaryReader br) : base(client, br)
        {
            base.SetMessageType(12951);
        }

        /// <summary>
        /// 假设返回为空
        /// </summary>
        public override void Encode()
        {
            List<byte> list = new List<byte>();
            base.Encrypt(list.ToArray());
        }
    }
}
