using System;
using System.IO;
using System.Text.RegularExpressions;
using UCS.PacketProcessing;

namespace UCS.Core
{
	internal static class Logger
	{
		private static readonly object m_vSyncObject;

		private static readonly TextWriter m_vTextWriter;

		private static int m_vLogLevel;

		static Logger()
		{
			Logger.m_vSyncObject = new object();
			Logger.m_vTextWriter = TextWriter.Synchronized(File.AppendText("logs/data_" + DateTime.Now.ToString("yyyyMMdd") + ".log"));
			Logger.m_vLogLevel = 1;
		}

		public static void SetLogLevel(int level)
		{
			Logger.m_vLogLevel = level;
		}

		public static int GetLogLevel()
		{
			return Logger.m_vLogLevel;
		}

		public static void WriteLine(Message p, string prefix = null, int logLevel = 4)
		{
			if (logLevel <= Logger.m_vLogLevel)
			{
				object vSyncObject = Logger.m_vSyncObject;
				lock (vSyncObject)
				{
					Logger.m_vTextWriter.Write(DateTime.Now.ToString("yyyyMMddHHmmss"));
					Logger.m_vTextWriter.Write("; ");
					if (prefix != null)
					{
						Logger.m_vTextWriter.Write(prefix);
					}
					Logger.m_vTextWriter.Write(p.GetMessageType().ToString());
					Logger.m_vTextWriter.Write("(");
					Logger.m_vTextWriter.Write(p.GetMessageVersion().ToString());
					Logger.m_vTextWriter.Write(")");
					Logger.m_vTextWriter.Write("; ");
					Logger.m_vTextWriter.Write(p.GetLength().ToString());
					Logger.m_vTextWriter.Write("; ");
					Logger.m_vTextWriter.WriteLine(p.ToHexString());
					Logger.m_vTextWriter.WriteLine(Regex.Replace(p.ToString(), "[^\\u0020-\\u007F]", "."));
					Logger.m_vTextWriter.Flush();
				}
			}
		}

		public static void WriteLine(string s, string prefix = null, int logLevel = 4)
		{
			if (logLevel <= Logger.m_vLogLevel)
			{
				object vSyncObject = Logger.m_vSyncObject;
				lock (vSyncObject)
				{
					Logger.m_vTextWriter.Write("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
					Logger.m_vTextWriter.Write("; ");
					if (prefix != null)
					{
						Logger.m_vTextWriter.Write(prefix);
					}
					Logger.m_vTextWriter.WriteLine(s);
					Logger.m_vTextWriter.Flush();
				}
			}
		}
	}
}
