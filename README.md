GameTimer
=========

A bunch of timer objects that can be used for time effects like hiererchical time, slowdown, speedup, hitpause, etc.

The most important thing is that these timers can be used hierarchically.  You can pass one gameclock into the update function of another one, which updates based on that dude's time.  So the timer chain might look something like this:

```
						MainTime
							|
						GameTime
				/			|				\
	Character1Time	ParticleEngineTime	Character2Time
```
So like the MainTime is a GameTimer.GameClock and is updated first from the XNA GameTime object.  This one always runs at realtime, so you don't want to pause or slow it down.  Can problably run HUD or menu transitions off this clock.

The GameTime is also a GameTimer.GameClock and would be used to run the gameplay itself.  This one can be paused for the Pause menu, or slowed down/sped up if the game play speed needs to be changed.  Since all the clocks below this one are updated off it, they will also pause/slowdown/speedup.

Under that, you could have the ParticleEngine running off it's own clock separate form the player characters.  That way when the players are punching the snot out of each other, you can add some hit pause to add crunch and the particles will still be flying around.

Idunno, that's just example how I use it in my fighting games.  I also send these timers over the network to sync up the server & clients.

Cheers!