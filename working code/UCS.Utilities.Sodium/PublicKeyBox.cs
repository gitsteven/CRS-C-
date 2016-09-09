using System;
using System.Security.Cryptography;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	public static class PublicKeyBox
	{
		public const int PublicKeyBytes = 32;

		public const int SecretKeyBytes = 32;

		private const int NONCE_BYTES = 24;

		private const int MAC_BYTES = 16;

		public static KeyPair GenerateKeyPair()
		{
			byte[] publicKey = new byte[32];
			byte[] array = new byte[32];
			SodiumLibrary.crypto_box_keypair(publicKey, array);
			return new KeyPair(publicKey, array);
		}

		public static KeyPair GenerateKeyPair(byte[] privateKey)
		{
			if (privateKey == null || privateKey.Length != 32)
			{
				throw new SeedOutOfRangeException("privateKey", (privateKey == null) ? 0 : privateKey.Length, string.Format("privateKey must be {0} bytes in length.", 32));
			}
			return new KeyPair(ScalarMult.Base(privateKey), privateKey);
		}

		public static byte[] GenerateNonce()
		{
			return SodiumCore.GetRandomBytes(24);
		}

		public static byte[] Create(string message, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			return PublicKeyBox.Create(Encoding.UTF8.GetBytes(message), nonce, secretKey, publicKey);
		}

		public static byte[] Create(byte[] message, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			if (secretKey == null || secretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("secretKey", (secretKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (publicKey == null || publicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("publicKey", (publicKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			byte[] array = new byte[message.Length + 16];
			if (SodiumLibrary.crypto_box_easy(array, message, (long)message.Length, nonce, publicKey, secretKey) != 0)
			{
				throw new CryptographicException("Failed to create PublicKeyBox");
			}
			return array;
		}

		public static DetachedBox CreateDetached(string message, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			return PublicKeyBox.CreateDetached(Encoding.UTF8.GetBytes(message), nonce, secretKey, publicKey);
		}

		public static DetachedBox CreateDetached(byte[] message, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			if (secretKey == null || secretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("secretKey", (secretKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (publicKey == null || publicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("publicKey", (publicKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			byte[] array = new byte[message.Length];
			byte[] mac = new byte[16];
			if (SodiumLibrary.crypto_box_detached(array, mac, message, (long)message.Length, nonce, secretKey, publicKey) != 0)
			{
				throw new CryptographicException("Failed to create public detached Box");
			}
			return new DetachedBox(array, mac);
		}

		public static byte[] Open(byte[] cipherText, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			if (secretKey == null || secretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("secretKey", (secretKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (publicKey == null || publicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("publicKey", (publicKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			if (cipherText[0] == 0)
			{
				bool flag = true;
				for (int i = 0; i < 15; i++)
				{
					if (cipherText[i] != 0)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					byte[] array = new byte[cipherText.Length - 16];
					Array.Copy(cipherText, 16, array, 0, cipherText.Length - 16);
					cipherText = array;
				}
			}
			byte[] array2 = new byte[cipherText.Length - 16];
			if (SodiumLibrary.crypto_box_open_easy(array2, cipherText, (long)cipherText.Length, nonce, publicKey, secretKey) != 0)
			{
				throw new CryptographicException("Failed to open PublicKeyBox");
			}
			return array2;
		}

		public static byte[] OpenDetached(string cipherText, byte[] mac, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			return PublicKeyBox.OpenDetached(Utilities.HexToBinary(cipherText), mac, nonce, secretKey, publicKey);
		}

		public static byte[] OpenDetached(DetachedBox detached, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			return PublicKeyBox.OpenDetached(detached.CipherText, detached.Mac, nonce, secretKey, publicKey);
		}

		public static byte[] OpenDetached(byte[] cipherText, byte[] mac, byte[] nonce, byte[] secretKey, byte[] publicKey)
		{
			if (secretKey == null || secretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("secretKey", (secretKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (publicKey == null || publicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("publicKey", (publicKey == null) ? 0 : secretKey.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (mac == null || mac.Length != 16)
			{
				throw new MacOutOfRangeException("mac", (mac == null) ? 0 : mac.Length, string.Format("mac must be {0} bytes in length.", 16));
			}
			if (nonce == null || nonce.Length != 24)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 24));
			}
			byte[] array = new byte[cipherText.Length];
			if (SodiumLibrary.crypto_box_open_detached(array, cipherText, mac, (long)cipherText.Length, nonce, secretKey, publicKey) != 0)
			{
				throw new CryptographicException("Failed to open public detached Box");
			}
			return array;
		}
	}
}
