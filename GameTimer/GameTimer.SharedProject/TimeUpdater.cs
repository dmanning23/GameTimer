using System;
using System.Diagnostics;

namespace GameTimer
{
	/// <summary>
	/// this class is for updating time in winform games
	/// </summary>
	public class TimeUpdater
	{
		#region Members

		/// <summary>
		/// the current time of this thing
		/// </summary>
		private float m_fCurrentTime;

		/// <summary>
		/// the time this thing was started at
		/// </summary>
		private long m_iStartTime;

		#endregion //Members

		#region Properties

		public long StartTime
		{
			get { return m_iStartTime; }
		}

		public float CurrentTime
		{
			get { return m_fCurrentTime; }
		}

		#endregion //Properties

		#region Methods

		public TimeUpdater()
		{
			m_iStartTime = 0;
			m_fCurrentTime = 0.0f;
		}

		public void Start()
		{
			m_iStartTime = DateTime.Now.Ticks;
			m_fCurrentTime = 0.0f;
		}

		public void Update()
		{
			//check that this thing has been started!
			Debug.Assert(0 != m_iStartTime);

			long iCurrentTime = DateTime.Now.Ticks;
			float TimeDelta = (iCurrentTime - m_iStartTime);
			m_fCurrentTime = TimeDelta / 10000000.0f;
		}

		#endregion //Methods
	}
}