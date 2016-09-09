using System;
using System.Collections.Generic;
using System.Reflection;
using UCS.Logic;

namespace UCS.GameFiles
{
	internal class Data
	{
		private readonly int m_vGlobalID;

		protected CSVRow m_vCSVRow;

		protected DataTable m_vDataTable;

		public Data(CSVRow row, DataTable dt)
		{
			this.m_vCSVRow = row;
			this.m_vDataTable = dt;
			this.m_vGlobalID = GlobalID.CreateGlobalID(dt.GetTableIndex() + 1, dt.GetItemCount());
		}

		public int GetDataType()
		{
			return this.m_vDataTable.GetTableIndex();
		}

		public int GetGlobalID()
		{
			return this.m_vGlobalID;
		}

		public int GetInstanceID()
		{
			return GlobalID.GetInstanceID(this.m_vGlobalID);
		}

		public string GetName()
		{
			return this.m_vCSVRow.GetName();
		}

		public static void LoadData(Data obj, Type objectType, CSVRow row)
		{
			PropertyInfo[] properties = objectType.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				if (propertyInfo.PropertyType.IsGenericType)
				{
					Type arg_39_0 = typeof(List<>);
					Type[] genericArguments = propertyInfo.PropertyType.GetGenericArguments();
					Type expr_3E = arg_39_0.MakeGenericType(genericArguments);
					object obj2 = Activator.CreateInstance(expr_3E);
					MethodInfo method = expr_3E.GetMethod("Add");
					string memberName = ((DefaultMemberAttribute)obj2.GetType().GetCustomAttributes(typeof(DefaultMemberAttribute), true)[0]).MemberName;
					PropertyInfo property = obj2.GetType().GetProperty(memberName);
					for (int j = row.GetRowOffset(); j < row.GetRowOffset() + row.GetArraySize(propertyInfo.Name); j++)
					{
						string text = row.GetValue(propertyInfo.Name, j - row.GetRowOffset());
						if (text == string.Empty && j != row.GetRowOffset())
						{
							text = property.GetValue(obj2, new object[]
							{
								j - row.GetRowOffset() - 1
							}).ToString();
						}
						if (text == string.Empty)
						{
							object obj3 = genericArguments[0].IsValueType ? Activator.CreateInstance(genericArguments[0]) : string.Empty;
							method.Invoke(obj2, new object[]
							{
								obj3
							});
						}
						else
						{
							method.Invoke(obj2, new object[]
							{
								Convert.ChangeType(text, genericArguments[0])
							});
						}
					}
					propertyInfo.SetValue(obj, obj2);
				}
				else if (row.GetValue(propertyInfo.Name, 0) == string.Empty)
				{
					propertyInfo.SetValue(obj, null, null);
				}
				else
				{
					propertyInfo.SetValue(obj, Convert.ChangeType(row.GetValue(propertyInfo.Name, 0), propertyInfo.PropertyType), null);
				}
			}
		}
	}
}
