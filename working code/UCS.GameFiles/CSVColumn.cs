using System;
using System.Collections.Generic;

namespace UCS.GameFiles
{
	internal class CSVColumn
	{
		private readonly List<string> m_vValues;

		public CSVColumn()
		{
			this.m_vValues = new List<string>();
		}

		public void Add(string value)
		{
			this.m_vValues.Add(value);
		}

		public string Get(int row)
		{
			return this.m_vValues[row];
		}

		public static int GetArraySize(int currentOffset, int nextOffset)
		{
			return nextOffset - currentOffset;
		}

		public int GetSize()
		{
			return this.m_vValues.Count;
		}
	}
}
