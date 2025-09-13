
namespace GameTimer
{
	/// <summary>
	/// This class countsdown to 0
	/// </summary>
	public class CountdownTimer : GameClock
	{
		#region Members

		/// <summary>
		/// the amount of time to run this eggtimer for
		/// </summary>
		public float CountdownLength { get; set; }

		/// <summary>
		/// the time this eggtimer was started
		/// </summary>
		public float StartTime { get; protected set; }

		/// <summary>
		/// get the amount of time remaining on this egg timer
		/// </summary>
		/// <returns>the number of seconds left</returns>
		public float RemainingTime
		{
			get
			{
				return CountdownLength - (CurrentTime - StartTime);
			}
		}

		/// <summary>
		/// check if there is any time left on this timer
		/// </summary>
		/// <returns></returns>
		public bool HasTimeRemaining
		{
			get
			{
				return 0.0f < RemainingTime;
			}
		}

		/// <summary>
		/// Get the percentage of time remaining on this timer.
		/// 1.0 is all the time remaining, 0.0 is time is done
		/// </summary>
		/// <returns>float: the percentage of time remaining from 1.0 -> 0.0</returns></returns>
		public float Lerp
		{
			get
			{
				//guard against divide by 0
				if (0.0 < CountdownLength)
				{
					return RemainingTime / CountdownLength;
				}
				else
				{
					//this timer is done
					return 0.0f;
				}
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// constructor
		/// </summary>
		public CountdownTimer()
		{
			CountdownLength = 0.0f;
			StartTime = 0.0f;
		}

		/// <summary>
		/// start the egg timer!
		/// </summary>
		/// <param name="seconds">how long to run this timer</param>
		public void Start(float seconds)
		{
			base.Start();
			CountdownLength = seconds;
			StartTime = CurrentTime;
		}

		/// <summary>
		/// start the egg timer!
		/// </summary>
		/// <param name="seconds">how long to run this timer</param>
		/// <param name="startTime"></param>
		public void Start(float seconds, float startTime)
		{
			base.Start();
			CountdownLength = seconds;
			CurrentTime = startTime;
			StartTime = CurrentTime;
		}

		/// <summary>
		/// start the egg timer!
		/// </summary>
		public override void Start()
		{
			base.Start();
			StartTime = CurrentTime;
		}

		/// <summary>
		/// Set a stopwatch so that it will return a negative time (stopped)
		/// </summary>
		public override void Stop()
		{
			base.Stop();
			CountdownLength = 0.0f;
			StartTime = 0.0f;
		}

		/// <summary>
		/// Add some time to a time that is currently counting down
		/// </summary>
		/// <param name="time"></param>
		public void AddTime(float time)
		{
			CountdownLength += time;
		}

		public float LerpValues(float start, float end)
		{
			if (HasTimeRemaining)
			{
				//guard against divide by 0
				return end - ((end - start) * Lerp);
			}
			else
			{
				return end;
			}
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return GameClock.ToTimeString(RemainingTime);
		}

		#endregion //Methods
	}
}