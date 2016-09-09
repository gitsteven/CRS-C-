using System;
using System.Collections.Generic;
using System.IO;
using UCS.Helpers;

namespace UCS.Logic
{
	internal class Base
	{
		private int m_vUnknown1;

		public Base(int unknown1)
		{
			this.m_vUnknown1 = unknown1;
		}

		public virtual void Decode(byte[] baseData)
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(baseData)))
			{
				this.m_vUnknown1 = binaryReader.ReadInt32WithEndian();
			}
		}

		public virtual byte[] Encode()
		{
			List<byte> expr_05 = new List<byte>();
			expr_05.AddInt32(this.m_vUnknown1);
			return expr_05.ToArray();
		}
	}
}
