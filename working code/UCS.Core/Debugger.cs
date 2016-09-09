using System;
using System.IO;

namespace UCS.Core
{
	internal static class Debugger
	{
		private static readonly object m_vSyncObject;

		private static readonly TextWriter m_vTextWriter;

		private static int m_vLogLevel;

		static Debugger()
		{
			Debugger.m_vSyncObject = new object();
			Debugger.m_vTextWriter = TextWriter.Synchronized(File.AppendText("logs/debug_" + DateTime.Now.ToString("yyyyMMdd") + ".log"));
			Debugger.m_vLogLevel = 1;
		}

		public static void SetLogLevel(int level)
		{
			Debugger.m_vLogLevel = level;
		}

		public static int GetLogLevel()
		{
			return Debugger.m_vLogLevel;
		}

		public static void WriteLine(string text, Exception ex = null, int logLevel = 4)
		{
			string text2 = text;
			if (ex != null)
			{
				text2 += ex.ToString();
			}
			if (logLevel <= Debugger.m_vLogLevel)
			{
				Console.WriteLine(text2);
				object vSyncObject = Debugger.m_vSyncObject;
				lock (vSyncObject)
				{
					Debugger.m_vTextWriter.Write(DateTime.Now.ToString("yyyyMMddHHmmss"));
					Debugger.m_vTextWriter.Write("\t");
					Debugger.m_vTextWriter.WriteLine(text2);
					if (ex != null)
					{
						Debugger.m_vTextWriter.WriteLine(ex.ToString());
					}
					Debugger.m_vTextWriter.Flush();
				}
			}
		}
	}
}
