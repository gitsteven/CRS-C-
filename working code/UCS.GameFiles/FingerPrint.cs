using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace UCS.GameFiles
{
	internal class FingerPrint
	{
		public List<GameFile> files
		{
			get;
			set;
		}

		public string sha
		{
			get;
			set;
		}

		public string version
		{
			get;
			set;
		}

		public FingerPrint(string filePath)
		{
			this.files = new List<GameFile>();
			string jsonString = null;
			if (File.Exists(filePath))
			{
				using (StreamReader streamReader = new StreamReader(filePath))
				{
					jsonString = streamReader.ReadToEnd();
				}
				this.LoadFromJson(jsonString);
				Console.WriteLine("[UCS]    ObjectManager: fingerprint loaded");
				return;
			}
			Console.WriteLine("[UCS]    LoadFingerPrint: error! tried to load FingerPrint without file, run gen_patch first");
		}

		public void LoadFromJson(string jsonString)
		{
			JObject jObject = JObject.Parse(jsonString);
			using (IEnumerator<JToken> enumerator = ((JArray)jObject["files"]).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JObject jsonObject = (JObject)enumerator.Current;
					GameFile gameFile = new GameFile();
					gameFile.Load(jsonObject);
					this.files.Add(gameFile);
				}
			}
			this.sha = jObject["sha"].ToObject<string>();
			this.version = jObject["version"].ToObject<string>();
		}

		public string SaveToJson()
		{
			JObject jObject = new JObject();
			JArray jArray = new JArray();
			foreach (GameFile arg_28_0 in this.files)
			{
				JObject jObject2 = new JObject();
				arg_28_0.SaveToJson(jObject2);
				jArray.Add(jObject2);
			}
			jObject.Add("files", jArray);
			jObject.Add("sha", this.sha);
			jObject.Add("version", this.version);
			return JsonConvert.SerializeObject(jObject).Replace("/", "\\/");
		}
	}
}
