using System;

namespace UCS.Logic
{
	public static class GlobalID
	{
		public static int CreateGlobalID(int index, int count)
		{
			return count + 1000000 * index;
		}

		public static int GetClassID(int commandType)
		{
			commandType = (int)(1125899907L * (long)commandType >> 32);
			return (commandType >> 18) + (commandType >> 31);
		}

		public static int GetInstanceID(int globalID)
		{
			int num = 1125899907;
			num = (int)((long)num * (long)globalID >> 32);
			return globalID - 1000000 * ((num >> 18) + (num >> 31));
		}
	}
}
