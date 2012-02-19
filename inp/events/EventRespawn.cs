using System;
using ScubyNet.inp;
using ScubyNet.obj;

namespace ScubyNet.inp.events
{
	public class EventRespawn : InpEvent
	{
		
		public override event HandleEvent FireEvent;
		
		public EventRespawn ()
		{
		}
		
		public override string Name { get { return "RESPAWN"; } }
		
		public override void Consume (World w)
		{
			
		}
	}
}

