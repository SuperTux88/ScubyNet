using System;
using ScubyNet.obj;

namespace ScubyNet.inp.events
{
	public class EventShot : InpEvent
	{
		public EventShot ()	{}		
		
		public override string Name  { get { return "SHOT"; } }
		
		public override void Consume(World w) {
			w.PlayerEntered += this.PlayerEntered;
		}
		
		private void PlayerEntered(Player voPlayer) {
			voPlayer.PlayerRenamed += this.PlayerRenamed;
			voPlayer.ShotFired += this.ShotFired;
		}
		
		private void PlayerRenamed(Player voPlayer) {
			// sollte eine freund/feind unterscheidung notwendig werden, kann das hier abgefragt werden
		}
		
		private void ShotFired(Player voPlayer) {
			//System.Media.SystemSounds.Beep.Play();
			// trigger – hier kann dass script getriggert werden.
		}
	}
}

