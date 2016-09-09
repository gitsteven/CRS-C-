using System;
using System.Collections.Generic;
using UCS.Logic;

namespace UCS.GameFiles
{
	internal class DataTables
	{
		private readonly List<DataTable> m_vDataTables;

		public DataTables()
		{
			this.m_vDataTables = new List<DataTable>();
			for (int i = 0; i < 41; i++)
			{
				this.m_vDataTables.Add(new DataTable());
			}
		}

		public Data GetDataById(int id)
		{
			int index = GlobalID.GetClassID(id) - 1;
			return this.m_vDataTables[index].GetItemById(id);
		}

		public DataTable GetTable(int i)
		{
			return this.m_vDataTables[i];
		}

		public void InitDataTable(CSVTable t, int index)
		{
			this.m_vDataTables[index] = new DataTable(t, index);
		}
	}
}
