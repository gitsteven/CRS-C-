using Newtonsoft.Json.Linq;
using System;

namespace UCS.Logic
{
	internal class Component
	{
		private readonly GameObject m_vParentGameObject;

		private bool m_vIsEnabled;

		public virtual int Type
		{
			get
			{
				return -1;
			}
		}

		public Component()
		{
		}

		public Component(GameObject go)
		{
			this.m_vIsEnabled = true;
			this.m_vParentGameObject = go;
		}

		public GameObject GetParent()
		{
			return this.m_vParentGameObject;
		}

		public bool IsEnabled()
		{
			return this.m_vIsEnabled;
		}

		public virtual void Load(JObject jsonObject)
		{
		}

		public virtual JObject Save(JObject jsonObject)
		{
			return jsonObject;
		}

		public void SetEnabled(bool status)
		{
			this.m_vIsEnabled = status;
		}

		public virtual void Tick()
		{
		}
	}
}
