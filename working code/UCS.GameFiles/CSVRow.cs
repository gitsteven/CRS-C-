using System;

namespace UCS.GameFiles
{
	internal class CSVRow
	{
		private readonly CSVTable m_vCSVTable;

		private readonly int m_vRowStart;

		public CSVRow(CSVTable table)
		{
			this.m_vCSVTable = table;
			this.m_vRowStart = this.m_vCSVTable.GetColumnRowCount();
			this.m_vCSVTable.AddRow(this);
		}

		public int GetArraySize(string name)
		{
			int columnIndexByName = this.m_vCSVTable.GetColumnIndexByName(name);
			int result = 0;
			if (columnIndexByName != -1)
			{
				result = this.m_vCSVTable.GetArraySizeAt(this, columnIndexByName);
			}
			return result;
		}

		public string GetName()
		{
			return this.m_vCSVTable.GetValueAt(0, this.m_vRowStart);
		}

		public int GetRowOffset()
		{
			return this.m_vRowStart;
		}

		public string GetValue(string name, int level)
		{
			return this.m_vCSVTable.GetValue(name, level + this.m_vRowStart);
		}
	}
}
