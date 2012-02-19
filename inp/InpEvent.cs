using System;
using System.Reflection;
using System.Collections.Generic;
using ScubyNet.obj;

namespace ScubyNet.inp
{
	public abstract class InpEvent
	{
		private static Dictionary<string, InpEvent> gcoEvents = new Dictionary<string, InpEvent>();
		
		static InpEvent() {
			Console.WriteLine("collecting events");
			foreach (Type t in Assembly.GetExecutingAssembly().GetTypes()) {
				if (t.Namespace.Equals("ScubyNet.inp.events")) {
					Console.WriteLine(t.ToString());
					InpEvent oEvent = Assembly.GetExecutingAssembly().CreateInstance(t.FullName) as InpEvent;
					if (oEvent != null) {
						Console.WriteLine("found event " + oEvent.Name);
						gcoEvents.Add(oEvent.Name, oEvent);
					}
				}	
			}
		}
		
		public static void ConsumeWorld(World w) {
			foreach (InpEvent oEvent in gcoEvents.Values) {
				
			}
		}
		
		public abstract string Name { get; }
		public abstract void Consume(World w);
		
		public InpEvent ()
		{
		}
	}
}

