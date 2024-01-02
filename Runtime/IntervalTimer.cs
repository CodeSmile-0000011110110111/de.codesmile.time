// Copyright (C) 2021-2023 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using UnityEngine;

namespace CodeSmile.Time
{
	[Serializable] public struct IntervalTimer
	{
		[Tooltip("Time in seconds until elapsed.")]
		public Single Interval;
		private Single m_Timer;

		/// <summary>
		///     State of the internal Timer towards the goal (0f or negative).
		/// </summary>
		/// <remarks>
		///     The internal timer runs from Interval down towards 0, and might even go negative. Benefits:
		///     - Timer is unaffected by changes to Interval until restarted.
		///     - Timer value speaks for itself: value '1.4' reads as '1.4 seconds until elapsed'
		///     - Timer value 0 or less means the timer is elapsed.
		/// </remarks>
		public Single Timer => m_Timer;

		/// <summary>
		///     Returns true if the timer has elapsed, or started as elapsed.
		/// </summary>
		public Boolean IsElapsed => m_Timer <= 0f;
		public Boolean IsStopped => m_Timer > Interval;
		public Boolean IsRunning => m_Timer <= Interval;

		/// <summary>
		///     Starts (restarts) the timer.
		/// </summary>
		/// <exception cref="ArgumentException">If Interval is negative (editor only)</exception>
		public void Start()
		{
#if UNITY_EDITOR
			if (Interval < 0f) throw new ArgumentException($"Interval is negative: {Interval}");
#endif
			m_Timer = Interval;
		}

		public void Stop() => m_Timer = Single.MaxValue;

		/// <summary>
		///     Starts the timer in elapsed state. Sets Timer to 0f.
		/// </summary>
		/// <remarks>
		///     Use this where you need a timer to fire on its first use rather than waiting out the first
		///     Interval. For example a weapon should fire right after the reload timer/counter has elapsed
		///     but subsequently should fire only at the Interval rate.
		/// </remarks>
		public void StartElapsed() => m_Timer = 0f;

		/// <summary>
		///     Decrements the timer by the amount of delta time.
		/// </summary>
		/// <exception cref="ArgumentException">If deltaTime is negative (editor only).</exception>
		/// <returns>True if the timer is elapsed, false otherwise.</returns>
		public Boolean Decrement(Single deltaTime)
		{
#if UNITY_EDITOR
			if (deltaTime < 0f) throw new ArgumentException($"DeltaTime is negative: {deltaTime}");
			if (IsStopped) throw new InvalidOperationException("Timer is stopped: Call Start() before Decrement()");
#endif

			m_Timer -= deltaTime;
			return IsElapsed;
		}

		public Boolean DecrementIfRunning(Single deltaTime) => IsRunning && Decrement(deltaTime);
	}
}
