using System;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	public static class ShortHash
	{
		private const int BYTES = 8;

		private const int KEY_BYTES = 16;

		public static byte[] GenerateKey()
		{
			return SodiumCore.GetRandomBytes(16);
		}

		public static byte[] Hash(string message, string key)
		{
			return ShortHash.Hash(message, Encoding.UTF8.GetBytes(key));
		}

		public static byte[] Hash(string message, byte[] key)
		{
			return ShortHash.Hash(Encoding.UTF8.GetBytes(message), key);
		}

		public static byte[] Hash(byte[] message, byte[] key)
		{
			if (key == null || key.Length != 16)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 16));
			}
			byte[] array = new byte[8];
			SodiumLibrary.crypto_shorthash(array, message, (long)message.Length, key);
			return array;
		}
	}
}
