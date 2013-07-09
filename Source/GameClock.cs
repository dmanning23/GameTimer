using Microsoft.Xna.Framework;
#if NETWORKING
using Microsoft.Xna.Framework.Net;
#endif

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
		public float TimerSpeed { protected get; set; }

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

		/// <summary>
		/// Start the game timer.
		/// </summary>
		public virtual void Start()
		{
			CurrentTime = 0.0f;
			Paused = false;
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
			return ((float)iFrames / 60.0f);
		}

		/// <summary>
		/// A method to be used in a delegate
		/// </summary>
		/// <returns></returns>
		public float GetCurrentTime()
		{
			return CurrentTime;
		}

		#endregion

		#region Networking

#if NETWORKING

		/// <summary>
		/// Read this object from a network packet reader.
		/// </summary>
		public virtual void ReadFromNetwork(PacketReader packetReader)
		{
			CurrentTime = packetReader.ReadSingle();
			Paused = packetReader.ReadBoolean();
		}

		/// <summary>
		/// Write this object to a network packet reader.
		/// </summary>
		public virtual void WriteToNetwork(PacketWriter packetWriter)
		{
			packetWriter.Write(CurrentTime);
			packetWriter.Write(Paused);
		}

#endif

		#endregion //Networking
	}
}