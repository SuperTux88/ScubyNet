using System;
using ScubyNet.inp;

namespace ScubyNet.inp.events
{
	public class EventWorld : InpEvent
	{
		public override event HandleEvent FireEvent;
		
		public EventWorld ()
		{
		}
		
		public override string Name {
			get {
				 return "WORLD";
			}
		}
		
		public override void Consume (ScubyNet.obj.World w)
		{
			w.WorldRefreshed += this.WorldRefreshed; 
		}
		
		private void WorldRefreshed() {
			if (FireEvent!=null)
				FireEvent();
		}
	}
}

