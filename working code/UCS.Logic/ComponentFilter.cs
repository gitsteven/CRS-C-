using System;

namespace UCS.Logic
{
	internal class ComponentFilter : GameObjectFilter
	{
		public int Type;

		public ComponentFilter(int type)
		{
			this.Type = type;
		}

		public override bool IsComponentFilter()
		{
			return true;
		}

		public bool TestComponent(Component c)
		{
			GameObject parent = c.GetParent();
			return this.TestGameObject(parent);
		}

		public new bool TestGameObject(GameObject go)
		{
			bool result = false;
			if (go.GetComponent(this.Type, true) != null)
			{
				result = base.TestGameObject(go);
			}
			return result;
		}
	}
}
