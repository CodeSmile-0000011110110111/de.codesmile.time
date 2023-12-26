// Copyright (C) 2021-2023 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using CodeSmile.Time;
using NUnit.Framework;
using System;

namespace CodeSmile.Tests
{
	public class TimerTests
	{
		[Test] public void DefaultCtor_IsElapsed()
		{
			var timer = new IntervalTimer();

			Assert.True(timer.IsElapsed);
		}

		[Test] public void Start_NegativeInterval_Throws()
		{
#if UNITY_EDITOR
			var timer = new IntervalTimer { Interval = -1f };
			Assert.Throws<ArgumentException>(() => { timer.Start(); });
#endif
		}

		[TestCase(0f)] [TestCase(1.1f)] [TestCase(2.00009f)]
		public void Increment_IntervalTimes_IsElapsed(Single interval)
		{
			var timer = new IntervalTimer { Interval = interval };
			timer.Start();

			var count = interval * 10f;
			for (var i = 0; i < count; i++)
			{
				Assert.False(timer.IsElapsed);
				timer.Decrement(.1f);
			}

			Assert.True(timer.IsElapsed);

			timer.Decrement(.1f); // test overshoot still elapsed
			Assert.True(timer.IsElapsed);
		}

		[TestCase(0f, 1f)] [TestCase(1.2f, 2.3f)] [TestCase(2.1f, 3.13f)]
		public void Increment_MoreThanIntervalTimes_IsElapsed(Single interval, Single overshoot)
		{
			var timer = new IntervalTimer { Interval = interval };
			timer.Start();

			for (var i = 0; i < interval * 10f + overshoot; i++)
				timer.Decrement(.1f);

			Assert.True(timer.IsElapsed);
		}

		[TestCase(.1f)] [TestCase(1.2f)] [TestCase(3f)]
		public void Start_AfterIncrementUntilElapsed_IsNotElapsed(Single interval)
		{
			var timer = new IntervalTimer { Interval = interval };
			timer.Start();

			for (var i = 0; i < interval * 10f; i++)
				timer.Decrement(.1f);

			timer.Start();
			Assert.False(timer.IsElapsed);
		}

		[TestCase(0f)] [TestCase(.1f)] [TestCase(123f)]
		public void StartElapsed_IsElapsed(Single interval)
		{
			var timer = new IntervalTimer { Interval = interval };
			timer.StartElapsed();

			Assert.True(timer.IsElapsed);
		}
	}
}
