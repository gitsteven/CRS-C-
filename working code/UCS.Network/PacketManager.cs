using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;
using UCS.Core;
using UCS.PacketProcessing;

namespace UCS.Network
{
	internal class PacketManager : IDisposable
	{
		private delegate void IncomingProcessingDelegate();

		private delegate void OutgoingProcessingDelegate();

		private static readonly EventWaitHandle m_vIncomingWaitHandle = new AutoResetEvent(false);

		private static readonly EventWaitHandle m_vOutgoingWaitHandle = new AutoResetEvent(false);

		private static ConcurrentQueue<Message> m_vIncomingPackets;

		private static ConcurrentQueue<Message> m_vOutgoingPackets;

		private bool m_vIsRunning;

		public PacketManager()
		{
			PacketManager.m_vIncomingPackets = new ConcurrentQueue<Message>();
			PacketManager.m_vOutgoingPackets = new ConcurrentQueue<Message>();
			this.m_vIsRunning = false;
		}

		public void Dispose()
		{
			PacketManager.m_vIncomingWaitHandle.Dispose();
			GC.SuppressFinalize(this);
			PacketManager.m_vOutgoingWaitHandle.Dispose();
		}

		public static void ProcessIncomingPacket(Message p)
		{
			PacketManager.m_vIncomingPackets.Enqueue(p);
			PacketManager.m_vIncomingWaitHandle.Set();
		}

		public static void ProcessOutgoingPacket(Message p)
		{
			p.Encode();
			try
			{
				PacketManager.m_vOutgoingPackets.Enqueue(p);
				PacketManager.m_vOutgoingWaitHandle.Set();
			}
			catch (Exception)
			{
			}
		}

		public void Start()
		{
			new PacketManager.IncomingProcessingDelegate(this.IncomingProcessing).BeginInvoke(null, null);
			new PacketManager.OutgoingProcessingDelegate(this.OutgoingProcessing).BeginInvoke(null, null);
			this.m_vIsRunning = true;
			Console.WriteLine("[CRS]    Packet Manager started successfully");
		}

		private void IncomingProcessing()
		{
			while (this.m_vIsRunning)
			{
				PacketManager.m_vIncomingWaitHandle.WaitOne();
				Message message;
				while (PacketManager.m_vIncomingPackets.TryDequeue(out message))
				{
					message.GetData();
					Logger.WriteLine(message, "R", 4);
					MessageManager.ProcessPacket(message);
				}
			}
		}

		private void OutgoingProcessing()
		{
			while (this.m_vIsRunning)
			{
				PacketManager.m_vOutgoingWaitHandle.WaitOne();
				Message message;
				while (PacketManager.m_vOutgoingPackets.TryDequeue(out message))
				{
					Logger.WriteLine(message, "S", 4);
					try
					{
						if (message.Client.Socket != null)
						{
							message.Client.Socket.Send(message.GetRawData());
						}
						else
						{
							ResourcesManager.DropClient(message.Client.GetSocketHandle());
						}
					}
					catch (Exception)
					{
						try
						{
							ResourcesManager.DropClient(message.Client.GetSocketHandle());
							message.Client.Socket.Shutdown(SocketShutdown.Both);
							message.Client.Socket.Close();
						}
						catch (Exception ex)
						{
							Debugger.WriteLine("[CRS]    Exception thrown when dropping client : ", ex, 4);
						}
					}
				}
			}
		}
	}
}
