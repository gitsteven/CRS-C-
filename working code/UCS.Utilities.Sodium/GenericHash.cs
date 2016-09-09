using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	public class GenericHash
	{
		public class GenericHashAlgorithm : HashAlgorithm
		{
			private readonly int bytes;

			private readonly IntPtr hashStatePtr;

			private readonly byte[] key;

			public GenericHashAlgorithm(string key, int bytes) : this(Encoding.UTF8.GetBytes(key), bytes)
			{
			}

			public GenericHashAlgorithm(byte[] key, int bytes)
			{
				this.hashStatePtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SodiumLibrary._HashState)));
				if (key != null)
				{
					if (key.Length > 64 || key.Length < 16)
					{
						throw new KeyOutOfRangeException(string.Format("key must be between {0} and {1} bytes in length.", 16, 64));
					}
					int arg_52_0 = key.Length;
				}
				else
				{
					key = new byte[0];
				}
				this.key = key;
				if (bytes > 64 || bytes < 16)
				{
					throw new BytesOutOfRangeException("bytes", bytes, string.Format("bytes must be between {0} and {1} bytes in length.", 16, 64));
				}
				this.bytes = bytes;
				this.Initialize();
			}

			~GenericHashAlgorithm()
			{
				Marshal.FreeHGlobal(this.hashStatePtr);
			}

			public override void Initialize()
			{
				SodiumLibrary.hash_init(this.hashStatePtr, this.key, this.key.Length, this.bytes);
			}

			protected override void HashCore(byte[] array, int ibStart, int cbSize)
			{
				byte[] array2 = new byte[cbSize];
				Array.Copy(array, ibStart, array2, 0, cbSize);
				SodiumLibrary.hash_update(this.hashStatePtr, array2, (long)cbSize);
			}

			protected override byte[] HashFinal()
			{
				byte[] array = new byte[this.bytes];
				SodiumLibrary.hash_final(this.hashStatePtr, array, this.bytes);
				return array;
			}
		}

		private const int BYTES_MIN = 16;

		private const int BYTES_MAX = 64;

		private const int KEY_BYTES_MIN = 16;

		private const int KEY_BYTES_MAX = 64;

		private const int OUT_BYTES = 64;

		private const int SALT_BYTES = 16;

		private const int PERSONAL_BYTES = 16;

		public static byte[] GenerateKey()
		{
			return SodiumCore.GetRandomBytes(64);
		}

		public static byte[] Hash(string message, string key, int bytes)
		{
			return GenericHash.Hash(message, Encoding.UTF8.GetBytes(key), bytes);
		}

		public static byte[] Hash(string message, byte[] key, int bytes)
		{
			return GenericHash.Hash(Encoding.UTF8.GetBytes(message), key, bytes);
		}

		public static byte[] Hash(byte[] message, byte[] key, int bytes)
		{
			int keyLength;
			if (key != null)
			{
				if (key.Length > 64 || key.Length < 16)
				{
					throw new KeyOutOfRangeException(string.Format("key must be between {0} and {1} bytes in length.", 16, 64));
				}
				keyLength = key.Length;
			}
			else
			{
				key = new byte[0];
				keyLength = 0;
			}
			if (bytes > 64 || bytes < 16)
			{
				throw new BytesOutOfRangeException("bytes", bytes, string.Format("bytes must be between {0} and {1} bytes in length.", 16, 64));
			}
			byte[] array = new byte[bytes];
			SodiumLibrary.crypto_generichash(array, array.Length, message, (long)message.Length, key, keyLength);
			return array;
		}

		public static byte[] HashSaltPersonal(string message, string key, string salt, string personal, int bytes = 64)
		{
			return GenericHash.HashSaltPersonal(Encoding.UTF8.GetBytes(message), Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(salt), Encoding.UTF8.GetBytes(personal), bytes);
		}

		public static byte[] HashSaltPersonal(byte[] message, byte[] key, byte[] salt, byte[] personal, int bytes = 64)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message", "Message cannot be null");
			}
			if (salt == null)
			{
				throw new ArgumentNullException("salt", "Salt cannot be null");
			}
			if (personal == null)
			{
				throw new ArgumentNullException("personal", "Personal string cannot be null");
			}
			if (key != null && (key.Length > 64 || key.Length < 16))
			{
				throw new KeyOutOfRangeException(string.Format("key must be between {0} and {1} bytes in length.", 16, 64));
			}
			if (key == null)
			{
				key = new byte[0];
			}
			if (salt.Length != 16)
			{
				throw new SaltOutOfRangeException(string.Format("Salt must be {0} bytes in length.", 16));
			}
			if (personal.Length != 16)
			{
				throw new PersonalOutOfRangeException(string.Format("Personal bytes must be {0} bytes in length.", 16));
			}
			if (bytes > 64 || bytes < 16)
			{
				throw new BytesOutOfRangeException("bytes", bytes, string.Format("bytes must be between {0} and {1} bytes in length.", 16, 64));
			}
			byte[] array = new byte[bytes];
			SodiumLibrary.crypto_generichash_blake2b_salt_personal(array, array.Length, message, (long)message.Length, key, key.Length, salt, personal);
			return array;
		}
	}
}
