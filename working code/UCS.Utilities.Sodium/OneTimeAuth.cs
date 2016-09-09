using System;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	public static class OneTimeAuth
	{
		private const int KEY_BYTES = 32;

		private const int BYTES = 16;

		public static byte[] GenerateKey()
		{
			return SodiumCore.GetRandomBytes(32);
		}

		public static byte[] Sign(string message, byte[] key)
		{
			return OneTimeAuth.Sign(Encoding.UTF8.GetBytes(message), key);
		}

		public static byte[] Sign(byte[] message, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[16];
			SodiumLibrary.crypto_onetimeauth(array, message, (long)message.Length, key);
			return array;
		}

		public static bool Verify(string message, byte[] signature, byte[] key)
		{
			return OneTimeAuth.Verify(Encoding.UTF8.GetBytes(message), signature, key);
		}

		public static bool Verify(byte[] message, byte[] signature, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (signature == null || signature.Length != 16)
			{
				throw new SignatureOutOfRangeException("signature", (signature == null) ? 0 : signature.Length, string.Format("signature must be {0} bytes in length.", 16));
			}
			return SodiumLibrary.crypto_onetimeauth_verify(signature, message, (long)message.Length, key) == 0;
		}
	}
}
