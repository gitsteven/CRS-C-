using System;
using System.IO;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class ExecuteCommandsMessage : Message
    {
        public uint Checksum;

        public byte[] NestedCommands;

        public uint NumberOfCommands;

        public uint Subtick;

        public ExecuteCommandsMessage(Client client, BinaryReader br) : base(client, br)
        {
            base.Decrypt();
        }

        public override void Decode()
        {
            using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(base.GetData())))
            {
                Subtick = binaryReader.ReadUInt32WithEndian();
                Checksum = binaryReader.ReadUInt32WithEndian();
                NumberOfCommands = binaryReader.ReadUInt32WithEndian();

                if (this.NumberOfCommands > 0u)
                {
                    this.NestedCommands = binaryReader.ReadBytes(base.GetLength());
                }
            }
        }

        public override void Process(Level level)
        {
            try
            {
                level.Tick();
                if (this.NumberOfCommands > 0u)
                {
                    using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(this.NestedCommands)))
                    {
                        int num = 0;
                        while ((long)num < (long)((ulong)this.NumberOfCommands))
                        {
                            object obj = CommandFactory.Read(binaryReader);
                            if (obj == null)
                            {
                                break;
                            }
                            ((Command)obj).Execute(level);
                            num++;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.ResetColor();
            }
        }
    }
}
