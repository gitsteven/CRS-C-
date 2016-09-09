using System;
using System.Security.Cryptography;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	public static class SecretBox
	{
		private const int KEY_BYTES = 32;

		private const int NONCE_BYTES = 24;

		private const int ZERO_BYTES = 32;

		private const int MAC_BYTES = 16;

		public static byte[] GenerateKey()
		{
			return SodiumCore.GetRandomBytes(32);
		}

		public static byte[] GenerateNonce()
		{
			return SodiumCore.GetRandomBytes(24);
		}

		public static byte[] Create(string message, byte[] nonce, byte[] key)
		{
			return SecretBox.Create(Encoding.UTF8.GetBytes(message), nonce, key);
		}

		public static byte[] Create(byte[] message, byte[] nonce, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			byte[] array = new byte[message.Length + 32];
			Array.Copy(message, 0, array, 32, message.Length);
			byte[] array2 = new byte[array.Length];
			if (SodiumLibrary.crypto_secretbox(array2, array, (long)array.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Failed to create SecretBox");
			}
			return array2;
		}

		public static DetachedBox CreateDetached(string message, byte[] nonce, byte[] key)
		{
			return SecretBox.CreateDetached(Encoding.UTF8.GetBytes(message), nonce, key);
		}

		public static DetachedBox CreateDetached(byte[] message, byte[] nonce, byte[] key)
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
			byte[] mac = new byte[16];
			if (SodiumLibrary.crypto_secretbox_detached(array, mac, message, (long)message.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Failed to create detached SecretBox");
			}
			return new DetachedBox(array, mac);
		}

		public static byte[] Open(string cipherText, byte[] nonce, byte[] key)
		{
			return SecretBox.Open(Utilities.HexToBinary(cipherText), nonce, key);
		}

		public static byte[] Open(byte[] cipherText, byte[] nonce, byte[] key)
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
			if (SodiumLibrary.crypto_secretbox_open(array, cipherText, (long)cipherText.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Failed to open SecretBox");
			}
			byte[] array2 = new byte[array.Length - 32];
			Array.Copy(array, 32, array2, 0, array.Length - 32);
			return array2;
		}

		public static byte[] OpenDetached(string cipherText, byte[] mac, byte[] nonce, byte[] key)
		{
			return SecretBox.OpenDetached(Utilities.HexToBinary(cipherText), mac, nonce, key);
		}

		public static byte[] OpenDetached(DetachedBox detached, byte[] nonce, byte[] key)
		{
			return SecretBox.OpenDetached(detached.CipherText, detached.Mac, nonce, key);
		}

		public static byte[] OpenDetached(byte[] cipherText, byte[] mac, byte[] nonce, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			if (mac == null || mac.Length != 16)
			{
				throw new MacOutOfRangeException("mac", (mac == null) ? 0 : mac.Length, string.Format("mac must be {0} bytes in length.", 16));
			}
			byte[] array = new byte[cipherText.Length];
			if (SodiumLibrary.crypto_secretbox_open_detached(array, cipherText, mac, (long)cipherText.Length, nonce, key) != 0)
			{
				throw new CryptographicException("Failed to open detached SecretBox");
			}
			return array;
		}
	}
}
