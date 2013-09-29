using NUnit.Framework;
using System;
using GameTimer;
using Microsoft.Xna.Framework;

namespace GameTimer.Tests
{
	[TestFixture()]
	public class GameClockTests
	{
		#region Setup

		GameClock test;

		[SetUp()]
		public void Setup()
		{
			test = new GameClock();
		}

		#endregion //Setup

		#region Defaults

		[Test()]
		public void DefaultCurrentTime()
		{
			Assert.AreEqual(0.0f, test.CurrentTime);
		}

		[Test()]
		public void DefaultCurrentTime1()
		{
			Assert.AreEqual(0.0f, test.GetCurrentTime());
		}

		[Test()]
		public void DefaultTimeDelta()
		{
			Assert.AreEqual(0.0f, test.TimeDelta);
		}

		[Test()]
		public void DefaultPause()
		{
			Assert.AreEqual(false, test.Paused);
		}

		[Test()]
		public void DefaultTimerSpeed()
		{
			Assert.AreEqual(1.0f, test.TimerSpeed);
		}

		#endregion //Defaults

		#region Start Tests

		[Test()]
		public void StartCurrentTime()
		{
			test.CurrentTime = 1.0f;
			test.Start();
			Assert.AreEqual(0.0f, test.CurrentTime);
		}

		[Test()]
		public void StartCurrentTime1()
		{
			test.CurrentTime = 1.0f;
			test.Start();
			Assert.AreEqual(0.0f, test.GetCurrentTime());
		}

		[Test()]
		public void StartTimeDelta()
		{
			test.TimeDelta = 1.0f;
			test.Start();
			Assert.AreEqual(1.0f, test.TimeDelta);
		}

		[Test()]
		public void StartPause()
		{
			test.Paused = true;
			test.Start();
			Assert.AreEqual(false, test.Paused);
		}

		[Test()]
		public void StartTimerSpeed()
		{
			test.TimerSpeed = 2.0f;
			test.Start();
			Assert.AreEqual(2.0f, test.TimerSpeed);
		}

		#endregion //Defaults

		#region Frames

		[Test()]
		public void FramesToSeconds()
		{
			Assert.AreEqual(1.0f, GameClock.FramesToSeconds(60));
			Assert.AreEqual(0.5f, GameClock.FramesToSeconds(30));
			Assert.AreEqual(2.0f, GameClock.FramesToSeconds(120));
		}

		[Test()]
		public void SecondsToFrames()
		{
			Assert.AreEqual(60, GameClock.SecondsToFrames(1.0f));
			Assert.AreEqual(30, GameClock.SecondsToFrames(0.5f));
			Assert.AreEqual(120, GameClock.SecondsToFrames(2.0f));
		}

		#endregion Frames

		#region String methods

		[Test()]
		public void TwoHours()
		{
			Assert.AreEqual("2:00:00", GameClock.ToTimeString(7200.0f));
		}

		[Test()]
		public void SixtyMinutes()
		{
			Assert.AreEqual("1:00:00", GameClock.ToTimeString(3600.0f));
		}

		[Test()]
		public void ThirtyMinutes()
		{
			Assert.AreEqual("30:00", GameClock.ToTimeString(1800.0f));
		}

		[Test()]
		public void SixtyOneMinutes()
		{
			Assert.AreEqual("31:00", GameClock.ToTimeString(1860.0f));
		}

		[Test()]
		public void SixtySeconds()
		{
			Assert.AreEqual("1:00", GameClock.ToTimeString(60.0f));
		}

		[Test()]
		public void SixtyOneSeconds()
		{
			Assert.AreEqual("1:01", GameClock.ToTimeString(61.0f));
		}

		[Test()]
		public void ThirtySeconds()
		{
			Assert.AreEqual("0:30", GameClock.ToTimeString(30.0f));
		}

		[Test()]
		public void FiveSeconds()
		{
			Assert.AreEqual("0:05", GameClock.ToTimeString(5.0f));
		}

		[Test()]
		public void TwelveHoursThirtyFourMinutesFiftySixSeconds()
		{

			Assert.AreEqual("12:34:56", GameClock.ToTimeString(43200.0f + 2040.0f + 56.0f));
		}

		[Test()]
		public void GameClockToString()
		{
			test.CurrentTime = (43200.0f + 2040.0f + 56.0f);
			Assert.AreEqual("12:34:56", test.ToString());
		}

		#endregion //String methods

		#region Update from gametimer

		[Test()]
		public void UpdateFromGameTimerCurrent()
		{
			GameClock test2 = new GameClock();
			test2.TimeDelta = 0.5f;
			test.Update(test2);
			Assert.AreEqual(0.5f, test.CurrentTime);
		}

		[Test()]
		public void UpdateFromGameTimerCurrent1()
		{
			GameClock test2 = new GameClock();
			test2.TimeDelta = 0.5f;
			test.Update(test2);
			Assert.AreEqual(0.5f, test.GetCurrentTime());
		}

		[Test()]
		public void UpdateFromGameTimerDelta()
		{
			GameClock test2 = new GameClock();
			test2.TimeDelta = 0.5f;
			test.Update(test2);
			Assert.AreEqual(0.5f, test.TimeDelta);
		}

		[Test()]
		public void UpdateFromGameTimerCurrentMultiple()
		{
			GameClock test2 = new GameClock();
			test2.TimeDelta = 0.5f;
			test.Update(test2);
			test.Update(test2);
			Assert.AreEqual(1.0f, test.CurrentTime);
		}

		[Test()]
		public void UpdateFromGameTimerDeltaMultiple()
		{
			GameClock test2 = new GameClock();
			test2.TimeDelta = 0.5f;
			test.Update(test2);
			test.Update(test2);
			Assert.AreEqual(0.5f, test.TimeDelta);
		}

		[Test()]
		public void UpdateFromGameTimerPaused()
		{
			test.Paused = true;
			GameClock test2 = new GameClock();
			test2.TimeDelta = 0.5f;
			test.Update(test2);
			test.Update(test2);
			Assert.AreEqual(0.0f, test.CurrentTime);
		}

		[Test()]
		public void UpdateFromGameTimerPaused1()
		{
			GameClock test2 = new GameClock();
			test2.TimeDelta = 0.5f;
			test.Update(test2);
			test.Paused = true;
			test.Update(test2);
			Assert.AreEqual(0.5f, test.CurrentTime);
		}

		[Test()]
		public void UpdateFromGameTimerFast()
		{
			test.TimerSpeed = 2.0f;
			GameClock test2 = new GameClock();
			test2.TimeDelta = 0.5f;
			test.Update(test2);
			Assert.AreEqual(1.0f, test.TimeDelta);
		}

		#endregion //Update from gametimer
	}
}
