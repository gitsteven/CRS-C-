using System;
using System.Collections.Generic;
using UCS.Core;

namespace UCS.Logic
{
	internal class ResourceStorageComponent : Component
	{
		private readonly List<int> m_vCurrentResources;

		private readonly List<int> m_vStolenResources;

		private List<int> m_vMaxResources;

		public override int Type
		{
			get
			{
				return 6;
			}
		}

		public ResourceStorageComponent(GameObject go) : base(go)
		{
			this.m_vCurrentResources = new List<int>();
			this.m_vMaxResources = new List<int>();
			this.m_vStolenResources = new List<int>();
			int itemCount = ObjectManager.DataTables.GetTable(2).GetItemCount();
			for (int i = 0; i < itemCount; i++)
			{
				this.m_vCurrentResources.Add(0);
				this.m_vMaxResources.Add(0);
				this.m_vStolenResources.Add(0);
			}
		}

		public int GetCount(int resourceIndex)
		{
			return this.m_vCurrentResources[resourceIndex];
		}

		public int GetMax(int resourceIndex)
		{
			return this.m_vMaxResources[resourceIndex];
		}

		public void SetMaxArray(List<int> resourceCaps)
		{
			this.m_vMaxResources = resourceCaps;
		}
	}
}
