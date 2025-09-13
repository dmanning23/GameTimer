using Microsoft.Xna.Framework;

namespace GameTimer
{
	/// <summary>
	/// this is a game clock with the ability to do hit pause
	/// </summary>
	public class HitPauseClock : GameClock
	{
		#region Members

		//timer for doing hit pause
		protected CountdownTimer HitPause { get; set; }

		#endregion //Members

		#region Methods

		public HitPauseClock()
		{
			HitPause = new CountdownTimer();
		}

		/// <summary>
		/// Pause the character clock for hit pause
		/// </summary>
		/// <param name="hitPauseTimeDelta">the amount of time to hit pause for</param>
		public void AddHitPause(float hitPauseTimeDelta)
		{
			HitPause.Start(hitPauseTimeDelta);
			Paused = HitPause.HasTimeRemaining;
		}

		/// <summary>
		/// Update this clock... this is actually the only one I'm sure works
		/// </summary>
		/// <param name="clock"></param>
		public override void Update(GameClock clock)
		{
			//update the hit pause first to tell if we are paused
			HitPause.Update(clock);
			Paused = clock.Paused || HitPause.HasTimeRemaining;

			base.Update(clock);
		}

		public override void Update(TimeUpdater clock)
		{
			HitPause.Update(clock);
			Paused = HitPause.HasTimeRemaining;

			base.Update(clock);
		}

		public override void Update(GameTime currentTime)
		{
			HitPause.Update(currentTime);
			Paused = HitPause.HasTimeRemaining;

			base.Update(currentTime);
		}

		public override void Update(float currentTime)
		{
			HitPause.Update(currentTime);
			Paused = HitPause.HasTimeRemaining;

			base.Update(currentTime);
		}

		#endregion //Methods
	}
}