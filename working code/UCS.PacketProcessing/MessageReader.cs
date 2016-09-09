using System;
using System.IO;
using System.Text;

namespace UCS.PacketProcessing
{
	public class MessageReader : BinaryReader
	{
		public MessageReader(Stream input) : base(input)
		{
		}

		public override double ReadDouble()
		{
			return BitConverter.ToDouble(this.ReadByteArrayEndian(8), 0);
		}

		public override long ReadInt64()
		{
			return (long)this.ReadUInt64();
		}

		public override ulong ReadUInt64()
		{
			return BitConverter.ToUInt64(this.ReadByteArrayEndian(8), 0);
		}

		public override float ReadSingle()
		{
			return BitConverter.ToSingle(this.ReadByteArrayEndian(4), 0);
		}

		public override int ReadInt32()
		{
			return (int)this.ReadUInt32();
		}

		public override uint ReadUInt32()
		{
			return BitConverter.ToUInt32(this.ReadByteArrayEndian(4), 0);
		}

		public override short ReadInt16()
		{
			return (short)this.ReadUInt16();
		}

		public override ushort ReadUInt16()
		{
			return BitConverter.ToUInt16(this.ReadByteArrayEndian(2), 0);
		}

		public override string ReadString()
		{
			int num = this.ReadInt32();
			this.CheckLength(num, "string");
			if (num == -1)
			{
				return null;
			}
			byte[] bytes = this.ReadBytes(num);
			return Encoding.UTF8.GetString(bytes);
		}

		public byte[] ReadBytes()
		{
			int num = this.ReadInt32();
			this.CheckLength(num, "byte array");
			if (num == -1)
			{
				return null;
			}
			return this.ReadBytes(num);
		}

		private byte[] ReadByteArrayEndian(int count)
		{
			byte[] array = this.ReadBytes(count);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return array;
		}

		private void CheckLength(int length, string typeName)
		{
			if (length < -1)
			{
				Console.WriteLine(string.Concat(new object[]
				{
					"The length of a ",
					typeName,
					" was invalid: ",
					length
				}));
			}
			if ((long)length > this.BaseStream.Length - this.BaseStream.Position)
			{
				Console.WriteLine(string.Concat(new object[]
				{
					"The length of a ",
					typeName,
					" was larger than the remaining bytes: ",
					length
				}));
			}
		}
	}
}
