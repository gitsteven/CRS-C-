using System;
using System.Runtime.InteropServices;
using System.Text;

namespace UCS.Utilities.Sodium
{
	public static class Utilities
	{
		public enum HexCase
		{
			Lower,
			Upper
		}

		public enum HexFormat
		{
			None,
			Colon,
			Hyphen,
			Space
		}

		public static string BinaryToHex(byte[] data)
		{
			byte[] array = new byte[data.Length * 2 + 1];
			IntPtr expr_1F = SodiumLibrary.sodium_bin2hex(array, array.Length, data, data.Length);
			if (expr_1F == IntPtr.Zero)
			{
				throw new OverflowException("Internal error, encoding failed.");
			}
			return Marshal.PtrToStringAnsi(expr_1F);
		}

		public static string BinaryToHex(byte[] data, Utilities.HexFormat format, Utilities.HexCase hcase = Utilities.HexCase.Lower)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				if (i != 0 && format != Utilities.HexFormat.None)
				{
					switch (format)
					{
					case Utilities.HexFormat.Colon:
						stringBuilder.Append(':');
						break;
					case Utilities.HexFormat.Hyphen:
						stringBuilder.Append('-');
						break;
					case Utilities.HexFormat.Space:
						stringBuilder.Append(' ');
						break;
					}
				}
				int num = data[i] >> 4;
				if (hcase == Utilities.HexCase.Lower)
				{
					stringBuilder.Append((char)(87 + num + (num - 10 >> 31 & -39)));
				}
				else
				{
					stringBuilder.Append((char)(55 + num + (num - 10 >> 31 & -7)));
				}
				num = (int)(data[i] & 15);
				if (hcase == Utilities.HexCase.Lower)
				{
					stringBuilder.Append((char)(87 + num + (num - 10 >> 31 & -39)));
				}
				else
				{
					stringBuilder.Append((char)(55 + num + (num - 10 >> 31 & -7)));
				}
			}
			return stringBuilder.ToString();
		}

		public static byte[] HexToBinary(string hex)
		{
			byte[] array = new byte[hex.Length >> 1];
			IntPtr intPtr = Marshal.AllocHGlobal(array.Length);
			int num;
			bool arg_43_0 = SodiumLibrary.sodium_hex2bin(intPtr, array.Length, hex, hex.Length, ":- ", out num, null) != 0;
			Marshal.Copy(intPtr, array, 0, num);
			Marshal.FreeHGlobal(intPtr);
			if (arg_43_0)
			{
				throw new Exception("Internal error, decoding failed.");
			}
			if (array.Length != num)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				return array2;
			}
			return array;
		}

		public static byte[] Increment(byte[] value)
		{
			SodiumLibrary.sodium_increment(value, (long)value.Length);
			return value;
		}

		public static bool Compare(byte[] a, byte[] b)
		{
			return SodiumLibrary.sodium_compare(a, b, (long)a.Length) == 0;
		}
	}
}
