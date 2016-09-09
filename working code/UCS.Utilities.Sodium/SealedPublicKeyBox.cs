using System;
using System.Security.Cryptography;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	public static class SealedPublicKeyBox
	{
		public const int RecipientPublicKeyBytes = 32;

		public const int RecipientSecretKeyBytes = 32;

		private const int CryptoBoxSealbytes = 48;

		public static byte[] Create(string message, KeyPair recipientKeyPair)
		{
			return SealedPublicKeyBox.Create(Encoding.UTF8.GetBytes(message), recipientKeyPair.PublicKey);
		}

		public static byte[] Create(byte[] message, KeyPair recipientKeyPair)
		{
			return SealedPublicKeyBox.Create(message, recipientKeyPair.PublicKey);
		}

		public static byte[] Create(string message, byte[] recipientPublicKey)
		{
			return SealedPublicKeyBox.Create(Encoding.UTF8.GetBytes(message), recipientPublicKey);
		}

		public static byte[] Create(byte[] message, byte[] recipientPublicKey)
		{
			if (recipientPublicKey == null || recipientPublicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("recipientPublicKey", (recipientPublicKey == null) ? 0 : recipientPublicKey.Length, string.Format("recipientPublicKey must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[message.Length + 48];
			if (SodiumLibrary.crypto_box_seal(array, message, (long)message.Length, recipientPublicKey) != 0)
			{
				throw new CryptographicException("Failed to create SealedBox");
			}
			return array;
		}

		public static byte[] Open(string cipherText, KeyPair recipientKeyPair)
		{
			return SealedPublicKeyBox.Open(Utilities.HexToBinary(cipherText), recipientKeyPair.PrivateKey, recipientKeyPair.PublicKey);
		}

		public static byte[] Open(byte[] cipherText, KeyPair recipientKeyPair)
		{
			return SealedPublicKeyBox.Open(cipherText, recipientKeyPair.PrivateKey, recipientKeyPair.PublicKey);
		}

		public static byte[] Open(string cipherText, byte[] recipientSecretKey, byte[] recipientPublicKey)
		{
			return SealedPublicKeyBox.Open(Utilities.HexToBinary(cipherText), recipientSecretKey, recipientPublicKey);
		}

		public static byte[] Open(byte[] cipherText, byte[] recipientSecretKey, byte[] recipientPublicKey)
		{
			if (recipientSecretKey == null || recipientSecretKey.Length != 32)
			{
				throw new KeyOutOfRangeException("recipientPublicKey", (recipientSecretKey == null) ? 0 : recipientSecretKey.Length, string.Format("recipientSecretKey must be {0} bytes in length.", 32));
			}
			if (recipientPublicKey == null || recipientPublicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("recipientPublicKey", (recipientPublicKey == null) ? 0 : recipientPublicKey.Length, string.Format("recipientPublicKey must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[cipherText.Length - 48];
			if (SodiumLibrary.crypto_box_seal_open(array, cipherText, (long)cipherText.Length, recipientPublicKey, recipientSecretKey) != 0)
			{
				throw new CryptographicException("Failed to open SealedBox");
			}
			return array;
		}
	}
}
