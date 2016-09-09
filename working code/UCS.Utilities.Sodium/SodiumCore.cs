using System;
using System.Runtime.InteropServices;

namespace UCS.Utilities.Sodium
{
	public static class SodiumCore
	{
		private static bool _isInit;

		static SodiumCore()
		{
			SodiumCore.Init();
		}

		public static byte[] GetRandomBytes(int count)
		{
			byte[] array = new byte[count];
			SodiumLibrary.randombytes_buff(array, count);
			return array;
		}

		public static int GetRandomNumber(int upperBound)
		{
			return SodiumLibrary.randombytes_uniform(upperBound);
		}

		public static string SodiumVersionString()
		{
			return Marshal.PtrToStringAnsi(SodiumLibrary.sodium_version_string());
		}

		internal static void Init()
		{
			if (!SodiumCore._isInit)
			{
				SodiumLibrary.init();
				SodiumCore._isInit = true;
			}
		}
	}
}
