using System;
using System.IO;
using System.Text;

namespace UCS.Helpers
{
	public class CoCSharpPacketReader : BinaryReader
	{
		public CoCSharpPacketReader(Stream stream) : base(stream)
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.BaseStream.Read(buffer, 0, count);
		}

		public override bool ReadBoolean()
		{
			byte b = this.ReadByte();
			if (b == 0)
			{
				return false;
			}
			if (b == 1)
			{
				return true;
			}
			throw new Exception("Invalid.");
		}

		public override byte ReadByte()
		{
			return (byte)this.BaseStream.ReadByte();
		}

		public byte[] ReadByteArray()
		{
			int num = this.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			if (num < -1)
			{
				throw new Exception("A byte array length was incorrect: " + num + ".");
			}
			if ((long)num > this.BaseStream.Length - this.BaseStream.Position)
			{
				throw new Exception(string.Format("A byte array was larger than remaining bytes. {0} > {1}.", num, this.BaseStream.Length - this.BaseStream.Position));
			}
			return this.ReadBytesWithEndian(num, false);
		}

		public override short ReadInt16()
		{
			return (short)this.ReadUInt16();
		}

		public int ReadInt24()
		{
			byte[] array = this.ReadBytesWithEndian(3, false);
			return (int)array[0] << 16 | (int)array[1] << 8 | (int)array[2];
		}

		public override int ReadInt32()
		{
			return (int)this.ReadUInt32();
		}

		public override long ReadInt64()
		{
			return (long)this.ReadUInt64();
		}

		public override string ReadString()
		{
			int num = this.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			if (num < -1)
			{
				throw new Exception("A string length was incorrect: " + num);
			}
			if ((long)num > this.BaseStream.Length - this.BaseStream.Position)
			{
				throw new Exception(string.Format("A string was larger than remaining bytes. {0} > {1}.", num, this.BaseStream.Length - this.BaseStream.Position));
			}
			byte[] bytes = this.ReadBytesWithEndian(num, false);
			return Encoding.UTF8.GetString(bytes);
		}

		public override ushort ReadUInt16()
		{
			return BitConverter.ToUInt16(this.ReadBytesWithEndian(2, true), 0);
		}

		public uint ReadUInt24()
		{
			return (uint)this.ReadInt24();
		}

		public override uint ReadUInt32()
		{
			return BitConverter.ToUInt32(this.ReadBytesWithEndian(4, true), 0);
		}

		public override ulong ReadUInt64()
		{
			return BitConverter.ToUInt64(this.ReadBytesWithEndian(8, true), 0);
		}

		public long Seek(long offset, SeekOrigin origin)
		{
			return this.BaseStream.Seek(offset, origin);
		}

		private byte[] ReadBytesWithEndian(int count, bool switchEndian = true)
		{
			byte[] array = new byte[count];
			this.BaseStream.Read(array, 0, count);
			if (BitConverter.IsLittleEndian & switchEndian)
			{
				Array.Reverse(array);
			}
			return array;
		}
	}
}
