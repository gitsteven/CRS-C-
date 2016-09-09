using System;
using System.Security.Cryptography;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	public static class StreamEncryption
	{
		private const int XSALSA20_KEY_BYTES = 32;

		private const int XSALSA20_NONCE_BYTES = 24;

		private const int CHACHA20_KEY_BYTES = 32;

		private const int CHACHA20_NONCEBYTES = 8;

		public static byte[] GenerateKey()
		{
			return SodiumCore.GetRandomBytes(32);
		}

		public static byte[] GenerateNonce()
		{
			return SodiumCore.GetRandomBytes(24);
		}

		public static byte[] GenerateNonceChaCha20()
		{
			return SodiumCore.GetRandomBytes(8);
		}

		public static byte[] Encrypt(string message, byte[] nonce, byte[] key)
		{
			return StreamEncryption.Encrypt(Encoding.UTF8.GetBytes(message), nonce, key);
		}

		public static byte[] Encrypt(byte[] message, byte[] nonce, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			byte[] array = new byte[message.Length];
			if (SodiumLibrary.crypto_stream_xor(array, message, (long)message.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Error encrypting message.");
			}
			return array;
		}

		public static byte[] EncryptChaCha20(string message, byte[] nonce, byte[] key)
		{
			return StreamEncryption.EncryptChaCha20(Encoding.UTF8.GetBytes(message), nonce, key);
		}

		public static byte[] EncryptChaCha20(byte[] message, byte[] nonce, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 8)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 8));
			}
			byte[] array = new byte[message.Length];
			if (SodiumLibrary.crypto_stream_chacha20_xor(array, message, (long)message.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Error encrypting message.");
			}
			return array;
		}

		public static byte[] Decrypt(string cipherText, byte[] nonce, byte[] key)
		{
			return StreamEncryption.Decrypt(Utilities.HexToBinary(cipherText), nonce, key);
		}

		public static byte[] Decrypt(byte[] cipherText, byte[] nonce, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			byte[] array = new byte[cipherText.Length];
			if (SodiumLibrary.crypto_stream_xor(array, cipherText, (long)cipherText.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Error derypting message.");
			}
			return array;
		}

		public static byte[] DecryptChaCha20(string cipherText, byte[] nonce, byte[] key)
		{
			return StreamEncryption.DecryptChaCha20(Utilities.HexToBinary(cipherText), nonce, key);
		}

		public static byte[] DecryptChaCha20(byte[] cipherText, byte[] nonce, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 8)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 8));
			}
			byte[] array = new byte[cipherText.Length];
			if (SodiumLibrary.crypto_stream_chacha20_xor(array, cipherText, (long)cipherText.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Error derypting message.");
			}
			return array;
		}
	}
}
