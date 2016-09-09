using System;
using UCS.Logic;

namespace UCS.PacketProcessing
{
	internal class GameOpCommand
	{
		private byte m_vRequiredAccountPrivileges;

		public virtual void Execute(Level level)
		{
		}

		public byte GetRequiredAccountPrivileges()
		{
			return this.m_vRequiredAccountPrivileges;
		}

		public static void SendCommandFailedMessage(Client c)
		{
		}

		public void SetRequiredAccountPrivileges(byte level)
		{
			this.m_vRequiredAccountPrivileges = level;
		}
	}
}
