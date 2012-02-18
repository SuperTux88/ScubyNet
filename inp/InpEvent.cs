using System;
using System.Collections.Generic;

namespace ScubyNet.inp
{
	public class InpEvent
	{
		private string msEventName;
		private string[] msCommands;
		
		public InpEvent(string vsEventName, List<string> vcsCommands)
		{
			msEventName = vsEventName;
			msCommands = vcsCommands.ToArray();
		}
		
		public string Name { get { return msEventName; } } 
		
		
		
		public void Trigger() {
			
		}
	}
}

