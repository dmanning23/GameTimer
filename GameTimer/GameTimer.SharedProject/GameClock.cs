using System.Text;
using Microsoft.Xna.Framework;
using System;

namespace GameTimer
{
	public class GameClock
	{
		#region Properties

		/// <summary>
		/// The current time of this clock in seconds
		/// </summary>
		public float CurrentTime { get; set; }

		/// <summary>
		/// The time delta since last frame in seconds
		/// </summary>
		public float TimeDelta { get; set; }

		/// <summary>
		/// whether or not this dude is paused
		/// </summary>
		public bool Paused { get; set; }

		/// <summary>
		/// Defaults to 1.0f, used to speed up or slow down the timer.  
		/// This will also affect all the timers down stream when they are updated off this timer
		/// </summary>
		public float TimerSpeed { get; set; }

		public float PreviousTime
		{
			get
			{
				float prevTime = (CurrentTime - TimeDelta);

				//dont return less than 0 for time
				return Math.Max(prevTime, 0f);
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// hello, standard constructor!
		/// </summary>
		public GameClock()
		{
			CurrentTime = 0.0f;
			TimeDelta = 0.0f;
			Paused = false;
			TimerSpeed = 1.0f;
		}

		public GameClock(GameClock inst)
		{
			CurrentTime = inst.CurrentTime;
			TimeDelta = inst.TimeDelta;
			Paused = inst.Paused;
			TimerSpeed = inst.TimerSpeed;
		}

		/// <summary>
		/// Start the game timer.
		/// </summary>
		public virtual void Start()
		{
			CurrentTime = 0.0f;
			Paused = false;
		}

		/// <summary>
		/// another way to update time, via time update object
		/// </summary>
		/// <param name="currentTime">the current time in seconds</param>
		public virtual void Update(TimeUpdater currentTime)
		{
			Update(currentTime.CurrentTime);
		}

		/// <summary>
		/// Update the time of this clock
		/// </summary>
		/// <param name="currentTime">The gametime object sent by xna to the app</param>
		public virtual void Update(GameTime currentTime)
		{
			if (!Paused)
			{
				//Get the time delta
				TimeDelta = currentTime.ElapsedGameTime.Seconds;
				var milliseconds = currentTime.ElapsedGameTime.Milliseconds;
				TimeDelta += (milliseconds / 1000.0f);

				//update the delta by our speed multiplier
				TimeDelta *= TimerSpeed;

				//set the current time
				CurrentTime += TimeDelta;
			}
			else
			{
				TimeDelta = 0.0f;
			}
		}

		/// <summary>
		/// another way to update the clock, via another game clock
		/// </summary>
		/// <param name="inst">the clock that we will use to update this dude</param>
		public virtual void Update(GameClock inst)
		{
			if (!Paused)
			{
				TimeDelta = inst.TimeDelta;

				//update the delta by our speed multiplier
				TimeDelta *= TimerSpeed;

				CurrentTime += TimeDelta;
			}
			else
			{
				TimeDelta = 0.0f;
			}
		}

		/// <summary>
		/// another way to update time, via plain ol float
		/// </summary>
		/// <param name="currentTime">the current time in seconds</param>
		public virtual void Update(float currentTime)
		{
			if (!Paused)
			{
				TimeDelta = currentTime - CurrentTime;

				//update the delta by our speed multiplier
				TimeDelta *= TimerSpeed;

				CurrentTime = currentTime;
			}
			else
			{
				TimeDelta = 0.0f;
			}
		}

		/// <summary>
		/// another way to update time, via plain ol int
		/// </summary>
		/// <param name="fCurrentTime">the current time in frames (1/60 second)</param>
		public virtual void Update(int currentTime)
		{
			Update(GameClock.FramesToSeconds(currentTime));
		}

		/// <summary>
		/// Subtract some time from this timer
		/// </summary>
		/// <param name="timeDelta">the amount of time to remove</param>
		public void SubtractTime(float timeDelta)
		{
			CurrentTime -= timeDelta;
		}

		/// <summary>
		/// Add some time to this timer
		/// </summary>
		/// <param name="timeDelta">the amount of time to add</param>
		public void AppendTime(float timeDelta)
		{
			CurrentTime += timeDelta;
		}

		/// <summary>
		/// Set a stopwatch so that it will return a negative time (stopped)
		/// </summary>
		public virtual void Stop()
		{
			CurrentTime = 0.0f;
			TimeDelta = 0.0f;
			Paused = true;
		}

		/// <summary>
		/// convert seconds to frames
		/// </summary>
		/// <param name="seconds">the time in floating point seconds</param>
		/// <returns>a whole number representing the current time in 1/60 second increments</returns>
		public static int SecondsToFrames(float seconds)
		{
			//at .5 so it will round up when it chops off the decimals
			return (int)((seconds * 60.0f) + 0.5f);
		}

		/// <summary>
		/// convert frames to seconds
		/// </summary>
		/// <param name="frames">the time to convert in 1/60 second increments</param>
		/// <returns>floating point time</returns>
		public static float FramesToSeconds(int frames)
		{
			return (frames / 60.0f);
		}

		/// <summary>
		/// A method to be used in a delegate
		/// </summary>
		/// <returns></returns>
		public float GetCurrentTime()
		{
			return CurrentTime;
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return GameClock.ToTimeString(GetCurrentTime());
		}

		/// <summary>
		/// Get the remaining time as a human readable string
		/// </summary>
		/// <returns>The time string.</returns>
		/// <param name="time">the time to convert to a string</param>
		public static string ToTimeString(float time)
		{
			//stringbuilder to hold our text
			var timeText = new StringBuilder();

			//Get the number of hours
			var hours = (int)(time / 3600.0f);
			if (0 < hours)
			{
				//Add the number of hours to the string
				timeText.AppendFormat("{0}:", hours.ToString());

				//subtract the number of hours from the time
				time -= hours * 3600;
			}

			//get the number of minutes
			var minutes = (int)(time / 60.0f);

			//add a 0 if there are hours on the clock but single digit minutes
			if ((0 < hours) && (minutes < 10))
			{
				timeText.AppendFormat("0");
			}

			//add the number of minutes to the string
			timeText.AppendFormat("{0}:", minutes.ToString());

			//add the number of seconds
			var seconds = (int)(time % 60.0f);
			if (seconds < 10)
			{
				timeText.AppendFormat("0");
			}
			timeText.Append(seconds.ToString());

			return timeText.ToString();
		}

		#endregion
	}
}