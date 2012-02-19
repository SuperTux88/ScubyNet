using System;
using System.Collections.Generic;
using System.Reflection;
using ScubyNet.obj;

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
		
		protected Point ResolvePoint(string vsParam) {
			string p = vsParam.Trim();
			if (p.StartsWith("{") && p.EndsWith("}")) {
				long id;
				if (!long.TryParse(p.Substring(1, p.Length - 2).Trim(), out id)) 
					return null;
                Entity e = World.TheWorld.GetEntity(id);
                if (e == null)
                    return null;
				return e.Position;
			} else if (p.StartsWith("(") && p.EndsWith(")") && p.IndexOf('|') > 0) {
				int pos = p.IndexOf('|');
				string px = p.Substring(1, pos-1).Trim();
				string py = p.Substring(pos +1, p.Length - pos - 1).Trim();
				Point ret = new Point();
				double dx;
				double dy;
				if (!double.TryParse(px, out dx)) return null;
				if (!double.TryParse(py, out dy)) return null;
				ret.PosX = dx;
				ret.PosY = dy;
				return ret;
			} else 
				return null;
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

