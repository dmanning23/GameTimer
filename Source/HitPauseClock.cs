using System.Diagnostics;
using Microsoft.Xna.Framework;
#if !NO_NETWORKING
using Microsoft.Xna.Framework.Net;
#endif

namespace GameTimer
{
	/// <summary>
	/// this is a game clock with the ability to do hit pause
	/// </summary>
	public class HitPauseClock : GameClock
	{
		#region Members

		//timer for doing hit pause
		CountdownTimer m_HitPause;

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

		#region Networking

#if !NO_NETWORKING

		/// <summary>
		/// Read this object from a network packet reader.
		/// </summary>
		public override void ReadFromNetwork(PacketReader packetReader)
		{
			base.ReadFromNetwork(packetReader);
			m_HitPause.ReadFromNetwork(packetReader);
		}

		/// <summary>
		/// Write this object to a network packet reader.
		/// </summary>
		public override void WriteToNetwork(PacketWriter packetWriter)
		{
			base.WriteToNetwork(packetWriter);
			m_HitPause.WriteToNetwork(packetWriter);
		}

#endif

		#endregion //Networking
	}
}