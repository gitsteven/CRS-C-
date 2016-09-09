using System;
using System.IO;
using System.Text;

namespace UCS.PacketProcessing
{
	public class MessageWriter : BinaryWriter
	{
		private bool _disposed;

		public MessageWriter()
		{
		}

		public MessageWriter(Stream output) : base(output)
		{
		}

		public override void Write(decimal value)
		{
			this.CheckDispose();
			this.Write((double)value);
		}

		public override void Write(double value)
		{
			this.CheckDispose();
			byte[] bytes = BitConverter.GetBytes(value);
			this.WriteByteArrayEndian(bytes);
		}

		public override void Write(long value)
		{
			this.CheckDispose();
			this.Write((ulong)value);
		}

		public override void Write(ulong value)
		{
			this.CheckDispose();
			byte[] bytes = BitConverter.GetBytes(value);
			this.WriteByteArrayEndian(bytes);
		}

		public override void Write(float value)
		{
			this.CheckDispose();
			byte[] bytes = BitConverter.GetBytes(value);
			this.WriteByteArrayEndian(bytes);
		}

		public override void Write(int value)
		{
			this.CheckDispose();
			this.Write((uint)value);
		}

		public override void Write(uint value)
		{
			this.CheckDispose();
			byte[] bytes = BitConverter.GetBytes(value);
			this.WriteByteArrayEndian(bytes);
		}

		public override void Write(short value)
		{
			this.CheckDispose();
			this.Write((ushort)value);
		}

		public override void Write(ushort value)
		{
			this.CheckDispose();
			byte[] bytes = BitConverter.GetBytes(value);
			this.WriteByteArrayEndian(bytes);
		}

		public override void Write(string value)
		{
			this.CheckDispose();
			if (value == null)
			{
				this.Write(-1);
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			this.Write(bytes.Length);
			this.Write(bytes);
		}

		public override void Write(bool value)
		{
			this.CheckDispose();
			base.Write(value);
		}

		public void Write(byte[] buffer, bool prefixed)
		{
			this.CheckDispose();
			this.Write(buffer, 0, buffer.Length, prefixed);
		}

		public void Write(byte[] buffer, int index, int count, bool prefixed)
		{
			this.CheckDispose();
			if (!prefixed)
			{
				this.Write(buffer, index, count);
				return;
			}
			this.Write(buffer.Length);
			this.Write(buffer, index, count);
		}

		public new void Dispose()
		{
			this.Dispose(true);
			this._disposed = true;
		}

		private void CheckDispose()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null, "Cannot access the MessageWriter object because it was disposed.");
			}
		}

		private void WriteByteArrayEndian(byte[] buffer)
		{
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(buffer);
			}
			this.Write(buffer);
		}
	}
}
