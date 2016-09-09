using Ionic.Zlib;
using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.Logic
{
	internal class ClientHome : Base
	{
		private readonly long m_vId;

		private int m_vRemainingShieldTime;

		private byte[] m_vSerializedVillage;

		public ClientHome() : base(0)
		{
		}

		public ClientHome(long id) : base(0)
		{
			this.m_vId = id;
		}

		public override byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(base.Encode());
			list.AddInt64(this.m_vId);
			list.AddInt32(this.m_vRemainingShieldTime);
			list.AddInt32(1800);
			list.AddInt32(0);
			list.AddInt32(1200);
			list.AddInt32(60);
			list.Add(1);
			list.AddInt32(this.m_vSerializedVillage.Length + 4);
			List<byte> arg_7D_0 = list;
			byte[] expr_6D = new byte[4];
			expr_6D[0] = 255;
			expr_6D[1] = 255;
			arg_7D_0.AddRange(expr_6D);
			list.AddRange(this.m_vSerializedVillage);
			return list.ToArray();
		}

		public byte[] GetHomeJSON()
		{
			return this.m_vSerializedVillage;
		}

		public void SetHomeJSON(string json)
		{
			this.m_vSerializedVillage = ZlibStream.CompressString(json);
		}

		public void SetShieldDurationSeconds(int seconds)
		{
			this.m_vRemainingShieldTime = seconds;
		}
	}
}
