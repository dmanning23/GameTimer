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
		private readonly CountdownTimer m_HitPause;

		#endregion //Members

		#region Methods

		public HitPauseClock()
		{
			m_HitPause = new CountdownTimer();
		}

		/// <summary>
		/// Pause the character clock for hit pause
		/// </summary>
		/// <param name="fTimeDelta">the amount of time to hit pause for</param>
		public void AddHitPause(float fTimeDelta)
		{
			m_HitPause.Start(fTimeDelta);
		}

		/// <summary>
		/// Update this clock... this is actually the only one I'm sure works
		/// </summary>
		/// <param name="rInst"></param>
		public override void Update(GameClock rInst)
		{
			//update the hit pause first to tell if we are paused
			m_HitPause.Update(rInst);
			Paused = rInst.Paused || (0.0f < m_HitPause.RemainingTime());

			base.Update(rInst);
		}

		public override void Update(TimeUpdater fCurrentTime)
		{
			m_HitPause.Update(fCurrentTime);
			Paused = (0.0f < m_HitPause.RemainingTime());

			base.Update(fCurrentTime);
		}

		public override void Update(GameTime CurrentTime)
		{
			m_HitPause.Update(CurrentTime);
			Paused = (0.0f < m_HitPause.RemainingTime());

			base.Update(CurrentTime);
		}

		public override void Update(float fCurrentTime)
		{
			m_HitPause.Update(fCurrentTime);
			Paused = (0.0f < m_HitPause.RemainingTime());

			base.Update(fCurrentTime);
		}

		#endregion //Methods
	}
}