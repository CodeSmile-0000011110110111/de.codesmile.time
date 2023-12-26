// Copyright (C) 2021-2023 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using UnityEngine;

namespace CodeSmile.Time
{
	[Serializable] public struct IntervalTimer
	{
		[Tooltip("Time interval in seconds.")]
		public Single Interval;
		private Single m_Timer;

		public void Start() => m_Timer = 0f;
		public void Elapse() => m_Timer = Interval;
		public Boolean IsElapsed(Single deltaTime) => (m_Timer += deltaTime) >= Interval;
	}
}
