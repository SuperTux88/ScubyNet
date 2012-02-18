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
			int lPos = vsEventName.IndexOf("=");
			if (lPos > 0 ) {
				msEventName = vsEventName.Substring(0, lPos).Trim();
			} else {
				msEventName = vsEventName;
			}
			msCommands = vcsCommands.ToArray();
		}
		
		public string Name { get { return msEventName; } } 
		
		public void Trigger() {
			
		}
	}
}

