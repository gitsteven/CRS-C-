using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.Logic
{
	internal class InvitationStreamEntry : StreamEntry
	{
		public static string Message = "Hello, i want to join your clan.";

		public static string Judge;

		public static int State = 3;

		public override byte[] Encode()
		{
			List<byte> expr_05 = new List<byte>();
			expr_05.AddRange(base.Encode());
			expr_05.AddString(InvitationStreamEntry.Message);
			expr_05.AddString(InvitationStreamEntry.Judge);
			expr_05.AddInt32(InvitationStreamEntry.State);
			return expr_05.ToArray();
		}

		public override int GetStreamEntryType()
		{
			return 3;
		}

		public override void Load(JObject jsonObject)
		{
			base.Load(jsonObject);
			InvitationStreamEntry.Message = jsonObject["message"].ToObject<string>();
			InvitationStreamEntry.Judge = jsonObject["judge"].ToObject<string>();
			InvitationStreamEntry.State = jsonObject["state"].ToObject<int>();
		}

		public override JObject Save(JObject jsonObject)
		{
			jsonObject = base.Save(jsonObject);
			jsonObject.Add("message", InvitationStreamEntry.Message);
			jsonObject.Add("judge", InvitationStreamEntry.Judge);
			jsonObject.Add("state", InvitationStreamEntry.State);
			return jsonObject;
		}

		public void SetJudgeName(string name)
		{
			InvitationStreamEntry.Judge = name;
		}

		public void SetMessage(string message)
		{
			InvitationStreamEntry.Message = message;
		}

		public void SetState(int status)
		{
			InvitationStreamEntry.State = status;
		}
	}
}
