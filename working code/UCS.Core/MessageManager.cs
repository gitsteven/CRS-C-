using System;
using System.Collections.Concurrent;
using System.Threading;
using UCS.Logic;
using UCS.PacketProcessing;

namespace UCS.Core
{
	internal class MessageManager
	{
		private delegate void PacketProcessingDelegate();

		private static ConcurrentQueue<Message> m_vPackets;

		private static readonly EventWaitHandle m_vWaitHandle = new AutoResetEvent(false);

		private bool m_vIsRunning;

		public MessageManager()
		{
			MessageManager.m_vPackets = new ConcurrentQueue<Message>();
			this.m_vIsRunning = false;
		}

		public void Start()
		{
			new MessageManager.PacketProcessingDelegate(this.PacketProcessing).BeginInvoke(null, null);
			this.m_vIsRunning = true;
			Console.WriteLine("[CRS]    Message manager has been successfully started !");
		}

		private void PacketProcessing()
		{
			while (this.m_vIsRunning)
			{
				MessageManager.m_vWaitHandle.WaitOne();
				Message message;
				while (MessageManager.m_vPackets.TryDequeue(out message))
				{
					Level level = message.Client.GetLevel();
					string str = "(0, NoNameYet)";
					if (level != null)
					{
						str = string.Concat(new object[]
						{
							" (",
							level.GetPlayerAvatar().GetId(),
							", ",
							level.GetPlayerAvatar().GetAvatarName(),
							")"
						});
					}
					try
					{
						Debugger.WriteLine("[CRS]    Processing message " + message.GetType().Name + str, null, 4);
						message.Decode();
						message.Process(level);
					}
					catch (Exception ex)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Debugger.WriteLine("[CRS]    An exception occured during processing of message " + message.GetType().Name + str, ex, 4);
						Console.ResetColor();
					}
				}
			}
		}

		public static void ProcessPacket(Message p)
		{
			MessageManager.m_vPackets.Enqueue(p);
			MessageManager.m_vWaitHandle.Set();
		}
	}
}
