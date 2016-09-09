using System;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	public static class SecretKeyAuth
	{
		private const int KEY_BYTES = 32;

		private const int BYTES = 32;

		private const int CRYPTO_AUTH_HMACSHA256_KEY_BYTES = 32;

		private const int CRYPTO_AUTH_HMACSHA256_BYTES = 32;

		private const int CRYPTO_AUTH_HMACSHA512_KEY_BYTES = 32;

		private const int CRYPTO_AUTH_HMACSHA512_BYTES = 64;

		public static byte[] GenerateKey()
		{
			return SodiumCore.GetRandomBytes(32);
		}

		public static byte[] Sign(string message, byte[] key)
		{
			return SecretKeyAuth.Sign(Encoding.UTF8.GetBytes(message), key);
		}

		public static byte[] Sign(byte[] message, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[32];
			SodiumLibrary.crypto_auth(array, message, (long)message.Length, key);
			return array;
		}

		public static bool Verify(string message, byte[] signature, byte[] key)
		{
			return SecretKeyAuth.Verify(Encoding.UTF8.GetBytes(message), signature, key);
		}

		public static bool Verify(byte[] message, byte[] signature, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (signature == null || signature.Length != 32)
			{
				throw new SignatureOutOfRangeException("signature", (signature == null) ? 0 : signature.Length, string.Format("signature must be {0} bytes in length.", 32));
			}
			return SodiumLibrary.crypto_auth_verify(signature, message, (long)message.Length, key) == 0;
		}

		public static byte[] SignHmacSha256(byte[] message, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[32];
			SodiumLibrary.crypto_auth_hmacsha256(array, message, (long)message.Length, key);
			return array;
		}

		public static byte[] SignHmacSha256(string message, byte[] key)
		{
			return SecretKeyAuth.SignHmacSha256(Encoding.UTF8.GetBytes(message), key);
		}

		public static byte[] SignHmacSha512(byte[] message, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[64];
			SodiumLibrary.crypto_auth_hmacsha512(array, message, (long)message.Length, key);
			return array;
		}

		public static byte[] SignHmacSha512(string message, byte[] key)
		{
			return SecretKeyAuth.SignHmacSha512(Encoding.UTF8.GetBytes(message), key);
		}

		public static bool VerifyHmacSha256(string message, byte[] signature, byte[] key)
		{
			return SecretKeyAuth.VerifyHmacSha256(Encoding.UTF8.GetBytes(message), signature, key);
		}

		public static bool VerifyHmacSha256(byte[] message, byte[] signature, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (signature == null || signature.Length != 32)
			{
				throw new SignatureOutOfRangeException("signature", (signature == null) ? 0 : signature.Length, string.Format("signature must be {0} bytes in length.", 32));
			}
			return SodiumLibrary.crypto_auth_hmacsha256_verify(signature, message, (long)message.Length, key) == 0;
		}

		public static bool VerifyHmacSha512(string message, byte[] signature, byte[] key)
		{
			return SecretKeyAuth.VerifyHmacSha512(Encoding.UTF8.GetBytes(message), signature, key);
		}

		public static bool VerifyHmacSha512(byte[] message, byte[] signature, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (signature == null || signature.Length != 64)
			{
				throw new SignatureOutOfRangeException("signature", (signature == null) ? 0 : signature.Length, string.Format("signature must be {0} bytes in length.", 64));
			}
			return SodiumLibrary.crypto_auth_hmacsha512_verify(signature, message, (long)message.Length, key) == 0;
		}
	}
}
