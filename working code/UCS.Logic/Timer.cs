using System;

namespace UCS.Logic
{
	internal class Timer
	{
		private int m_vSeconds;

		private DateTime m_vStartTime;

		public Timer()
		{
			this.m_vStartTime = new DateTime(1970, 1, 1);
			this.m_vSeconds = 0;
		}

		public void FastForward(int seconds)
		{
			this.m_vSeconds -= seconds;
		}

		public int GetRemainingSeconds(DateTime time, bool boost = false, DateTime boostEndTime = default(DateTime), float multiplier = 0f)
		{
			int num;
			if (!boost)
			{
				num = this.m_vSeconds - (int)time.Subtract(this.m_vStartTime).TotalSeconds;
			}
			else if (boostEndTime >= time)
			{
				num = this.m_vSeconds - (int)(time.Subtract(this.m_vStartTime).TotalSeconds * (double)multiplier);
			}
			else
			{
				float num2 = (float)time.Subtract(this.m_vStartTime).TotalSeconds - (float)(time - boostEndTime).TotalSeconds;
				float num3 = (float)time.Subtract(this.m_vStartTime).TotalSeconds - num2;
				num = this.m_vSeconds - (int)(num2 * multiplier + num3);
			}
			if (num <= 0)
			{
				num = 0;
			}
			return num;
		}

		public int GetRemainingSeconds(DateTime time)
		{
			int num = this.m_vSeconds - (int)time.Subtract(this.m_vStartTime).TotalSeconds;
			if (num <= 0)
			{
				num = 0;
			}
			return num;
		}

		public DateTime GetStartTime()
		{
			return this.m_vStartTime;
		}

		public void StartTimer(int seconds, DateTime time)
		{
			this.m_vStartTime = time;
			this.m_vSeconds = seconds;
		}
	}
}
