using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScubyNet.inp
{
	public abstract class InpCommand
	{
		public static Dictionary<string, InpCommand> mcCommands = new Dictionary<string, InpCommand>();
		
		static InpCommand() {
			// todo: collect commands and store into dict 
			foreach (Type t in Assembly.GetExecutingAssembly().GetTypes()) {
				if (t.Namespace.Equals("ScubyNet.inp.cmd")) {
					InpCommand oCmd = Assembly.GetExecutingAssembly().CreateInstance(t.Name) as InpCommand;
					if (oCmd != null) {
						Console.WriteLine("found command" + oCmd.Name);
						mcCommands.Add(oCmd.Name, oCmd);
					}
				}	
			}
		}
			
		public InpCommand () {}
		
		public abstract string Name { get; }
		public abstract void Run();
		
	}
}

