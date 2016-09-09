using System;
using System.Net.Sockets;

namespace UCS.Network
{
	public class SocketRead
	{
		public delegate void IncomingReadErrorHandler(SocketRead read, Exception exception);

		public delegate void IncomingReadHandler(SocketRead read, byte[] data);

		public const int kBufferSize = 256;

		private readonly byte[] buffer = new byte[256];

		private readonly SocketRead.IncomingReadErrorHandler errorHandler;

		private readonly SocketRead.IncomingReadHandler readHandler;

		public Socket Socket
		{
			get;
			set;
		}

		private SocketRead(Socket socket, SocketRead.IncomingReadHandler readHandler, SocketRead.IncomingReadErrorHandler errorHandler = null)
		{
			this.Socket = socket;
			this.readHandler = readHandler;
			this.errorHandler = errorHandler;
			this.BeginReceive();
		}

		public static SocketRead Begin(Socket socket, SocketRead.IncomingReadHandler readHandler, SocketRead.IncomingReadErrorHandler errorHandler = null)
		{
			return new SocketRead(socket, readHandler, errorHandler);
		}

		private void BeginReceive()
		{
			this.Socket.BeginReceive(this.buffer, 0, 256, SocketFlags.None, new AsyncCallback(this.OnReceive), this);
		}

		private void OnReceive(IAsyncResult result)
		{
			try
			{
				if (result.IsCompleted)
				{
					int num = this.Socket.EndReceive(result);
					if (num > 0)
					{
						byte[] array = new byte[num];
						Array.Copy(this.buffer, 0, array, 0, num);
						this.readHandler(this, array);
						SocketRead.Begin(this.Socket, this.readHandler, this.errorHandler);
					}
				}
			}
			catch (Exception exception)
			{
				if (this.errorHandler != null)
				{
					this.errorHandler(this, exception);
				}
			}
		}
	}
}
