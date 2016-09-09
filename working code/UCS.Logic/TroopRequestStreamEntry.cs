using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.Logic
{
	internal class TroopRequestStreamEntry : StreamEntry
	{
		public static int Unknown1;

		public static int Unknown2 = 2;

		public static int Unknown3;

		public static int Unknown4 = 2;

		public static int Unknown5;

		public static string Message;

		public static DataSlot AllianceDonation;

		public static DataSlot UnitComponent;

		public override byte[] Encode()
		{
			List<byte> expr_05 = new List<byte>();
			expr_05.AddRange(base.Encode());
			expr_05.AddInt32(TroopRequestStreamEntry.Unknown1);
			expr_05.AddInt32(TroopRequestStreamEntry.Unknown2);
			expr_05.AddInt32(TroopRequestStreamEntry.Unknown3);
			expr_05.AddInt32(TroopRequestStreamEntry.Unknown4);
			expr_05.AddInt32(TroopRequestStreamEntry.Unknown5);
			expr_05.AddDataSlots(new List<DataSlot>());
			expr_05.AddString(TroopRequestStreamEntry.Message);
			expr_05.AddDataSlots(new List<DataSlot>());
			return expr_05.ToArray();
		}

		public override int GetStreamEntryType()
		{
			return 1;
		}

		public override void Load(JObject jsonObject)
		{
			base.Load(jsonObject);
			TroopRequestStreamEntry.Unknown1 = jsonObject["unknown1"].ToObject<int>();
			TroopRequestStreamEntry.Unknown2 = jsonObject["unknown2"].ToObject<int>();
			TroopRequestStreamEntry.Unknown3 = jsonObject["unknown3"].ToObject<int>();
			TroopRequestStreamEntry.Unknown4 = jsonObject["unknown4"].ToObject<int>();
			TroopRequestStreamEntry.Unknown5 = jsonObject["unknown5"].ToObject<int>();
			TroopRequestStreamEntry.Message = jsonObject["message"].ToObject<string>();
		}

		public override JObject Save(JObject jsonObject)
		{
			jsonObject = base.Save(jsonObject);
			jsonObject.Add("unknown1", TroopRequestStreamEntry.Unknown1);
			jsonObject.Add("unknown2", TroopRequestStreamEntry.Unknown2);
			jsonObject.Add("unknown3", TroopRequestStreamEntry.Unknown3);
			jsonObject.Add("unknown4", TroopRequestStreamEntry.Unknown4);
			jsonObject.Add("unknown5", TroopRequestStreamEntry.Unknown5);
			JObject arg_99_0 = jsonObject;
			string arg_99_1 = "donations";
			JArray expr_7D = new JArray();
			expr_7D.Add(300000);
			expr_7D.Add(0);
			arg_99_0.Add(arg_99_1, expr_7D);
			jsonObject.Add("message", TroopRequestStreamEntry.Message);
			jsonObject.Add("tdonations", new JArray());
			return jsonObject;
		}

		public void SetMessage(string msg)
		{
			TroopRequestStreamEntry.Message = msg;
		}
	}
}
