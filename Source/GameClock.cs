using System.Text;
using Microsoft.Xna.Framework;

namespace GameTimer
{
	public class GameClock
	{
		#region Member Variables

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

		#endregion //Member Variables

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
		/// <param name="fCurrentTime">the current time in seconds</param>
		public virtual void Update(TimeUpdater fCurrentTime)
		{
			Update(fCurrentTime.CurrentTime);
		}

		/// <summary>
		/// Update the time of this clock
		/// </summary>
		/// <param name="CurrentTime">The gametime object sent by xna to the app</param>
		public virtual void Update(GameTime rCurrentTime)
		{
			if (!Paused)
			{
				//Get the time delta
				TimeDelta = rCurrentTime.ElapsedGameTime.Seconds;
				float fMilliseconds = rCurrentTime.ElapsedGameTime.Milliseconds;
				TimeDelta += (fMilliseconds / 1000.0f);

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
		/// <param name="rInst">the clock that we will use to update this dude</param>
		public virtual void Update(GameClock rInst)
		{
			if (!Paused)
			{
				TimeDelta = rInst.TimeDelta;

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
		/// <param name="fCurrentTime">the current time in seconds</param>
		public virtual void Update(float fCurrentTime)
		{
			if (!Paused)
			{
				TimeDelta = fCurrentTime - CurrentTime;

				//update the delta by our speed multiplier
				TimeDelta *= TimerSpeed;

				CurrentTime = fCurrentTime;
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
		public virtual void Update(int iCurrentTime)
		{
			Update(GameClock.FramesToSeconds(iCurrentTime));
		}

		public float PreviousTime()
		{
			float PrevTime = (CurrentTime - TimeDelta);

			//dont return less than 0 for time
			return ((PrevTime >= 0.0f) ? PrevTime : 0.0f);
		}

		/// <summary>
		/// Subtract some time from this timer
		/// </summary>
		/// <param name="fTimeDelta">the amount of time to remove</param>
		public void SubtractTime(float fTimeDelta)
		{
			CurrentTime -= fTimeDelta;
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
		/// <param name="fSeconds">the time in floating point seconds</param>
		/// <returns>a whole number representing the current time in 1/60 second increments</returns>
		public static int SecondsToFrames(float fSeconds)
		{
			//at .5 so it will round up when it chops off the decimals
			return (int)((fSeconds * 60.0f) + 0.5f);
		}

		/// <summary>
		/// convert frames to seconds
		/// </summary>
		/// <param name="iFrames">the time to convert in 1/60 second increments</param>
		/// <returns>floating point time</returns>
		public static float FramesToSeconds(int iFrames)
		{
			return (iFrames / 60.0f);
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
		/// <param name="fTime">the time to convert to a string</param>
		public static string ToTimeString(float fTime)
		{
			//stringbuilder to hold our text
			var strTime = new StringBuilder();

			//Get the number of hours
			var iHours = (int)(fTime / 3600.0f);
			if (0 < iHours)
			{
				//Add the number of hours to the string
				strTime.AppendFormat("{0}:", iHours.ToString());

				//subtract the number of hours from the time
				fTime -= iHours * 3600;
			}

			//get the number of minutes
			var iMinutes = (int)(fTime / 60.0f);

			//add a 0 if there are hours on the clock but single digit minutes
			if ((0 < iHours) && (iMinutes < 10))
			{
				strTime.AppendFormat("0");
			}

			//add the number of minutes to the string
			strTime.AppendFormat("{0}:", iMinutes.ToString());

			//add the number of seconds
			var iSeconds = (int)(fTime % 60.0f);
			if (iSeconds < 10)
			{
				strTime.AppendFormat("0");
			}
			strTime.Append(iSeconds.ToString());

			return strTime.ToString();
		}

		#endregion
	}
}