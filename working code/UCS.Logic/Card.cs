using System;
using System.Collections.Generic;

namespace UCS.Logic
{
	internal class Card
	{
		private int m_vCardId;

		private int m_vCount;

		private bool m_vIsNew;

		private int m_vLevel;

		public void SetIsNew(bool New)
		{
			this.m_vIsNew = New;
		}

		public bool IsNew()
		{
			return this.m_vIsNew;
		}

		public void SetCardId(int Id)
		{
			this.m_vCardId = Id;
		}

		public void SetLevel(int Level)
		{
			this.m_vLevel = Level;
		}

		public void SetCount(int Count)
		{
			this.m_vCount = Count;
		}

		public int GetLevel()
		{
			return this.m_vLevel;
		}

		public int GetCardId()
		{
			return this.m_vCardId;
		}

		public int GetCount()
		{
			return this.m_vCount;
		}

		public byte[] Encode()
		{
			return new List<byte>
			{
				26,
				(byte)this.m_vCardId,
				(byte)this.m_vLevel,
				0,
				(byte)this.m_vCount,
				0,
				0,
				this.m_vIsNew ? (byte)1 : (byte)0
			}.ToArray();
		}
	}
}
