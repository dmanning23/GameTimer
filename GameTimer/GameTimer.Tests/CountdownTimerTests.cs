using Microsoft.Xna.Framework;
using NUnit.Framework;
using Shouldly;
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
			Assert.AreEqual(1, timer.RemainingTime);
		}

		[Test]
		public void Start_Lerp()
		{
			var timer = new CountdownTimer();
			timer.Start(1f);
			Assert.AreEqual(1, timer.Lerp);
		}

		[Test]
		public void Half_RemainingTime()
		{
			var timer = new CountdownTimer();
			timer.Start(1f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 0, 0, 500)));

			Assert.AreEqual(0.5f, timer.RemainingTime);
		}

		[Test]
		public void Half_Lerp()
		{
			var timer = new CountdownTimer();
			timer.Start(1f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 0, 0, 500)));

			Assert.AreEqual(0.5f, timer.Lerp);
		}

		[Test]
		public void Quarter_RemainingTime()
		{
			var timer = new CountdownTimer();
			timer.Start(1f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 250), new TimeSpan(0, 0, 0, 0, 250)));

			Assert.AreEqual(0.75f, timer.RemainingTime);
		}

		[Test]
		public void Quarter_Lerp()
		{
			var timer = new CountdownTimer();
			timer.Start(1f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 250), new TimeSpan(0, 0, 0, 0, 250)));

			Assert.AreEqual(0.75f, timer.Lerp);
		}

		[Test]
		public void Quarter_LotsRemainingTime()
		{
			var timer = new CountdownTimer();
			timer.Start(4f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 1), new TimeSpan(0, 0, 0, 1)));

			Assert.AreEqual(3f, timer.RemainingTime);
		}

		[Test]
		public void Quarter_LotsLerp()
		{
			var timer = new CountdownTimer();
			timer.Start(4f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 1), new TimeSpan(0, 0, 0, 1)));

			Assert.AreEqual(0.75f, timer.Lerp);
		}

		[Test]
		public void Started()
		{
			var timer = new CountdownTimer();
			timer.Start(4f);

			Assert.IsTrue(timer.HasTimeRemaining);
		}

		[Test]
		public void Stopped()
		{
			var timer = new CountdownTimer();
			timer.Start(4f);
			timer.Stop();

			Assert.IsFalse(timer.HasTimeRemaining);
		}

		[Test]
		public void Start_LerpValues()
		{
			var timer = new CountdownTimer();
			timer.Start(1f);
			timer.LerpValues(100, 200).ShouldBe(100);
		}

		[Test]
		public void Quarter_LerpValues()
		{
			var timer = new CountdownTimer();
			timer.Start(4f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 1), new TimeSpan(0, 0, 0, 1)));

			timer.LerpValues(100, 200).ShouldBe(125);
		}

		[Test]
		public void Quarter_LotsLerpValues()
		{
			var timer = new CountdownTimer();
			timer.Start(4f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 3), new TimeSpan(0, 0, 0, 3)));

			timer.LerpValues(100, 200).ShouldBe(175);
		}

		[Test]
		public void Start_LerpValues2()
		{
			var timer = new CountdownTimer();
			timer.Start(1f);
			timer.LerpValues(200, 100).ShouldBe(200);
		}

		[Test]
		public void Quarter_LerpValues2()
		{
			var timer = new CountdownTimer();
			timer.Start(4f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 1), new TimeSpan(0, 0, 0, 1)));

			timer.LerpValues(200, 100).ShouldBe(175);
		}

		[Test]
		public void Quarter_LotsLerpValues2()
		{
			var timer = new CountdownTimer();
			timer.Start(4f);

			timer.Update(new GameTime(new TimeSpan(0, 0, 0, 3), new TimeSpan(0, 0, 0, 3)));

			timer.LerpValues(200, 100).ShouldBe(125);
		}
	}
}
