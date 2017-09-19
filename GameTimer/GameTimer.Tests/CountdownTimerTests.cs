using Microsoft.Xna.Framework;
using NUnit.Framework;
using System;

namespace GameTimer.Tests
{
	[TestFixture]
	public class CountdownTimerTests
	{
		[Test]
		public void Start_RemainingTime()
		{
			var timer = new CountdownTimer();
			timer.Start(1f);
			Assert.AreEqual(1, timer.RemainingTime());
		}

		[Test]
		public void Start_Lerp()
		{
			var timer = new CountdownTimer();
			timer.Start(1f);
			Assert.AreEqual(1, timer.Lerp());
		}

		[Test]
		public void Half_RemainingTime()
		{
			var timer = new CountdownTimer();
			timer.Start(1f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 0, 0, 500)));

			Assert.AreEqual(0.5f, timer.RemainingTime());
		}

		[Test]
		public void Half_Lerp()
		{
			var timer = new CountdownTimer();
			timer.Start(1f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 0, 0, 500)));

			Assert.AreEqual(0.5f, timer.Lerp());
		}

		[Test]
		public void Quarter_RemainingTime()
		{
			var timer = new CountdownTimer();
			timer.Start(1f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 250), new TimeSpan(0, 0, 0, 0, 250)));

			Assert.AreEqual(0.75f, timer.RemainingTime());
		}

		[Test]
		public void Quarter_Lerp()
		{
			var timer = new CountdownTimer();
			timer.Start(1f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 250), new TimeSpan(0, 0, 0, 0, 250)));

			Assert.AreEqual(0.75f, timer.Lerp());
		}

		[Test]
		public void Quarter_LotsRemainingTime()
		{
			var timer = new CountdownTimer();
			timer.Start(4f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 1), new TimeSpan(0, 0, 0, 1)));

			Assert.AreEqual(3f, timer.RemainingTime());
		}

		[Test]
		public void Quarter_LotsLerp()
		{
			var timer = new CountdownTimer();
			timer.Start(4f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 1), new TimeSpan(0, 0, 0, 1)));

			Assert.AreEqual(0.75f, timer.Lerp());
		}

		[Test]
		public void Started()
		{
			var timer = new CountdownTimer();
			timer.Start(4f);

			Assert.IsTrue(timer.HasTimeRemaining());
		}

		[Test]
		public void Stopped()
		{
			var timer = new CountdownTimer();
			timer.Start(4f);
			timer.Stop();

			Assert.IsFalse(timer.HasTimeRemaining());
		}
	}
}
