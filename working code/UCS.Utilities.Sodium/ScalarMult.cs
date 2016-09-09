using System;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	public static class ScalarMult
	{
		private const int BYTES = 32;

		private const int SCALAR_BYTES = 32;

		public static int Bytes()
		{
			return SodiumLibrary.crypto_scalarmult_bytes();
		}

		public static int ScalarBytes()
		{
			return SodiumLibrary.crypto_scalarmult_scalarbytes();
		}

		private static byte Primitive()
		{
			return SodiumLibrary.crypto_scalarmult_primitive();
		}

		public static byte[] Base(byte[] secretKey)
		{
			if (secretKey == null || secretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("secretKey", (secretKey == null) ? 0 : secretKey.Length, string.Format("secretKey must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[32];
			SodiumLibrary.crypto_scalarmult_base(array, secretKey);
			return array;
		}

		public static byte[] Mult(byte[] secretKey, byte[] publicKey)
		{
			if (secretKey == null || secretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("secretKey", (secretKey == null) ? 0 : secretKey.Length, string.Format("secretKey must be {0} bytes in length.", 32));
			}
			if (publicKey == null || publicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("publicKey", (publicKey == null) ? 0 : publicKey.Length, string.Format("publicKey must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[32];
			SodiumLibrary.crypto_scalarmult(array, secretKey, publicKey);
			return array;
		}
	}
}
