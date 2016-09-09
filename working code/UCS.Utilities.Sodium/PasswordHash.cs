using System;
using System.Text;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	public class PasswordHash
	{
		public enum HashType
		{
			Argon,
			Scrypt
		}

		public enum Strength
		{
			Interactive,
			[Obsolete("Use Strength.Medium instead.")]
			Moderate,
			Medium,
			MediumSlow,
			Sensitive
		}

		public enum StrengthArgon
		{
			Interactive,
			Moderate,
			Sensitive
		}

		private const int ARGON_ALGORITHM_DEFAULT = 1;

		private const uint ARGON_STRBYTES = 128u;

		private const uint ARGON_SALTBYTES = 16u;

		private const long ARGON_OPSLIMIT_INTERACTIVE = 4L;

		private const long ARGON_OPSLIMIT_MODERATE = 6L;

		private const long ARGON_OPSLIMIT_SENSITIVE = 8L;

		private const int ARGON_MEMLIMIT_INTERACTIVE = 33554432;

		private const int ARGON_MEMLIMIT_MODERATE = 134217728;

		private const int ARGON_MEMLIMIT_SENSITIVE = 536870912;

		private const uint SCRYPT_SALSA208_SHA256_STRBYTES = 102u;

		private const uint SCRYPT_SALSA208_SHA256_SALTBYTES = 32u;

		private const long SCRYPT_OPSLIMIT_INTERACTIVE = 524288L;

		private const long SCRYPT_OPSLIMIT_MODERATE = 8388608L;

		private const long SCRYPT_OPSLIMIT_MEDIUM = 8388608L;

		private const long SCRYPT_OPSLIMIT_SENSITIVE = 33554432L;

		private const int SCRYPT_MEMLIMIT_INTERACTIVE = 16777216;

		private const int SCRYPT_MEMLIMIT_MODERATE = 100000000;

		private const int SCRYPT_MEMLIMIT_MEDIUM = 134217728;

		private const int SCRYPT_MEMLIMIT_SENSITIVE = 1073741824;

		[Obsolete("Use ScryptGenerateSalt() or ArgonGenerateSalt() instead.")]
		public static byte[] GenerateSalt(PasswordHash.HashType hashType = PasswordHash.HashType.Scrypt)
		{
			if (hashType != PasswordHash.HashType.Argon)
			{
				return PasswordHash.ScryptGenerateSalt();
			}
			return PasswordHash.ArgonGenerateSalt();
		}

		public static byte[] ScryptGenerateSalt()
		{
			return SodiumCore.GetRandomBytes(32);
		}

		public static byte[] ArgonGenerateSalt()
		{
			return SodiumCore.GetRandomBytes(16);
		}

		public static byte[] ArgonHashBinary(byte[] password, byte[] salt, long opsLimit, int memLimit, long outputLength = 16L)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password", "Password cannot be null");
			}
			if (salt == null)
			{
				throw new ArgumentNullException("salt", "Salt cannot be null");
			}
			if ((long)salt.Length != 16L)
			{
				throw new SaltOutOfRangeException(string.Format("Salt must be {0} bytes in length.", 16u));
			}
			if (opsLimit < 3L)
			{
				throw new ArgumentOutOfRangeException("opsLimit", "opsLimit the number of passes, has to be at least 3");
			}
			if (memLimit <= 0)
			{
				throw new ArgumentOutOfRangeException("memLimit", "memLimit cannot be zero or negative");
			}
			if (outputLength <= 0L)
			{
				throw new ArgumentOutOfRangeException("outputLength", "OutputLength cannot be zero or negative");
			}
			byte[] array = new byte[outputLength];
			if (SodiumLibrary.crypto_pwhash(array, (long)array.Length, password, (long)password.Length, salt, opsLimit, memLimit, 1) != 0)
			{
				throw new OutOfMemoryException("Internal error, hash failed (usually because the operating system refused to allocate the amount of requested memory).");
			}
			return array;
		}

		public static byte[] ArgonHashBinary(string password, string salt, PasswordHash.StrengthArgon limit = PasswordHash.StrengthArgon.Interactive, long outputLength = 16L)
		{
			return PasswordHash.ArgonHashBinary(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt), limit, outputLength);
		}

		public static byte[] ArgonHashBinary(byte[] password, byte[] salt, PasswordHash.StrengthArgon limit = PasswordHash.StrengthArgon.Interactive, long outputLength = 16L)
		{
			long opsLimit;
			int memLimit;
			switch (limit)
			{
			case PasswordHash.StrengthArgon.Interactive:
				opsLimit = 4L;
				memLimit = 33554432;
				break;
			case PasswordHash.StrengthArgon.Moderate:
				opsLimit = 6L;
				memLimit = 134217728;
				break;
			case PasswordHash.StrengthArgon.Sensitive:
				opsLimit = 8L;
				memLimit = 536870912;
				break;
			default:
				opsLimit = 4L;
				memLimit = 33554432;
				break;
			}
			return PasswordHash.ArgonHashBinary(password, salt, opsLimit, memLimit, outputLength);
		}

		public static byte[] ArgonHashBinary(string password, string salt, long opsLimit, int memLimit, long outputLength = 16L)
		{
			byte[] arg_1C_0 = Encoding.UTF8.GetBytes(password);
			byte[] bytes = Encoding.UTF8.GetBytes(salt);
			return PasswordHash.ArgonHashBinary(arg_1C_0, bytes, opsLimit, memLimit, outputLength);
		}

		public static string ArgonHashString(string password, PasswordHash.StrengthArgon limit = PasswordHash.StrengthArgon.Interactive)
		{
			long opsLimit;
			int memLimit;
			switch (limit)
			{
			case PasswordHash.StrengthArgon.Interactive:
				opsLimit = 4L;
				memLimit = 33554432;
				break;
			case PasswordHash.StrengthArgon.Moderate:
				opsLimit = 6L;
				memLimit = 134217728;
				break;
			case PasswordHash.StrengthArgon.Sensitive:
				opsLimit = 8L;
				memLimit = 536870912;
				break;
			default:
				opsLimit = 4L;
				memLimit = 33554432;
				break;
			}
			return PasswordHash.ArgonHashString(password, opsLimit, memLimit);
		}

		public static string ArgonHashString(string password, long opsLimit, int memLimit)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password", "Password cannot be null");
			}
			if (opsLimit < 3L)
			{
				throw new ArgumentOutOfRangeException("opsLimit", "opsLimit the number of passes, has to be at least 3");
			}
			if (memLimit <= 0)
			{
				throw new ArgumentOutOfRangeException("memLimit", "memLimit cannot be zero or negative");
			}
			byte[] array = new byte[128];
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			if (SodiumLibrary.crypto_pwhash_str(array, bytes, (long)bytes.Length, opsLimit, memLimit) != 0)
			{
				throw new OutOfMemoryException("Internal error, hash failed (usually because the operating system refused to allocate the amount of requested memory).");
			}
			return Encoding.UTF8.GetString(array);
		}

		public static bool ArgonHashStringVerify(string hash, string password)
		{
			return PasswordHash.ArgonHashStringVerify(Encoding.UTF8.GetBytes(hash), Encoding.UTF8.GetBytes(password));
		}

		public static bool ArgonHashStringVerify(byte[] hash, byte[] password)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password", "Password cannot be null");
			}
			if (hash == null)
			{
				throw new ArgumentNullException("hash", "Hash cannot be null");
			}
			return SodiumLibrary.crypto_pwhash_str_verify(hash, password, (long)password.Length) == 0;
		}

		public static string ScryptHashString(string password, PasswordHash.Strength limit = PasswordHash.Strength.Interactive)
		{
			long opsLimit;
			int memLimit;
			switch (limit)
			{
			case PasswordHash.Strength.Interactive:
				opsLimit = 524288L;
				memLimit = 16777216;
				break;
			case PasswordHash.Strength.Moderate:
				opsLimit = 8388608L;
				memLimit = 100000000;
				break;
			case PasswordHash.Strength.Medium:
				opsLimit = 8388608L;
				memLimit = 134217728;
				break;
			case PasswordHash.Strength.MediumSlow:
				opsLimit = 33554432L;
				memLimit = 134217728;
				break;
			case PasswordHash.Strength.Sensitive:
				opsLimit = 33554432L;
				memLimit = 1073741824;
				break;
			default:
				opsLimit = 524288L;
				memLimit = 16777216;
				break;
			}
			return PasswordHash.ScryptHashString(password, opsLimit, memLimit);
		}

		public static string ScryptHashString(string password, long opsLimit, int memLimit)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password", "Password cannot be null");
			}
			if (opsLimit <= 0L)
			{
				throw new ArgumentOutOfRangeException("opsLimit", "opsLimit cannot be zero or negative");
			}
			if (memLimit <= 0)
			{
				throw new ArgumentOutOfRangeException("memLimit", "memLimit cannot be zero or negative");
			}
			byte[] array = new byte[102];
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			if (SodiumLibrary.crypto_pwhash_scryptsalsa208sha256_str(array, bytes, (long)bytes.Length, opsLimit, memLimit) != 0)
			{
				throw new OutOfMemoryException("Internal error, hash failed (usually because the operating system refused to allocate the amount of requested memory).");
			}
			return Encoding.UTF8.GetString(array);
		}

		public static byte[] ScryptHashBinary(string password, string salt, PasswordHash.Strength limit = PasswordHash.Strength.Interactive, long outputLength = 32L)
		{
			return PasswordHash.ScryptHashBinary(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt), limit, outputLength);
		}

		public static byte[] ScryptHashBinary(byte[] password, byte[] salt, PasswordHash.Strength limit = PasswordHash.Strength.Interactive, long outputLength = 32L)
		{
			long opsLimit;
			int memLimit;
			switch (limit)
			{
			case PasswordHash.Strength.Interactive:
				opsLimit = 524288L;
				memLimit = 16777216;
				break;
			case PasswordHash.Strength.Moderate:
				opsLimit = 8388608L;
				memLimit = 100000000;
				break;
			case PasswordHash.Strength.Medium:
				opsLimit = 8388608L;
				memLimit = 134217728;
				break;
			case PasswordHash.Strength.MediumSlow:
				opsLimit = 33554432L;
				memLimit = 134217728;
				break;
			case PasswordHash.Strength.Sensitive:
				opsLimit = 33554432L;
				memLimit = 1073741824;
				break;
			default:
				opsLimit = 524288L;
				memLimit = 16777216;
				break;
			}
			return PasswordHash.ScryptHashBinary(password, salt, opsLimit, memLimit, outputLength);
		}

		public static byte[] ScryptHashBinary(string password, string salt, long opsLimit, int memLimit, long outputLength = 32L)
		{
			byte[] arg_1C_0 = Encoding.UTF8.GetBytes(password);
			byte[] bytes = Encoding.UTF8.GetBytes(salt);
			return PasswordHash.ScryptHashBinary(arg_1C_0, bytes, opsLimit, memLimit, outputLength);
		}

		public static byte[] ScryptHashBinary(byte[] password, byte[] salt, long opsLimit, int memLimit, long outputLength = 32L)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password", "Password cannot be null");
			}
			if (salt == null)
			{
				throw new ArgumentNullException("salt", "Salt cannot be null");
			}
			if ((long)salt.Length != 32L)
			{
				throw new SaltOutOfRangeException(string.Format("Salt must be {0} bytes in length.", 32u));
			}
			if (opsLimit <= 0L)
			{
				throw new ArgumentOutOfRangeException("opsLimit", "opsLimit cannot be zero or negative");
			}
			if (memLimit <= 0)
			{
				throw new ArgumentOutOfRangeException("memLimit", "memLimit cannot be zero or negative");
			}
			if (outputLength <= 0L)
			{
				throw new ArgumentOutOfRangeException("outputLength", "OutputLength cannot be zero or negative");
			}
			byte[] array = new byte[outputLength];
			if (SodiumLibrary.crypto_pwhash_scryptsalsa208sha256(array, (long)array.Length, password, (long)password.Length, salt, opsLimit, memLimit) != 0)
			{
				throw new OutOfMemoryException("Internal error, hash failed (usually because the operating system refused to allocate the amount of requested memory).");
			}
			return array;
		}

		public static bool ScryptHashStringVerify(string hash, string password)
		{
			return PasswordHash.ScryptHashStringVerify(Encoding.UTF8.GetBytes(hash), Encoding.UTF8.GetBytes(password));
		}

		public static bool ScryptHashStringVerify(byte[] hash, byte[] password)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password", "Password cannot be null");
			}
			if (hash == null)
			{
				throw new ArgumentNullException("hash", "Hash cannot be null");
			}
			return SodiumLibrary.crypto_pwhash_scryptsalsa208sha256_str_verify(hash, password, (long)password.Length) == 0;
		}
	}
}
