// Copyright (C) 2021-2023 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

namespace CodeSmile.Tests
{
	public class TimerTests
	{
		[NUnit.Framework.Test]
		public void TimeTestsSimplePasses()
		{
			// Use the Assert class to test conditions.

		}

		// A UnityTest behaves like a coroutine in PlayMode
		// and allows you to yield null to skip a frame in EditMode
		[UnityEngine.TestTools.UnityTest]
		public System.Collections.IEnumerator TimeTestsWithEnumeratorPasses()
		{
			// Use the Assert class to test conditions.
			// yield to skip a frame
			yield return null;
		}
	}
}
