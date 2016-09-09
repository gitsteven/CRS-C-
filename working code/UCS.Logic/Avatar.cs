using System;
using System.Collections.Generic;
using UCS.GameFiles;

namespace UCS.Logic
{
	internal class Avatar
	{
		protected List<DataSlot> m_vHeroHealth;

		protected List<DataSlot> m_vHeroState;

		protected List<DataSlot> m_vHeroUpgradeLevel;

		protected List<DataSlot> m_vResourceCaps;

		protected List<DataSlot> m_vResources;

		protected List<DataSlot> m_vSpellCount;

		protected List<DataSlot> m_vSpellUpgradeLevel;

		protected List<DataSlot> m_vUnitCount;

		protected List<DataSlot> m_vUnitUpgradeLevel;

		public Avatar()
		{
			this.m_vResources = new List<DataSlot>();
			this.m_vResourceCaps = new List<DataSlot>();
			this.m_vUnitCount = new List<DataSlot>();
			this.m_vUnitUpgradeLevel = new List<DataSlot>();
			this.m_vHeroHealth = new List<DataSlot>();
			this.m_vHeroUpgradeLevel = new List<DataSlot>();
			this.m_vHeroState = new List<DataSlot>();
			this.m_vSpellCount = new List<DataSlot>();
			this.m_vSpellUpgradeLevel = new List<DataSlot>();
		}

		public static int GetDataIndex(List<DataSlot> dsl, Data d)
		{
			return dsl.FindIndex((DataSlot ds) => ds.Data == d);
		}

		public List<DataSlot> GetResourceCaps()
		{
			return this.m_vResourceCaps;
		}

		public List<DataSlot> GetResources()
		{
			return this.m_vResources;
		}

		public List<DataSlot> GetSpells()
		{
			return this.m_vSpellCount;
		}

		public List<DataSlot> GetUnits()
		{
			return this.m_vUnitCount;
		}
	}
}
