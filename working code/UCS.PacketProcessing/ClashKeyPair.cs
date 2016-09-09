using System;
using UCS.Utilities.Sodium;

namespace UCS.PacketProcessing
{
	public class ClashKeyPair : IDisposable
	{
		public const int KeyLength = 32;

		public const int NonceLength = 24;

		private readonly KeyPair _keyPair;

		private bool _disposed;

		public byte[] PrivateKey
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(null, "Cannot access CoCKeyPair object because it was disposed.");
				}
				return this._keyPair.PrivateKey;
			}
		}

		public byte[] PublicKey
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(null, "Cannot access CoCKeyPair object because it was disposed.");
				}
				return this._keyPair.PublicKey;
			}
		}

		public ClashKeyPair(byte[] publicKey, byte[] privateKey)
		{
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			if (publicKey.Length != 32)
			{
				throw new ArgumentOutOfRangeException("publicKey", "publicKey must be 32 bytes in length.");
			}
			if (privateKey == null)
			{
				throw new ArgumentNullException("privateKey");
			}
			if (privateKey.Length != 32)
			{
				throw new ArgumentOutOfRangeException("privateKey", "publicKey must be 32 bytes in length.");
			}
			this._keyPair = new KeyPair(publicKey, privateKey);
		}

		public void Dispose()
		{
			if (this._disposed)
			{
				return;
			}
			this._keyPair.Dispose();
			this._disposed = true;
			GC.SuppressFinalize(this);
		}
	}
}
