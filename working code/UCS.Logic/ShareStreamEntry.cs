using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.Logic
{
	internal class ShareStreamEntry : StreamEntry
	{
		public static int Unknown1;

		public static int Unknown2;

		public static int Unknown3;

		public static byte Unknown4;

		public static string Message = "Look this battle !";

		public static string EnemyName = "UltraPowa";

		public static string ReplayJson;

		public static int Unknown5;

		public static int Unknown6;

		public static int Unknown7;

		public override byte[] Encode()
		{
			List<byte> expr_05 = new List<byte>();
			expr_05.AddRange(base.Encode());
			expr_05.AddInt32(ShareStreamEntry.Unknown1);
			expr_05.AddInt32(ShareStreamEntry.Unknown2);
			expr_05.AddInt32(ShareStreamEntry.Unknown3);
			expr_05.Add(ShareStreamEntry.Unknown4);
			expr_05.AddString(ShareStreamEntry.Message);
			expr_05.AddString(ShareStreamEntry.EnemyName);
			expr_05.AddString(ShareStreamEntry.ReplayJson);
			expr_05.AddInt32(ShareStreamEntry.Unknown5);
			expr_05.AddInt32(ShareStreamEntry.Unknown6);
			expr_05.AddInt32(ShareStreamEntry.Unknown7);
			return expr_05.ToArray();
		}

		public override int GetStreamEntryType()
		{
			return 5;
		}

		public override void Load(JObject jsonObject)
		{
			base.Load(jsonObject);
			ShareStreamEntry.Unknown1 = jsonObject["unknown1"].ToObject<int>();
			ShareStreamEntry.Unknown2 = jsonObject["unknown2"].ToObject<int>();
			ShareStreamEntry.Unknown3 = jsonObject["unknown3"].ToObject<int>();
			ShareStreamEntry.Unknown4 = jsonObject["unknown4"].ToObject<byte>();
			ShareStreamEntry.Message = jsonObject["message"].ToObject<string>();
			ShareStreamEntry.EnemyName = jsonObject["enemy"].ToObject<string>();
			ShareStreamEntry.ReplayJson = jsonObject["replay"].ToObject<string>();
			ShareStreamEntry.Unknown5 = jsonObject["unknown5"].ToObject<int>();
			ShareStreamEntry.Unknown6 = jsonObject["unknown6"].ToObject<int>();
			ShareStreamEntry.Unknown7 = jsonObject["unknown7"].ToObject<int>();
		}

		public override JObject Save(JObject jsonObject)
		{
			jsonObject = base.Save(jsonObject);
			jsonObject.Add("unknown1", ShareStreamEntry.Unknown1);
			jsonObject.Add("unknown2", ShareStreamEntry.Unknown2);
			jsonObject.Add("unknown3", ShareStreamEntry.Unknown3);
			jsonObject.Add("unknown4", ShareStreamEntry.Unknown4);
			jsonObject.Add("message", ShareStreamEntry.Message);
			jsonObject.Add("enemy", ShareStreamEntry.EnemyName);
			jsonObject.Add("replay", ShareStreamEntry.ReplayJson);
			jsonObject.Add("unknown5", ShareStreamEntry.Unknown5);
			jsonObject.Add("unknown6", ShareStreamEntry.Unknown6);
			jsonObject.Add("unknown7", ShareStreamEntry.Unknown7);
			return jsonObject;
		}

		public void SetEnemyName(string name)
		{
			ShareStreamEntry.EnemyName = name;
		}

		public void SetMessage(string message)
		{
			ShareStreamEntry.Message = message;
		}

		public void SetReplayjson(string json)
		{
			ShareStreamEntry.ReplayJson = json;
		}
	}
}
