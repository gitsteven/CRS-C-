using System;
using System.Configuration;
using System.Net;
using UCS.Core;

namespace UCS.Helpers
{
	internal class CommandParser
	{
		public static void Parse(string Command)
		{
			if (Command == "/help")
			{
				Console.WriteLine("[UCS][MENU]  -> /startx      - Starts the UCS Interface.");
				Console.WriteLine("[UCS][MENU]  -> /status      - Shows the actual UCS status.");
				Console.WriteLine("[UCS][MENU]  -> /clear       - Clears the console screen.");
				Console.WriteLine("[UCS][MENU]  -> /restart     - Restarts UCS instantly.");
				Console.WriteLine("[UCS][MENU]  -> /prepareshutdown    - Send Shutdown Message To Users.");
				Console.WriteLine("[UCS][MENU]  -> /shutdown    - Shuts UCS down instantly.");
				return;
			}
			if (Command == "/status")
			{
				Console.WriteLine("");
				Console.WriteLine("[UCS][INFO]  -> IP Address (public):    " + new WebClient().DownloadString("http://bot.whatismyipaddress.com/"));
				Console.WriteLine("[UCS][INFO]  -> IP Address (local):     " + Dns.GetHostByName(Dns.GetHostName()).AddressList[0]);
				Console.WriteLine("[UCS][INFO]  -> Online players:         " + ResourcesManager.GetOnlinePlayers().Count);
				Console.WriteLine("[UCS][INFO]  -> Connected players:      " + ResourcesManager.GetConnectedClients().Count);
				Console.WriteLine("[UCS][INFO]  -> Clash Version:          " + ConfigurationManager.AppSettings["clientVersion"]);
				return;
			}
			if (Command == "/clear")
			{
				Console.Clear();
				return;
			}
			if (Command == "/restart")
			{
				Environment.Exit(0);
				return;
			}
			if (!(Command == "/shutdown"))
			{
				Console.WriteLine("[UCS]    Unknown command, type \"/help\" for a list containing all available commands.");
				return;
			}
			Environment.Exit(0);
		}
	}
}
