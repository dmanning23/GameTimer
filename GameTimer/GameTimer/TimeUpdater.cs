using System;
using System.Diagnostics;

namespace GameTimer
{
	/// <summary>
	/// this class is for updating time in winform games
	/// </summary>
	public class TimeUpdater
	{
		#region Properties

		/// <summary>
		/// the time this thing was started at
		/// </summary>
		public long StartTime { get; private set; }

		/// <summary>
		/// the current time of this thing
		/// </summary>
		public float CurrentTime { get; private set; }

		#endregion //Properties

		#region Methods

		public TimeUpdater()
		{
			StartTime = 0;
			CurrentTime = 0f;
		}

		public void Start()
		{
			StartTime = DateTime.Now.Ticks;
			CurrentTime = 0f;
		}

		public void Update()
		{
			var currentTicks = DateTime.Now.Ticks;
			var timeDelta = currentTicks - StartTime;
			CurrentTime = timeDelta / 10000000.0f;
		}

		#endregion //Methods
	}
}