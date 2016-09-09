using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UCS.PacketProcessing
{
    internal class UdpConnectionInfoMessage : Message
    {
        [CompilerGenerated]
        [Serializable]
        private sealed class UdpConnectionInfoMessageInternal
        {
            public static readonly UdpConnectionInfoMessage.UdpConnectionInfoMessageInternal inst = new UdpConnectionInfoMessage.UdpConnectionInfoMessageInternal();

            public static Func<int, bool> funcInt;

            internal bool StringToByteArray(int x)
            {
                return x % 2 == 0;
            }
        }

        private static string hex = "BB91010000000E3231322E3139352E39322E3132300000000A97FC1F349C6663EB1D330000002B65597478376D455A576A48765470776B626635347258656D4E683952346C6C6E68516A346D555975746F45";

        public UdpConnectionInfoMessage(Client client) : base(client)
        {
            base.SetMessageType(24112);
        }

        public override void Encode()
        {
            base.Encrypt(UdpConnectionInfoMessage.StringToByteArray(UdpConnectionInfoMessage.hex));
        }

        public static byte[] StringToByteArray(string hex)
        {
            IEnumerable<int> arg_3D_0 = Enumerable.Range(0, hex.Length);
            Func<int, bool> arg_3D_1 = null;
            if (arg_3D_1 == null)

            {
                arg_3D_1 = UdpConnectionInfoMessage.UdpConnectionInfoMessageInternal.inst.StringToByteArray;
            }
            return (from x in arg_3D_0.Where(arg_3D_1)
                    select Convert.ToByte(hex.Substring(x, 2), 16)).ToArray<byte>();
        }
    }
}
