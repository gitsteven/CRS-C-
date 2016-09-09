using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCS.PacketProcessing
{
    internal class SectorCommandMessage : Message
    {
        public SectorCommandMessage(Client client, BinaryReader br) : base(client, br)
        {
            base.SetMessageType(12904);
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
