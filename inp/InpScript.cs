using System;
using System.IO;
using System.Collections.Generic;

namespace ScubyNet.inp
{
	public class InpScript
	{
		
		
		private InpScript ()
		{
		}
		
		public static InpScript TryParse(string vsFilename) {
			InpScript oRet = null;
			
			StreamReader oSR = File.OpenText(vsFilename);
			bool started = false;
			string sEvent = "";
			List<string> csBuf = new  List<string>();
			int num = 0;

			while (!oSR.EndOfStream) {
				string line = oSR.ReadLine().Trim();
				num++;
				if (line.Length > 0 && !line.StartsWith("//")) { 
					if (started) {
						if (line.StartsWith("!")) {
						} else {
							if (sEvent.Length == 0) {
								Console.WriteLine ("expected event at line " + num);
								return null;
							}
						}
					} else {
						if (line.Equals("itsnotpython")) 
							started = true;
					}
				}
			}
			return oRet;
		}
		
	}
}

