using System;
using System.Collections.Generic;

namespace UCS.PacketProcessing
{
	internal static class GameOpCommandFactory
	{
		private static readonly Dictionary<string, Type> m_vCommands;

		static GameOpCommandFactory()
		{
			GameOpCommandFactory.m_vCommands = new Dictionary<string, Type>();
		}

		public static object Parse(string command)
		{
			string[] array = command.Split(new char[]
			{
				' '
			});
			object result = null;
			if (array.Length != 0 && GameOpCommandFactory.m_vCommands.ContainsKey(array[0]))
			{
				result = GameOpCommandFactory.m_vCommands[array[0]].GetConstructor(new Type[]
				{
					typeof(string[])
				}).Invoke(new object[]
				{
					array
				});
			}
			return result;
		}
	}
}
