using System;
using System.Collections.Generic;
using UCS.Logic;

namespace UCS.GameFiles
{
	internal class DataTable
	{
		protected List<Data> m_vData;

		protected int m_vIndex;

		public DataTable()
		{
			this.m_vIndex = 0;
			this.m_vData = new List<Data>();
		}

		public DataTable(CSVTable table, int index)
		{
			this.m_vIndex = index;
			this.m_vData = new List<Data>();
			for (int i = 0; i < table.GetRowCount(); i++)
			{
				CSVRow rowAt = table.GetRowAt(i);
				Data item = this.CreateItem(rowAt);
				this.m_vData.Add(item);
			}
		}

		public Data CreateItem(CSVRow row)
		{
			Data result = new Data(row, this);
			int vIndex = this.m_vIndex;
			if (vIndex != 23)
			{
				if (vIndex == 24)
				{
					result = new Data(row, this);
				}
			}
			else
			{
				result = new Data(row, this);
			}
			return result;
		}

		public Data GetDataByName(string name)
		{
			return this.m_vData.Find((Data d) => d.GetName() == name);
		}

		public Data GetItemAt(int index)
		{
			return this.m_vData[index];
		}

		public Data GetItemById(int id)
		{
			int instanceID = GlobalID.GetInstanceID(id);
			return this.m_vData[instanceID];
		}

		public int GetItemCount()
		{
			return this.m_vData.Count;
		}

		public int GetTableIndex()
		{
			return this.m_vIndex;
		}
	}
}
