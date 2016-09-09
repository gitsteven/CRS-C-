using System;
using System.Security.Cryptography;
using UCS.Utilities.Sodium.Exceptions;

namespace UCS.Utilities.Sodium
{
	public class KeyPair : IDisposable
	{
		private readonly byte[] _privateKey;

		public byte[] PublicKey
		{
			get;
			set;
		}

		public byte[] PrivateKey
		{
			get
			{
				this._UnprotectKey();
				byte[] array = new byte[this._privateKey.Length];
				Array.Copy(this._privateKey, array, array.Length);
				this._ProtectKey();
				return array;
			}
		}

		public KeyPair(byte[] publicKey, byte[] privateKey)
		{
			if (privateKey.Length % 16 != 0)
			{
				throw new KeyOutOfRangeException("Private Key length must be a multiple of 16 bytes.");
			}
			this.PublicKey = publicKey;
			this._privateKey = privateKey;
			this._ProtectKey();
		}

		public void Dispose()
		{
			if (this._privateKey != null && this._privateKey.Length != 0)
			{
				Array.Clear(this._privateKey, 0, this._privateKey.Length);
			}
		}

		~KeyPair()
		{
			this.Dispose();
		}

		private void _ProtectKey()
		{
			if (!SodiumLibrary.IsRunningOnMono)
			{
				ProtectedMemory.Protect(this._privateKey, MemoryProtectionScope.SameProcess);
			}
		}

		private void _UnprotectKey()
		{
			if (!SodiumLibrary.IsRunningOnMono)
			{
				ProtectedMemory.Unprotect(this._privateKey, MemoryProtectionScope.SameProcess);
			}
		}
	}
}
