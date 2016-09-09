using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UCS.Core;
using UCS.GameFiles;
using UCS.Helpers;

namespace UCS.Logic
{
	internal class DataSlot
	{
		public Data Data;

		public int Value;

		public DataSlot(Data d, int value)
		{
			this.Data = d;
			this.Value = value;
		}

		public void Decode(BinaryReader br)
		{
			this.Data = br.ReadDataReference();
			this.Value = br.ReadInt32WithEndian();
		}

		public byte[] Encode()
		{
			List<byte> expr_05 = new List<byte>();
			expr_05.AddInt32(this.Data.GetGlobalID());
			expr_05.AddInt32(this.Value);
			return expr_05.ToArray();
		}

		public void Load(JObject jsonObject)
		{
			this.Data = ObjectManager.DataTables.GetDataById(jsonObject["global_id"].ToObject<int>());
			this.Value = jsonObject["value"].ToObject<int>();
		}

		public JObject Save(JObject jsonObject)
		{
			jsonObject.Add("global_id", this.Data.GetGlobalID());
			jsonObject.Add("value", this.Value);
			return jsonObject;
		}
	}
}
