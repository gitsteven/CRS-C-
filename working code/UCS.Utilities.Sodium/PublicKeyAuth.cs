using System;
using System.Security.Cryptography;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	public static class PublicKeyAuth
	{
		private const int SECRET_KEY_BYTES = 64;

		private const int PUBLIC_KEY_BYTES = 32;

		private const int SIGNATURE_BYTES = 64;

		private const int BYTES = 64;

		private const int SEED_BYTES = 32;

		public static KeyPair GenerateKeyPair()
		{
			byte[] publicKey = new byte[32];
			byte[] array = new byte[64];
			SodiumLibrary.crypto_sign_keypair(publicKey, array);
			return new KeyPair(publicKey, array);
		}

		public static KeyPair GenerateKeyPair(byte[] seed)
		{
			byte[] publicKey = new byte[32];
			byte[] array = new byte[64];
			if (seed == null || seed.Length != 32)
			{
				throw new SeedOutOfRangeException("seed", (seed == null) ? 0 : seed.Length, string.Format("seed must be {0} bytes in length.", 32));
			}
			SodiumLibrary.crypto_sign_seed_keypair(publicKey, array, seed);
			return new KeyPair(publicKey, array);
		}

		public static byte[] Sign(string message, byte[] key)
		{
			return PublicKeyAuth.Sign(Encoding.UTF8.GetBytes(message), key);
		}

		public static byte[] Sign(byte[] message, byte[] key)
		{
			if (key == null || key.Length != 64)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 64));
			}
			byte[] array = new byte[message.Length + 64];
			long num = 0L;
			SodiumLibrary.crypto_sign(array, ref num, message, (long)message.Length, key);
			byte[] array2 = new byte[num];
			Array.Copy(array, 0L, array2, 0L, num);
			return array2;
		}

		public static byte[] Verify(byte[] signedMessage, byte[] key)
		{
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[signedMessage.Length];
			long num = 0L;
			if (SodiumLibrary.crypto_sign_open(array, ref num, signedMessage, (long)signedMessage.Length, key) != 0)
			{
				throw new CryptographicException("Failed to verify signature.");
			}
			byte[] array2 = new byte[num];
			Array.Copy(array, 0L, array2, 0L, num);
			return array2;
		}

		public static byte[] SignDetached(string message, byte[] key)
		{
			return PublicKeyAuth.SignDetached(Encoding.UTF8.GetBytes(message), key);
		}

		public static byte[] SignDetached(byte[] message, byte[] key)
		{
			if (key == null || key.Length != 64)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 64));
			}
			byte[] array = new byte[64];
			long num = 0L;
			SodiumLibrary.crypto_sign_detached(array, ref num, message, (long)message.Length, key);
			return array;
		}

		public static bool VerifyDetached(byte[] signature, byte[] message, byte[] key)
		{
			if (signature == null || signature.Length != 64)
			{
				throw new SignatureOutOfRangeException("signature", (signature == null) ? 0 : signature.Length, string.Format("signature must be {0} bytes in length.", 64));
			}
			if (key == null || key.Length != 32)
			{
				throw new KeyOutOfRangeException("key", (key == null) ? 0 : key.Length, string.Format("key must be {0} bytes in length.", 32));
			}
			return SodiumLibrary.crypto_sign_verify_detached(signature, message, (long)message.Length, key) == 0;
		}

		public static byte[] ConvertEd25519PublicKeyToCurve25519PublicKey(byte[] ed25519PublicKey)
		{
			if (ed25519PublicKey == null || ed25519PublicKey.Length != 32)
			{
				throw new KeyOutOfRangeException("ed25519PublicKey", (ed25519PublicKey == null) ? 0 : ed25519PublicKey.Length, string.Format("ed25519PublicKey must be {0} bytes in length.", 32));
			}
			byte[] array = new byte[32];
			if (SodiumLibrary.crypto_sign_ed25519_pk_to_curve25519(array, ed25519PublicKey) != 0)
			{
				throw new CryptographicException("Failed to convert public key.");
			}
			return array;
		}

		public static byte[] ConvertEd25519SecretKeyToCurve25519SecretKey(byte[] ed25519SecretKey)
		{
			if (ed25519SecretKey == null || (ed25519SecretKey.Length != 32 && ed25519SecretKey.Length != 64))
			{
				throw new KeyOutOfRangeException("ed25519SecretKey", (ed25519SecretKey == null) ? 0 : ed25519SecretKey.Length, string.Format("ed25519SecretKey must be either {0} or {1} bytes in length.", 32, 64));
			}
			byte[] array = new byte[32];
			if (SodiumLibrary.crypto_sign_ed25519_sk_to_curve25519(array, ed25519SecretKey) != 0)
			{
				throw new CryptographicException("Failed to convert secret key.");
			}
			return array;
		}

		public static byte[] ExtractEd25519SeedFromEd25519SecretKey(byte[] ed25519SecretKey)
		{
			if (ed25519SecretKey == null || ed25519SecretKey.Length != 64)
			{
				throw new KeyOutOfRangeException("ed25519SecretKey", (ed25519SecretKey == null) ? 0 : ed25519SecretKey.Length, string.Format("ed25519SecretKey must be {0} bytes in length.", 64));
			}
			byte[] array = new byte[32];
			if (SodiumLibrary.crypto_sign_ed25519_sk_to_seed(array, ed25519SecretKey) != 0)
			{
				throw new CryptographicException("Failed to extract seed from secret key.");
			}
			return array;
		}

		public static byte[] ExtractEd25519PublicKeyFromEd25519SecretKey(byte[] ed25519SecretKey)
		{
			if (ed25519SecretKey == null || ed25519SecretKey.Length != 64)
			{
				throw new KeyOutOfRangeException("ed25519SecretKey", (ed25519SecretKey == null) ? 0 : ed25519SecretKey.Length, string.Format("ed25519SecretKey must be {0} bytes in length.", 64));
			}
			byte[] array = new byte[32];
			if (SodiumLibrary.crypto_sign_ed25519_sk_to_pk(array, ed25519SecretKey) != 0)
			{
				throw new CryptographicException("Failed to extract public key from secret key.");
			}
			return array;
		}
	}
}
