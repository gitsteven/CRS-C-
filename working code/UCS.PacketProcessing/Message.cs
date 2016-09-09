using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UCS.Helpers;
using UCS.Logic;
using UCS.Utilities.Sodium;

namespace UCS.PacketProcessing
{
	internal class Message
	{
		private byte[] m_vData;

		private int m_vLength;

		private ushort m_vMessageVersion;

		private ushort m_vType;

		public int Broadcasting
		{
			get;
			set;
		}

		public Client Client
		{
			get;
			set;
		}

		public Message()
		{
		}

		public Message(Client c)
		{
			this.Client = c;
			this.m_vType = 0;
			this.m_vLength = -1;
			this.m_vMessageVersion = 0;
			this.m_vData = null;
		}

		public Message(Client c, BinaryReader br)
		{
			this.Client = c;
			this.m_vType = br.ReadUInt16WithEndian();
			byte[] array = br.ReadBytes(3);
			this.m_vLength = (0 | (int)array[0] << 16 | (int)array[1] << 8 | (int)array[2]);
			this.m_vMessageVersion = br.ReadUInt16WithEndian();
			this.m_vData = br.ReadBytes(this.m_vLength);
		}

		public void Encrypt(byte[] plainText)
		{
			try
			{
				if (this.GetMessageType() == 20103)
				{
					byte[] nonce = GenericHash.Hash(this.Client.CSNonce.Concat(this.Client.CPublicKey).Concat(Key.Crypto.PublicKey).ToArray<byte>(), null, 24);
					plainText = this.Client.CRNonce.Concat(this.Client.CSharedKey).Concat(plainText).ToArray<byte>();
					this.SetData(PublicKeyBox.Create(plainText, nonce, Key.Crypto.PrivateKey, this.Client.CPublicKey));
				}
				else if (this.GetMessageType() == 20104)
				{
					byte[] nonce2 = GenericHash.Hash(this.Client.CSNonce.Concat(this.Client.CPublicKey).Concat(Key.Crypto.PublicKey).ToArray<byte>(), null, 24);
					plainText = this.Client.CRNonce.Concat(this.Client.CSharedKey).Concat(plainText).ToArray<byte>();
					this.SetData(PublicKeyBox.Create(plainText, nonce2, Key.Crypto.PrivateKey, this.Client.CPublicKey));
					this.Client.CState = 2;
				}
				else
				{
					this.Client.CRNonce = Sodium.Utilities.Increment(Sodium.Utilities.Increment(this.Client.CRNonce));
					this.SetData(SecretBox.Create(plainText, this.Client.CRNonce, this.Client.CSharedKey).Skip(16).ToArray<byte>());
				}
			}
			catch (Exception)
			{
				this.Client.CState = 0;
			}
		}

		public void UpdateEncrypt()
		{
			this.Client.CRNonce = Sodium.Utilities.Increment(Sodium.Utilities.Increment(this.Client.CRNonce));
		}

		public void UpdateDecrypt()
		{
			this.Client.CSNonce = Sodium.Utilities.Increment(Sodium.Utilities.Increment(this.Client.CSNonce));
		}

		public void Decrypt()
		{
			try
			{
				if (this.m_vType == 10101)
				{
					byte[] array = this.m_vData;
					this.Client.CPublicKey = array.Take(32).ToArray<byte>();
					this.Client.CSharedKey = this.Client.CPublicKey;
					this.Client.CRNonce = Client.GenerateSessionKey();
					byte[] nonce = GenericHash.Hash(this.Client.CPublicKey.Concat(Key.Crypto.PublicKey).ToArray<byte>(), null, 24);
					array = array.Skip(32).ToArray<byte>();
					byte[] source = PublicKeyBox.Open(array, nonce, Key.Crypto.PrivateKey, this.Client.CPublicKey);
					this.Client.CSessionKey = source.Take(24).ToArray<byte>();
					this.Client.CSNonce = source.Skip(24).Take(24).ToArray<byte>();
					this.SetData(source.Skip(24).Skip(24).ToArray<byte>());
				}
				else
				{
					this.Client.CSNonce = Sodium.Utilities.Increment(Sodium.Utilities.Increment(this.Client.CSNonce));
					this.SetData(SecretBox.Open(new byte[16].Concat(this.m_vData).ToArray<byte>(), this.Client.CSNonce, this.Client.CSharedKey));
				}
			}
			catch (Exception)
			{
				this.Client.CState = 0;
			}
		}

