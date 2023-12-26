// Copyright (C) 2021-2023 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using CodeSmile.Time;
using NUnit.Framework;
using System;

namespace CodeSmile.Tests
{
	public class CounterTests
	{
		[Test] public void DefaultCtor_IsElapsed()
		{
			var counter = new IntervalCounter();

			Assert.True(counter.IsElapsed);
		}

		[Test] public void Start_NegativeInterval_Throws()
		{
#if UNITY_EDITOR
			var counter = new IntervalCounter { Interval = -1 };
			Assert.Throws<ArgumentException>(() => { counter.Start(); });
#endif
		}

		[TestCase(0)] [TestCase(1)] [TestCase(2)]
		public void Increment_IntervalTimes_IsElapsed(Int32 interval)
		{
			var counter = new IntervalCounter { Interval = interval };
			counter.Start();

			for (var i = 0; i < interval; i++)
			{
				Assert.False(counter.IsElapsed);
				counter.Decrement();
			}

			Assert.True(counter.IsElapsed);

			counter.Decrement(); // test overshoot still elapsed
			Assert.True(counter.IsElapsed);
		}

		[TestCase(0, 1)] [TestCase(1, 2)] [TestCase(2, 5)]
		public void Increment_MoreThanIntervalTimes_IsElapsed(Int32 interval, int overshoot)
		{
			var counter = new IntervalCounter { Interval = interval };
			counter.Start();

			for (var i = 0; i < interval + overshoot; i++)
			{
				counter.Decrement();
			}

			Assert.True(counter.IsElapsed);
		}

		[TestCase(1)] [TestCase(2)] [TestCase(3)]
		public void Start_AfterIncrementUntilElapsed_IsNotElapsed(Int32 interval)
		{
			var counter = new IntervalCounter { Interval = interval };
			counter.Start();

			for (var i = 0; i < interval; i++)
				counter.Decrement();

			counter.Start();
			Assert.False(counter.IsElapsed);
		}

		[TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(345)]
		public void StartElapsed_IsElapsed(Int32 interval)
		{
			var counter = new IntervalCounter { Interval = interval };
			counter.StartElapsed();

			Assert.True(counter.IsElapsed);
		}
	}
}
