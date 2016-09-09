using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	public static class SecretAead
	{
		private const int KEYBYTES = 32;

		private const int NPUBBYTES = 8;

		private const int ABYTES = 16;

		public static byte[] GenerateNonce()
		{
			return SodiumCore.GetRandomBytes(8);
		}

		public static byte[] Encrypt(byte[] message, byte[] nonce, byte[] key, byte[] additionalData = null)
		{
			if (additionalData == null)
			{
				additionalData = new byte[0];
			}
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 8)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 8));
			}
			if (additionalData.Length > 16 || additionalData.Length < 0)
			{
				throw new AdditionalDataOutOfRangeException(string.Format("additionalData must be between {0} and {1} bytes in length.", 0, 16));
			}
			byte[] array = new byte[message.Length + 16];
			IntPtr intPtr = Marshal.AllocHGlobal(array.Length);
			long num;
			bool arg_DA_0 = SodiumLibrary.crypto_aead_chacha20poly1305_encrypt(intPtr, out num, message, (long)message.Length, additionalData, (long)additionalData.Length, null, nonce, key) != 0;
			Marshal.Copy(intPtr, array, 0, (int)num);
			Marshal.FreeHGlobal(intPtr);
			if (arg_DA_0)
			{
				throw new CryptographicException("Error encrypting message.");
			}
			if ((long)array.Length == num)
			{
				return array;
			}
			byte[] array2 = new byte[num];
			Array.Copy(array, 0L, array2, 0L, num);
			return array2;
		}

		public static byte[] Decrypt(byte[] cipher, byte[] nonce, byte[] key, byte[] additionalData = null)
		{
			if (additionalData == null)
			{
				additionalData = new byte[0];
			}
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			if (nonce == null || nonce.Length != 8)
			{
				throw new NonceOutOfRangeException("nonce", (nonce == null) ? 0 : nonce.Length, string.Format("nonce must be {0} bytes in length.", 8));
			}
			if (additionalData.Length > 16 || additionalData.Length < 0)
			{
				throw new AdditionalDataOutOfRangeException(string.Format("additionalData must be between {0} and {1} bytes in length.", 0, 16));
			}
			byte[] array = new byte[cipher.Length - 16];
			IntPtr intPtr = Marshal.AllocHGlobal(array.Length);
			long num;
			bool arg_DA_0 = SodiumLibrary.crypto_aead_chacha20poly1305_decrypt(intPtr, out num, null, cipher, (long)cipher.Length, additionalData, (long)additionalData.Length, nonce, key) != 0;
			Marshal.Copy(intPtr, array, 0, (int)num);
			Marshal.FreeHGlobal(intPtr);
			if (arg_DA_0)
			{
				throw new CryptographicException("Error decrypting message.");
			}
			if ((long)array.Length == num)
			{
				return array;
			}
			byte[] array2 = new byte[num];
			Array.Copy(array, 0L, array2, 0L, num);
			return array2;
		}
	}
}