		public virtual void Decode()
		{
		}

		public virtual void Encode()
		{
		}

		public byte[] GetData()
		{
			return this.m_vData;
		}

		public int GetLength()
		{
			return this.m_vLength;
		}

		public ushort GetMessageType()
		{
			return this.m_vType;
		}

		public ushort GetMessageVersion()
		{
			return this.m_vMessageVersion;
		}

		public byte[] GetRawData()
		{
			List<byte> expr_05 = new List<byte>();
			expr_05.AddRange(BitConverter.GetBytes(this.m_vType).Reverse<byte>());
			expr_05.AddRange(BitConverter.GetBytes(this.m_vLength).Reverse<byte>().Skip(1));
			expr_05.AddRange(BitConverter.GetBytes(this.m_vMessageVersion).Reverse<byte>());
			expr_05.AddRange(this.m_vData);
			return expr_05.ToArray();
		}

		public virtual void Process(Level level)
		{
		}

		public void SetData(byte[] data)
		{
			this.m_vData = data;
			this.m_vLength = data.Length;
		}

		public void SetMessageType(ushort type)
		{
			this.m_vType = type;
		}

		public void SetMessageVersion(ushort v)
		{
			this.m_vMessageVersion = v;
		}

		public string ToHexString()
		{
			return BitConverter.ToString(this.m_vData).Replace("-", " ");
		}

		public override string ToString()
		{
			return Encoding.UTF8.GetString(this.m_vData, 0, this.m_vLength);
		}

		public static byte[] AddVInt(int v2)
		{
			MemoryStream memoryStream = new MemoryStream(5);
			if (v2 <= -1)
			{
				if (v2 + 63 < 0)
				{
					memoryStream.WriteByte((byte)((v2 & 63) | 64));
					return memoryStream.ToArray();
				}
				if (v2 >= -8191)
				{
					memoryStream.WriteByte((byte)(v2 | 192));
					v2 >>= 6;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				if (v2 >= -1048575)
				{
					memoryStream.WriteByte((byte)(v2 | 192));
					memoryStream.WriteByte((byte)(v2 >> 6 | 128));
					v2 >>= 13;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				memoryStream.WriteByte((byte)(v2 | 192));
				memoryStream.WriteByte((byte)(v2 >> 6 | 128));
				memoryStream.WriteByte((byte)(v2 >> 13 | 128));
				v2 >>= 20;
				if (v2 <= -134217728)
				{
					memoryStream.WriteByte((byte)(v2 | 128));
					v2 >>= 11;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				memoryStream.WriteByte((byte)(v2 & 127));
				return memoryStream.ToArray();
			}
			else
			{
				if (v2 <= 63)
				{
					v2 &= 63;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				if (v2 < 8192)
				{
					memoryStream.WriteByte((byte)((v2 & 63) | 128));
					v2 >>= 6;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				if (v2 < 1048576)
				{
					memoryStream.WriteByte((byte)((v2 & 63) | 128));
					memoryStream.WriteByte((byte)(v2 >> 6 | 128));
					v2 >>= 13;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				memoryStream.WriteByte((byte)((v2 & 63) | 128));
				memoryStream.WriteByte((byte)(v2 >> 6 | 128));
				memoryStream.WriteByte((byte)(v2 >> 13 | 128));
				v2 >>= 20;
				if (v2 >= 134217728)
				{
					memoryStream.WriteByte((byte)(v2 | 128));
					v2 >>= 11;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				memoryStream.WriteByte((byte)(v2 & 127));
				return memoryStream.ToArray();
			}
		}
	}
}
