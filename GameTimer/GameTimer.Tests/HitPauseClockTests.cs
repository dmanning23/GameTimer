using NUnit.Framework;
using Shouldly;
using System;

namespace GameTimer.Tests
{
	[TestFixture()]
	public class HitPauseClockTests
	{
		HitPauseClock test;

		[SetUp()]
		public void Setup()
		{
			test = new HitPauseClock();
		}

		[Test()]
		public void HitPause_DefaultPause()
		{
			test.Paused.ShouldBeFalse();
		}

		[Test()]
		public void HitPause_Pause()
		{
			test.Paused = true;
			test.Paused.ShouldBeTrue();
		}

		[Test()]
		public void HitPause_PauseStart()
		{
			test.Paused = true;
			test.Start();
			test.Paused.ShouldBeFalse();
		}

		[Test()]
		public void HitPause_UpdateFromGameTimerCurrent_NotPaused()
		{
			GameClock test2 = new GameClock();
			test2.TimeDelta = 0.5f;
			test.Update(test2);
			test.Paused.ShouldBeFalse();
		}

		[Test()]
		public void HitPause_UpdateFromGameTimerCurrent()
		{
			GameClock test2 = new GameClock();
			test2.TimeDelta = 0.5f;
			test.Update(test2);
			test.CurrentTime.ShouldBe(0.5f);
		}

		[Test()]
		public void HitPause_AddedHitPause()
		{
			test.AddHitPause(0.25f);
			test.Paused.ShouldBeTrue();
		}

		[Test()]
		public void HitPause_UpdateHitPause()
		{
			test.AddHitPause(0.25f);
			GameClock test2 = new GameClock();
			test2.TimeDelta = 0.1f;
			test.Paused.ShouldBeTrue();
		}

		[Test()]
		public void HitPause_HitPauseDone()
		{
			test.AddHitPause(0.25f);
			GameClock test2 = new GameClock();
			test2.TimeDelta = 0.5f;
			test.Update(test2);
			test.Paused.ShouldBeFalse();
		}

		[Test()]
		public void HitPause_HitPauseDone_TimeDelta()
		{
			test.AddHitPause(0.25f);
			GameClock test2 = new GameClock();
			test2.TimeDelta = 0.2f;
			test.Update(test2);
			test.Update(test2);
			test.CurrentTime.ShouldBe(0.2f);
		}
	}
}

