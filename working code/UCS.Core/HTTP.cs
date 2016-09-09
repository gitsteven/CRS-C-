using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;

namespace UCS.Core
{
	internal class HTTP
	{
		private HttpListener _listener;

		private int _port;

		private Thread _serverThread;

		private string jsonapp;

		private string mime = "text/plain";

		public int Port
		{
			get
			{
				return this._port;
			}
			private set
			{
			}
		}

		public Dictionary<string, string> UCS
		{
			get;
			internal set;
		}

		public HTTP(int port)
		{
			this.Initialize(port);
		}

		public HTTP()
		{
			TcpListener expr_1C = new TcpListener(IPAddress.Loopback, 0);
			expr_1C.Start();
			int port = ((IPEndPoint)expr_1C.LocalEndpoint).Port;
			expr_1C.Stop();
			this.Initialize(port);
		}

		public void Stop()
		{
			this._serverThread.Abort();
			this._listener.Stop();
		}

		private void Listen()
		{
			this._listener = new HttpListener();
			this._listener.Prefixes.Add(string.Concat(new object[]
			{
				"http://+:",
				this._port,
				"/",
				ConfigurationManager.AppSettings["ApiKey"],
				"/"
			}));
			this._listener.Start();
			while (true)
			{
				try
				{
					HttpListenerContext context = this._listener.GetContext();
					this.Process(context);
				}
				catch (Exception)
				{
				}
			}
		}

		private static byte[] GetBytes(string str)
		{
			byte[] array = new byte[str.Length * 2];
			Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
			return array;
		}

		public Stream GenerateStreamFromString(string s)
		{
			MemoryStream expr_05 = new MemoryStream();
			StreamWriter expr_0B = new StreamWriter(expr_05);
			expr_0B.Write(s);
			expr_0B.Flush();
			expr_05.Position = 0L;
			return expr_05;
		}

		private void Handler(string type)
		{
			try
			{
				if (type == "inmemclans")
				{
					this.jsonapp = Convert.ToString(ObjectManager.GetInMemoryAlliances().Count);
				}
				else if (type == "inmemplayers")
				{
					this.jsonapp = Convert.ToString(ResourcesManager.GetInMemoryLevels().Count);
				}
				else if (type == "onlineplayers")
				{
					this.jsonapp = Convert.ToString(ResourcesManager.GetOnlinePlayers().Count);
				}
				else if (type == "totalclients")
				{
					this.jsonapp = Convert.ToString(ResourcesManager.GetConnectedClients().Count);
				}
				else if (type == "all")
				{
					JsonApi jsonApi = new JsonApi
					{
						UCS = new Dictionary<string, string>
						{
							{
								"PatchingServer",
								ConfigurationManager.AppSettings["patchingServer"]
							},
							{
								"Maintenance",
								ConfigurationManager.AppSettings["maintenanceMode"]
							},
							{
								"MaintenanceTimeLeft",
								ConfigurationManager.AppSettings["maintenanceTimeLeft"]
							},
							{
								"ClientVersion",
								ConfigurationManager.AppSettings["clientVersion"]
							},
							{
								"ServerVersion",
								Assembly.GetExecutingAssembly().GetName().Version.ToString()
							},
							{
								"OnlinePlayers",
								Convert.ToString(ResourcesManager.GetOnlinePlayers().Count)
							},
							{
								"InMemoryPlayers",
								Convert.ToString(ResourcesManager.GetInMemoryLevels().Count)
							},
							{
								"InMemoryClans",
								Convert.ToString(ObjectManager.GetInMemoryAlliances().Count)
							},
							{
								"TotalConnectedClients",
								Convert.ToString(ResourcesManager.GetConnectedClients().Count)
							}
						}
					};
					this.jsonapp = JsonConvert.SerializeObject(jsonApi);
					this.mime = "application/json";
				}
				else if (type == "ram")
				{
					this.jsonapp = Performances.GetUsedMemory();
				}
				else
				{
					this.jsonapp = "OK";
				}
			}
			catch (Exception arg)
			{
				this.jsonapp = "An exception occured in UCS : \n" + arg;
			}
		}

		private void Process(HttpListenerContext context)
		{
			IEnumerable<string> expr_06 = new string[]
			{
				"inmemclans",
				"inmemplayers",
				"onlineplayers",
				"totalclients",
				"ram",
				string.Empty
			};
			string text = context.Request.Url.AbsolutePath.Substring(7).ToLower();
			if (expr_06.Contains(text))
			{
				this.Handler(text);
				try
				{
					context.Response.ContentType = this.mime;
					context.Response.ContentEncoding = Encoding.UTF8;
					context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
					context.Response.AddHeader("Last-Modified", DateTime.UtcNow.ToString("r"));
					context.Response.AddHeader("APIVersion", "1.0a");
					byte[] array = new byte[16384];
					using (Stream stream = this.GenerateStreamFromString(this.jsonapp))
					{
						int count;
						while ((count = stream.Read(array, 0, array.Length)) > 0)
						{
							context.Response.OutputStream.Write(array, 0, count);
						}
						stream.Close();
					}
					context.Response.StatusCode = 200;
					context.Response.OutputStream.Flush();
					goto IL_177;
				}
				catch (Exception)
				{
					context.Response.StatusCode = 500;
					goto IL_177;
				}
			}
			context.Response.StatusCode = 404;
			IL_177:
			context.Response.OutputStream.Close();
		}

		private void Initialize(int port)
		{
			this._port = port;
			this._serverThread = new Thread(new ThreadStart(this.Listen));
			this._serverThread.Start();
			Console.WriteLine("[UCS]    API has been successfully started");
		}
	}
}
