#if NETWORKING
using Microsoft.Xna.Framework.Net;
#endif

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
		public float StartTime { get; private set; }

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
		/// <param name="fSeconds">how long to run this timer</param>
		public void Start(float fSeconds)
		{
			base.Start();
			CountdownLength = fSeconds;
			StartTime = CurrentTime;
		}

		/// <summary>
		/// get the amount of time remaining on this egg timer
		/// </summary>
		/// <returns>the number of seconds left</returns>
		public float RemainingTime()
		{
			return (CountdownLength - (CurrentTime - StartTime));
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

		#endregion //Methods

		#region Networking

#if NETWORKING

		/// <summary>
		/// Read this object from a network packet reader.
		/// </summary>
		public override void ReadFromNetwork(PacketReader packetReader)
		{
			base.ReadFromNetwork(packetReader);
			CountdownLength = packetReader.ReadSingle();
			StartTime = packetReader.ReadSingle();
		}

		/// <summary>
		/// Write this object to a network packet reader.
		/// </summary>
		public override void WriteToNetwork(PacketWriter packetWriter)
		{
			base.WriteToNetwork(packetWriter);

			packetWriter.Write(CountdownLength);
			packetWriter.Write(StartTime);
		}

#endif

		#endregion //Networking
	}
}