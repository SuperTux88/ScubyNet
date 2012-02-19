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
		
		
		private string Eval(string sLine, bool vbFkt) { 
			int count = 0;
			string ssub = "";
			string sret = " ";
			foreach (char c in sLine.Trim()) {
				if (c == '[') 
					count++;
				else if (c == ']') {
					count--;
					if (count == 0)
						sret += Eval(ssub, true);
				}
				else {
					if (count == 0)
						ssub += c; 
					else 
						sret += c;
				}
			}
			if (count != 0) {
				Console.WriteLine ( "cannot parse expression" );
				return " {ERR} ";
			}
			
			string[] asParts = sret.Trim().Split(':');
			string sfkt = asParts[0].Trim();
			string[] asParams = null;
			if (asParts.Length > 0) {
				asParams = asParts[1].Replace(',',' ').Trim().Split(' ');
			}
			sret += InpFunction.RunFunction(sfkt, asParams);
			
			return sret + " ";
		}
		
		
		public void Trigger() {
			foreach (string sLine in msCommands) { 
				string sFullLine = Eval(sLine, false).Trim();
				if (sFullLine.Contains("{ERR")) {
					Console.WriteLine("error in line: " + sLine);
					Console.WriteLine(" >> " + sFullLine);
					continue;
				}
				
				
				
			}
		}
	}
}

