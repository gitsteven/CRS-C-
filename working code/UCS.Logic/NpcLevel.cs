using System;

namespace UCS.Logic
{
	internal class NpcLevel
	{
		private const int m_vType = 17000000;

		public int Id
		{
			get
			{
				return 17000000 + this.Index;
			}
		}

		public int Index
		{
			get;
			set;
		}

		public int LootedElixir
		{
			get;
			set;
		}

		public int LootedGold
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public int Stars
		{
			get;
			set;
		}

		public NpcLevel()
		{
		}

		public NpcLevel(int index)
		{
			this.Index = index;
			this.Stars = 0;
			this.LootedGold = 0;
			this.LootedElixir = 0;
		}
	}
}
