using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using UCS.Logic;
using UCS.Utilities.Sodium;

namespace UCS.PacketProcessing
{
	internal class Client
	{
		private readonly long m_vSocketHandle;

		private Level m_vLevel;

		public int ClientSeed
		{
			get;
			set;
		}

		public byte[] CPublicKey
		{
			get;
			set;
		}

		public byte[] CSessionKey
		{
			get;
			set;
		}

		public byte[] CSNonce
		{
			get;
			set;
		}

		public int CState
		{
			get;
			set;
		}

		public string CIPAddress
		{
			get;
			set;
		}

		public byte[] CRNonce
		{
			get;
			set;
		}

		public byte[] CSharedKey
		{
			get;
			set;
		}

		public List<byte> DataStream
		{
			get;
			set;
		}

		public byte[] IncomingPacketsKey
		{
			get;
			set;
		}

		public byte[] OutgoingPacketsKey
		{
			get;
			set;
		}

		public Socket Socket
		{
			get;
			set;
		}

		public Client(Socket so)
		{
			this.Socket = so;
			this.m_vSocketHandle = so.Handle.ToInt64();
			this.DataStream = new List<byte>();
			this.CState = 0;
		}

		public static ClashKeyPair GenerateKeyPair()
		{
			KeyPair keyPair = PublicKeyBox.GenerateKeyPair();
			return new ClashKeyPair(keyPair.PublicKey, keyPair.PrivateKey);
		}

		public static byte[] GenerateSessionKey()
		{
			return PublicKeyBox.GenerateNonce();
		}

		public Level GetLevel()
		{
			return this.m_vLevel;
		}

		public long GetSocketHandle()
		{
			return this.m_vSocketHandle;
		}

		public bool IsClientSocketConnected()
		{
			bool result;
			try
			{
				result = ((!this.Socket.Poll(1000, SelectMode.SelectRead) || this.Socket.Available != 0) && this.Socket.Connected);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public void SetLevel(Level l)
		{
			this.m_vLevel = l;
		}

		public bool TryGetPacket(out Message p)
		{
			p = null;
			bool result = false;
			if (this.DataStream.Count<byte>() >= 5)
			{
				int num = 0 | (int)this.DataStream[2] << 16 | (int)this.DataStream[3] << 8 | (int)this.DataStream[4];
				ushort packetType = (ushort)((int)this.DataStream[0] << 8 | (int)this.DataStream[1]);
				if (this.DataStream.Count - 7 >= num)
				{
					object obj = null;
					using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(this.DataStream.Take(7 + num).ToArray<byte>())))
					{
						obj = MessageFactory.Read(this, binaryReader, (int)packetType);
					}
					if (obj != null)
					{
						p = (Message)obj;
						result = true;
					}
					else
					{
						this.DataStream.Skip(7).Take(num).ToArray<byte>();
					}
					this.DataStream.RemoveRange(0, 7 + num);
				}
			}
			return result;
		}
	}
}
