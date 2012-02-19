using System;
using System.Collections.Generic;
using ScubyNet.obj;
using ScubyNet.net;

namespace ScubyNet.inp
{
	public class InpScriptEvent
	{
		private InpScript moParent;
		private string msEventName;
		private string msEventCondition = "";
		private string[] msCommands;
		
		public InpScriptEvent(InpScript voParent, string vsEventName, List<string> vcsCommands)
		{
			moParent = voParent;
			int lPos = vsEventName.IndexOf("=");
			if (lPos > 0 ) {
				msEventName = vsEventName.Substring(0, lPos).Trim();
				msEventCondition = vsEventName.Substring(lPos+1).Trim();
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
						sret += c; 
					else 
						ssub += c;
				}
			}
			if (count != 0) {
				Console.WriteLine ( "cannot parse expression" );
				return " {ERR} ";
			}
			
			if (vbFkt) {
				string[] asParts = sret.Trim().Split(':');
				string sfkt = asParts[0].Trim();
				string[] asParams = null;
				if (asParts.Length > 0) {
					asParams = asParts[1].Replace(',',' ').Trim().Split(' ');
				}
				sret += InpFunction.RunFunction(sfkt, asParams);
			}
			
			return sret + " ";
		}
		
		
		public void Trigger(Connection voConn) {
			foreach (string sLine in msCommands) { 
				Console.WriteLine("processing: " + sLine);
				string sFullLine = sLine.Replace("_", "{" + voConn.ID + "}" ); 
				if (msEventCondition.Length == 0) 
					sFullLine = sFullLine.Replace("*", "{" + voConn.ID + "}" );
				sFullLine = Eval(sFullLine, false).Trim();
				if (sFullLine.Contains("{ERR")) {
					Console.WriteLine("error in line: " + sLine);
					Console.WriteLine(" >> " + sFullLine);
					continue;
				}
				
				Console.WriteLine("process line: " + sFullLine);
				if (sFullLine.StartsWith("?")) {
					
				} else if (InpCommand.HasCommand(sFullLine.Split(' ')[0].Trim())) {
					InpCommand.RunCommand(sFullLine, voConn);
				} else { 
					
				}
				
				
			}
		}
	}
}

