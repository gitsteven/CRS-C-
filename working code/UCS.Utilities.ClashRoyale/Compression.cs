using System;
using System.IO;

namespace UCS.Utilities.ClashRoyale
{
	internal class Compression
	{
		public static byte[] ToVInt(int i)
		{
			MemoryStream memoryStream = new MemoryStream(5);
			while ((i & -128) != 0)
			{
				memoryStream.WriteByte((byte)((i & 63) | 128));
				i >>= 6;
			}
			memoryStream.WriteByte((byte)i);
			return memoryStream.ToArray();
		}

		public static int VIntToInt(byte[] vInt)
		{
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(vInt));
			byte b = binaryReader.ReadByte();
			int num = (int)(b & 63);
			int num2 = 6;
			while ((b & 128) != 0)
			{
				b = binaryReader.ReadByte();
				num |= (int)(b & 63) << num2;
				num2 += 6;
			}
			return num;
		}
	}
}
