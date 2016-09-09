using System;
using System.Runtime.InteropServices;

namespace UCS.Core
{
	internal class Performances
	{
		public static string GetFreeMemoryMB()
		{
			return PerformanceInfo.GetPhysicalAvailableMemoryInMiB().ToString();
		}

		public static string GetTotalMemory()
		{
			return PerformanceInfo.GetTotalMemoryInMiB().ToString();
		}

		public static string GetUsedMemory()
		{
			long arg_0B_0 = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
			long totalMemoryInMiB = PerformanceInfo.GetTotalMemoryInMiB();
			decimal d = arg_0B_0 / totalMemoryInMiB * 100m;
			return (100m - d).ToString("##.##");
		}

		public static string GetFreeMemory()
		{
			long arg_0B_0 = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
			long totalMemoryInMiB = PerformanceInfo.GetTotalMemoryInMiB();
			return (arg_0B_0 / totalMemoryInMiB * 100m).ToString("##.##");
		}
	}


    internal class PerformanceInfo
    {
        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPerformanceInfo();

        public static long GetPhysicalAvailableMemoryInMiB()
        {
            var pi = new PerformanceInformation();
            if (GetPerformanceInfo())
                return Convert.ToInt64(pi.PhysicalAvailable.ToInt64() * pi.PageSize.ToInt64() / 1048576);
            return -1;
        }

        public static long GetTotalMemoryInMiB()
        {
            var pi = new PerformanceInformation();
            if (GetPerformanceInfo())
                return Convert.ToInt64(pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / 1048576);
            return -1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PerformanceInformation
        {
            public int Size;
            public IntPtr CommitTotal;
            public IntPtr CommitLimit;
            public IntPtr CommitPeak;
            public IntPtr PhysicalTotal;
            public IntPtr PhysicalAvailable;
            public IntPtr SystemCache;
            public IntPtr KernelTotal;
            public IntPtr KernelPaged;
            public IntPtr KernelNonPaged;
            public IntPtr PageSize;
            public int HandlesCount;
            public int ProcessCount;
            public int ThreadCount;
        }
    }
}
