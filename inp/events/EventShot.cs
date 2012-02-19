using System;
using ScubyNet.obj;

namespace ScubyNet.inp.events
{
	public class EventShot : InpEvent
	{
		public EventShot ()	{}		
		
		public override string Name  { get { return "SHOT"; } }
		
		public override void Consume(World w) {
			
		}
	}
}

