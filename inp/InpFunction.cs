using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScubyNet
{
	public abstract class InpFunction
	{
		private static Dictionary<string, InpFunction> gcoFunctions = new Dictionary<string, InpFunction>();
		
		static InpFunction() {
			Console.WriteLine("collecting functions");
			foreach (Type t in Assembly.GetExecutingAssembly().GetTypes()) {
				if (t.Namespace.Equals("ScubyNet.inp.fkt")) {
					Console.WriteLine(t.ToString());
					InpFunction oFunction = Assembly.GetExecutingAssembly().CreateInstance(t.FullName) as InpFunction;
					if (oFunction != null) {
						Console.WriteLine("found function " + oFunction.Name);
						gcoFunctions.Add(oFunction.Name, oFunction);
					}
				}	
			}
		}
		
		public static string RunFunction(string vsName, string[] vasParams) {
			if (!gcoFunctions.ContainsKey(vsName))
				return " {ERR} ";
			return gcoFunctions[vsName].Run(vasParams);
		}
		
		public abstract string Name { get ; }
		
		public abstract string Run(string[] vasParams);
		
		
	}
}

