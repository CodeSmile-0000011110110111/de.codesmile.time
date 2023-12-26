// Copyright (C) 2021-2023 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using UnityEngine;

namespace CodeSmile.Time
{
	/// <summary>
	///     Counter that counts integer increments and is elapsed when the Interval is reached.
	/// </summary>
	[Serializable] public struct IntervalCounter
	{
		/// <summary>
		///     How often the counter needs to be incremented before it is considered elapsed.
		/// </summary>
		/// <remarks>
		///     Changing this value while the counter is started will not influence the current interval.
		///     For the new interval to take effect you have to call Start() or StartElapsed().
		///     This is useful to make interval changes right away without having to wait for the counter to elapse.
		/// </remarks>
		[Tooltip("How often the counter needs to increment until it's elapsed. Must be 0 or positive.")]
		public Int32 Interval;
		private Int32 m_Counter;
		/// <summary>
		///     State of the internal Counter towards the goal (0).
		/// </summary>
		/// <remarks>
		///     The internal counter runs from Interval down towards 0, and might even go negative. Benefits:
		///     - Counter is unaffected by changes to Interval until restarted.
		///     - Counter value speaks for itself: value '4' can read as 'four more increments until elapsed'
		///     - Counter value 0 or negative values mean the counter is elapsed.
		///     - Negative counter values mean the counter decrement overshot n times (may indicate an issue)
		/// </remarks>
		public Int32 Counter => m_Counter;

		/// <summary>
		///     Returns true if the counter has elapsed, or started as elapsed.
		/// </summary>
		public Boolean IsElapsed => m_Counter <= 0;

		/// <summary>
		///     Starts (restarts) the counter. Call Decrement() Interval times to make it elapsed again.
		/// </summary>
		/// <exception cref="ArgumentException">If Interval is negative (editor only)</exception>
		public void Start()
		{
#if UNITY_EDITOR
			if (Interval < 0) throw new ArgumentException($"Interval is negative: {Interval}");
#endif
			m_Counter = Interval;
		}

		/// <summary>
		///     Starts the counter in elapsed state. Sets Counter to 0.
		/// </summary>
		/// <remarks>
		///     Use this where you need a counter to fire on its first use rather than waiting out the
		///     Interval first. For example a weapon should fire right after the reload timer/counter has elapsed
		///     but subsequently should fire only at the Interval rate.
		/// </remarks>
		public void StartElapsed() => m_Counter = 0;

		/// <summary>
		///     Decrements the counter by one.
		/// </summary>
		public void Decrement() => m_Counter--;
	}
}
