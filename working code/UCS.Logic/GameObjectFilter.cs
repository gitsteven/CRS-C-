using System;
using System.Collections.Generic;

namespace UCS.Logic
{
	internal class GameObjectFilter
	{
		private List<int> m_vIgnoredObjects;

		public void AddIgnoreObject(GameObject go)
		{
			if (this.m_vIgnoredObjects == null)
			{
				this.m_vIgnoredObjects = new List<int>();
			}
			this.m_vIgnoredObjects.Add(go.GlobalId);
		}

		public virtual bool IsComponentFilter()
		{
			return false;
		}

		public void RemoveAllIgnoreObjects()
		{
			if (this.m_vIgnoredObjects != null)
			{
				this.m_vIgnoredObjects.Clear();
				this.m_vIgnoredObjects = null;
			}
		}

		public bool TestGameObject(GameObject go)
		{
			bool result = true;
			if (this.m_vIgnoredObjects != null)
			{
				result = (this.m_vIgnoredObjects.IndexOf(go.GlobalId) == -1);
			}
			return result;
		}
	}
}
