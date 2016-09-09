using System;

namespace UCS.Helpers
{
	internal static class GamePlayUtil
	{
		public static int CalculateResourceCost(int sup, int inf, int supCost, int infCost, int amount)
		{
			return (int)Math.Round((double)((long)(supCost - infCost) * (long)(amount - inf)) / ((double)sup - (double)inf * 1.0)) + infCost;
		}

		public static int CalculateSpeedUpCost(int sup, int inf, int supCost, int infCost, int amount)
		{
			return (int)Math.Round((double)((long)(supCost - infCost) * (long)(amount - inf)) / ((double)sup - (double)inf * 1.0)) + infCost;
		}
	}
}
