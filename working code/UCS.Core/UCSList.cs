using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace UCS.Core
{
	internal class UCSList
	{
		public static class Http
		{
			public static string Post(string uri, NameValueCollection pairs)
			{
				byte[] bytes = null;
				using (WebClient webClient = new WebClient())
				{
					bytes = webClient.UploadValues(uri, pairs);
				}
				return Encoding.UTF8.GetString(bytes);
			}
		}

		[CompilerGenerated]
		[Serializable]
		private sealed class UCSListInter
		{
			public static readonly UCSList.UCSListInter ins  = new UCSList.UCSListInter();

			public static ThreadStart start;

			internal void startD()
			{
				while (true)
				{
					UCSList.SendData();
					Thread.Sleep(60000);
				}
			}
		}

		private static readonly string APIKey = ConfigurationManager.AppSettings["UCSList - APIKey"];

		private static readonly int Status = UCSList.CheckStatus();

		private static readonly string UCSPanel = "https://www.ultrapowa.xyz/api/";

		private static Thread T
		{
			get;
			set;
		}

		public UCSList()
		{
			if (!string.IsNullOrEmpty(UCSList.APIKey) && UCSList.APIKey.Length == 25)
			{
                ThreadStart arg_3F_0 = null;
				if (arg_3F_0  == null)
				{
                    arg_3F_0 = UCSListInter.ins.startD;

                }
				UCSList.T = new Thread(arg_3F_0);
				UCSList.T.Start();
				return;
			}
			Console.WriteLine("[UCS]     UCSList API is disabled - Visit www.ultrapowa.xyz for more info.");
		}

		public static int CheckStatus()
		{
			if (Convert.ToBoolean(ConfigurationManager.AppSettings["maintenanceMode"]))
			{
				return 2;
			}
			return 1;
		}

		public static void SendData()
		{
			string text = UCSList.Http.Post(UCSList.UCSPanel, new NameValueCollection
			{
				{
					"ApiKey",
					UCSList.APIKey
				},
				{
					"OnlinePlayers",
					Convert.ToString(ResourcesManager.GetOnlinePlayers().Count)
				},
				{
					"Status",
					Convert.ToString(UCSList.Status)
				}
			}).Remove(0, 1);
			if (text == "OK")
			{
				Console.WriteLine("[UCS]    UCS Sent data successfully.");
				return;
			}
			Console.WriteLine("[UCS]    UCSList Server answer uncorrectly : " + text);
		}
	}
}
