using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace UCS.Logic
{
	internal class GameObjectManager
	{
		private readonly ComponentManager m_vComponentManager;

		private readonly List<List<GameObject>> m_vGameObjects;

		private readonly List<int> m_vGameObjectsIndex;

		private readonly Level m_vLevel;

		public GameObjectManager(Level l)
		{
			this.m_vLevel = l;
			this.m_vGameObjects = new List<List<GameObject>>();
			this.m_vGameObjectsIndex = new List<int>();
			for (int i = 0; i < 7; i++)
			{
				this.m_vGameObjects.Add(new List<GameObject>());
				this.m_vGameObjectsIndex.Add(0);
			}
			this.m_vComponentManager = new ComponentManager(this.m_vLevel);
		}

		public void Load(JObject jsonObject)
		{
		}

		public void RemoveGameObject(GameObject go)
		{
		}

		public JObject Save()
		{
			return new JObject();
		}

		public void Tick()
		{
		}
	}
}
