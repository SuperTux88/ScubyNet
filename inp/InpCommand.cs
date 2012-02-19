using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScubyNet.inp
{
	public abstract class InpCommand
	{
		public static Dictionary<string, InpCommand> mcCommands = new Dictionary<string, InpCommand>();
		
		static InpCommand() {
			Console.WriteLine("collecting commands");
			foreach (Type t in Assembly.GetExecutingAssembly().GetTypes()) {
				if (t.Namespace.Equals("ScubyNet.inp.cmd")) {
					Console.WriteLine(t.ToString());
					InpCommand oCmd = Assembly.GetExecutingAssembly().CreateInstance(t.FullName) as InpCommand;
					if (oCmd != null) {
						Console.WriteLine("found command " + oCmd.Name);
						mcCommands.Add(oCmd.Name, oCmd);
					}
				}	
			}
		}
			
		public InpCommand () {}
		
		
		public static bool HasCommand(string vsName) {
			return mcCommands.ContainsKey(vsName);
		}
		
		public static void RunCommand(string vsName) {
			string[] asParts = vsName.Split(' ');
			string sCommand = asParts[0].Trim();
			List<string> lsParams = new List<string>();
			for (int i=1; i<asParts.Length; i++)
				if (asParts[i].Trim().Length > 0)
					lsParams.Add(asParts[i].Trim());
			if (!mcCommands.ContainsKey(sCommand))
				Console.WriteLine("command " + sCommand + " not found");
			else
				mcCommands[vsName].Run(lsParams);
		}
		
		public abstract string Name { get; }
		public abstract void Run(List<string> vlsParams);
		
	}
}

