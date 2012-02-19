using System;
using ScubyNet.obj;

namespace ScubyNet.inp.events
{
	public class EventEnter : InpEvent
	{
		public override event HandleEvent FireEvent;
		
		public EventEnter ()
		{
		}
		
		public override string Name {
			get {
				return "ENTER";
			}
		}
		
		public override void Consume(World w)
		{
			w.PlayerEntered += this.PlayerEntered;
		}
		
		private void PlayerEntered(Player p) {
			if (FireEvent != null)
				FireEvent();
		}
	}
}

