using System;
using System.Collections.Generic;
using System.IO;

namespace UCS.GameFiles
{
	internal class CSVTable
	{
		private readonly List<string> m_vColumnHeaders;

		private readonly List<string> m_vColumnTypes;

		private readonly List<CSVColumn> m_vCSVColumns;

		private readonly List<CSVRow> m_vCSVRows;

		public CSVTable(string filePath)
		{
			this.m_vCSVRows = new List<CSVRow>();
			this.m_vColumnHeaders = new List<string>();
			this.m_vColumnTypes = new List<string>();
			this.m_vCSVColumns = new List<CSVColumn>();
			using (StreamReader streamReader = new StreamReader(filePath))
			{
				string[] array = streamReader.ReadLine().Replace("\"", string.Empty).Replace(" ", string.Empty).Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					string item = array[i];
					this.m_vColumnHeaders.Add(item);
					this.m_vCSVColumns.Add(new CSVColumn());
				}
				array = streamReader.ReadLine().Replace("\"", string.Empty).Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					string item2 = array[i];
					this.m_vColumnTypes.Add(item2);
				}
				while (!streamReader.EndOfStream)
				{
					string[] array2 = streamReader.ReadLine().Replace("\"", string.Empty).Split(new char[]
					{
						','
					});
					if (array2[0] != string.Empty)
					{
						this.CreateRow();
					}
					for (int j = 0; j < this.m_vColumnHeaders.Count; j++)
					{
						this.m_vCSVColumns[j].Add(array2[j]);
					}
				}
			}
		}

		public void AddRow(CSVRow row)
		{
			this.m_vCSVRows.Add(row);
		}

		public void CreateRow()
		{
			new CSVRow(this);
		}

		public int GetArraySizeAt(CSVRow row, int columnIndex)
		{
			int num = this.m_vCSVRows.IndexOf(row);
			if (num == -1)
			{
				return 0;
			}
			CSVColumn cSVColumn = this.m_vCSVColumns[columnIndex];
			int nextOffset;
			if (num + 1 >= this.m_vCSVRows.Count)
			{
				nextOffset = cSVColumn.GetSize();
			}
			else
			{
				nextOffset = this.m_vCSVRows[num + 1].GetRowOffset();
			}
			return CSVColumn.GetArraySize(row.GetRowOffset(), nextOffset);
		}

		public int GetColumnIndexByName(string name)
		{
			return this.m_vColumnHeaders.IndexOf(name);
		}

		public string GetColumnName(int index)
		{
			return this.m_vColumnHeaders[index];
		}

		public int GetColumnRowCount()
		{
			int result = 0;
			if (this.m_vCSVColumns.Count > 0)
			{
				result = this.m_vCSVColumns[0].GetSize();
			}
			return result;
		}

		public CSVRow GetRowAt(int index)
		{
			return this.m_vCSVRows[index];
		}

		public int GetRowCount()
		{
			return this.m_vCSVRows.Count;
		}

		public string GetValue(string name, int level)
		{
			int column = this.m_vColumnHeaders.IndexOf(name);
			return this.GetValueAt(column, level);
		}

		public string GetValueAt(int column, int row)
		{
			return this.m_vCSVColumns[column].Get(row);
		}
	}
}
