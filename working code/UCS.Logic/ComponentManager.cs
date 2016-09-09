using System;
using System.Collections.Generic;
using System.Windows;

namespace UCS.Logic
{
	internal class ComponentManager
	{
		private readonly List<List<Component>> m_vComponents;

		private readonly Level m_vLevel;

		public ComponentManager(Level l)
		{
			this.m_vComponents = new List<List<Component>>();
			for (int i = 0; i <= 10; i++)
			{
				this.m_vComponents.Add(new List<Component>());
			}
			this.m_vLevel = l;
		}

		public void AddComponent(Component c)
		{
			this.m_vComponents[c.Type].Add(c);
		}

		public Component GetClosestComponent(int x, int y, ComponentFilter cf)
		{
			Component component = null;
			int type = cf.Type;
			List<Component> list = this.m_vComponents[type];
			Vector vector = new Vector((double)x, (double)y);
			double num = 0.0;
			if (list.Count > 0)
			{
				foreach (Component current in list)
				{
					if (cf.TestComponent(current))
					{
						GameObject parent = current.GetParent();
						double lengthSquared = (vector - parent.GetPosition()).LengthSquared;
						if (lengthSquared < num || component == null)
						{
							num = lengthSquared;
							component = current;
						}
					}
				}
			}
			return component;
		}

		public List<Component> GetComponents(int type)
		{
			return this.m_vComponents[type];
		}

		public void RemoveGameObjectReferences(GameObject go)
		{
			foreach (List<Component> current in this.m_vComponents)
			{
				List<Component> list = new List<Component>();
				foreach (Component current2 in current)
				{
					if (current2.GetParent() == go)
					{
						list.Add(current2);
					}
				}
				foreach (Component current3 in list)
				{
					current.Remove(current3);
				}
			}
		}

		public void Tick()
		{
		}
	}
}
