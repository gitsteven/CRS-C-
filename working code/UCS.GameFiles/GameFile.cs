using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace UCS.GameFiles
{
	internal class GameFile
	{
		public string file
		{
			get;
			set;
		}

		public string sha
		{
			get;
			set;
		}

		public void Load(JObject jsonObject)
		{
			this.sha = jsonObject["sha"].ToObject<string>();
			this.file = jsonObject["file"].ToObject<string>();
		}

		public string SaveToJson(JObject fingerPrint)
		{
			fingerPrint.Add("sha", this.sha);
			fingerPrint.Add("file", this.file);
			return JsonConvert.SerializeObject(fingerPrint);
		}
	}
}
