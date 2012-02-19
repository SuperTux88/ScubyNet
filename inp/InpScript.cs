using System;
using System.IO;
using System.Collections.Generic;

namespace ScubyNet.inp
{
	public class InpScript
	{
		
		private Dictionary<string, InpScriptEvent> mcoEvents = new Dictionary<string, InpScriptEvent>();
		
		private InpScript ()
		{
		}
		
		public Dictionary<string, InpScriptEvent> Events { get { return mcoEvents; } }
		
		public static InpScript TryParse(string vsFilename) {
			InpScript oRet = new InpScript();
			
			StreamReader oSR = File.OpenText(vsFilename);
			bool started = false;
			string sEvent = "";
			List<string> csBuf = new  List<string>();
			int num = 0;
			
			Console.WriteLine("Reading Script " + vsFilename);
			while (!oSR.EndOfStream) {
				string line = oSR.ReadLine().Trim();
				num++;
				if (line.Length > 0 && !line.StartsWith("//")) { 
					if (started) {
						if (line.StartsWith("!")) {
							if (sEvent.Length != 0) {
								InpScriptEvent oEvent = new InpScriptEvent(oRet, sEvent, csBuf);
								oRet.mcoEvents.Add(sEvent, oEvent);
								csBuf.Clear();
							}
							sEvent = line.Substring(1);
						} else {
							if (sEvent.Length == 0) {
								Console.WriteLine ("expected event at line " + num + ", ignoring line");
							} else { 
								csBuf.Add(line);
							}
						}
					} else {
						if (line.Equals("itsnotpython")) 
							started = true;
					}
				}
			}
			if (sEvent.Length > 0) {
				InpScriptEvent oEvent = new InpScriptEvent(oRet, sEvent, csBuf);
				oRet.mcoEvents.Add(sEvent, oEvent);
			}
			
			Console.WriteLine("Checking...");
			foreach (InpScriptEvent oEvent in oRet.mcoEvents.Values) {
				Console.WriteLine ("got event: " + oEvent.Name);
			}
			
			return oRet;
		}
		
	}
}

