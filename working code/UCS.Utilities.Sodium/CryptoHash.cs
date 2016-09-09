using System;
using System.Text;

namespace UCS.Utilities.Sodium
{
	public class CryptoHash
	{
		private const int SHA512_BYTES = 64;

		private const int SHA256_BYTES = 32;

		public static byte[] Hash(string message)
		{
			return CryptoHash.Hash(Encoding.UTF8.GetBytes(message));
		}

		public static byte[] Hash(byte[] message)
		{
			byte[] array = new byte[64];
			SodiumLibrary.crypto_hash(array, message, (long)message.Length);
			return array;
		}

		public static byte[] Sha512(string message)
		{
			return CryptoHash.Sha512(Encoding.UTF8.GetBytes(message));
		}

		public static byte[] Sha512(byte[] message)
		{
			byte[] array = new byte[64];
			SodiumLibrary.crypto_hash_sha512(array, message, (long)message.Length);
			return array;
		}

		public static byte[] Sha256(string message)
		{
			return CryptoHash.Sha256(Encoding.UTF8.GetBytes(message));
		}

		public static byte[] Sha256(byte[] message)
		{
			byte[] array = new byte[32];
			SodiumLibrary.crypto_hash_sha256(array, message, (long)message.Length);
			return array;
		}
	}
}
