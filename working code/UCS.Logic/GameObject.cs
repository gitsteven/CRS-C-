using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows;
using UCS.GameFiles;

namespace UCS.Logic
{
	internal class GameObject
	{
		private readonly List<Component> m_vComponents;

		private readonly Data m_vData;

		private readonly Level m_vLevel;

		public virtual int ClassId
		{
			get
			{
				return -1;
			}
		}

		public int GlobalId
		{
			get;
			set;
		}

		public int X
		{
			get;
			set;
		}

		public int Y
		{
			get;
			set;
		}

		public GameObject(Data data, Level level)
		{
			this.m_vLevel = level;
			this.m_vData = data;
			this.m_vComponents = new List<Component>();
			for (int i = 0; i < 11; i++)
			{
				this.m_vComponents.Add(new Component());
			}
		}

		public void AddComponent(Component c)
		{
			if (this.m_vComponents[c.Type].Type == -1)
			{
				this.m_vComponents[c.Type] = c;
			}
		}

		public Component GetComponent(int index, bool test)
		{
			Component result = null;
			if (!test || this.m_vComponents[index].IsEnabled())
			{
				result = this.m_vComponents[index];
			}
			return result;
		}

		public Data GetData()
		{
			return this.m_vData;
		}

		public Level GetLevel()
		{
			return this.m_vLevel;
		}

		public Vector GetPosition()
		{
			return new Vector((double)this.X, (double)this.Y);
		}

		public virtual bool IsHero()
		{
			return false;
		}

		public void Load(JObject jsonObject)
		{
			this.X = jsonObject["x"].ToObject<int>();
			this.Y = jsonObject["y"].ToObject<int>();
			using (List<Component>.Enumerator enumerator = this.m_vComponents.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					enumerator.Current.Load(jsonObject);
				}
			}
		}

		public JObject Save(JObject jsonObject)
		{
			jsonObject.Add("x", this.X);
			jsonObject.Add("y", this.Y);
			using (List<Component>.Enumerator enumerator = this.m_vComponents.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					enumerator.Current.Save(jsonObject);
				}
			}
			return jsonObject;
		}

		public void SetPositionXY(int newX, int newY)
		{
			this.X = newX;
			this.Y = newY;
		}

		public virtual void Tick()
		{
			foreach (Component current in this.m_vComponents)
			{
				if (current.IsEnabled())
				{
					current.Tick();
				}
			}
		}
	}
}
