using System;
using System.Collections.Generic;

namespace ScubyNet.inp
{
	public class InpScriptEvent
	{
		private InpScript moParent;
		private string msEventName;
		private string[] msCommands;
		
		public InpScriptEvent(InpScript voParent, string vsEventName, List<string> vcsCommands)
		{
			moParent = voParent;
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
			foreach (string sLine in msCommands) { 
				string[] sParts = sLine.Split(' ');
				if (InpCommand.HasCommand(sParts[0])) {
					InpCommand.RunCommand(sLine);
				}
			}
		}
	}
}

