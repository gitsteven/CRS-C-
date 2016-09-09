using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.Logic
{
	internal class ChatStreamEntry : StreamEntry
	{
		private string m_vMessage;

		public override byte[] Encode()
		{
			List<byte> expr_05 = new List<byte>();
			expr_05.AddRange(base.Encode());
			expr_05.AddString(this.m_vMessage);
			return expr_05.ToArray();
		}

		public string GetMessage()
		{
			return this.m_vMessage;
		}

		public override int GetStreamEntryType()
		{
			return 2;
		}

		public override void Load(JObject jsonObject)
		{
			base.Load(jsonObject);
			this.m_vMessage = jsonObject["message"].ToObject<string>();
		}

		public override JObject Save(JObject jsonObject)
		{
			jsonObject = base.Save(jsonObject);
			jsonObject.Add("message", this.m_vMessage);
			return jsonObject;
		}

		public void SetMessage(string message)
		{
			this.m_vMessage = message;
		}
	}
}
