using System;
using ScubyNet.inp;
using ScubyNet.obj;

namespace ScubyNet.inp.fkt
{
	public class FktGetAngle : InpFunction 
	{
		public FktGetAngle ()
		{
		}
		
		public override string Name {
			get {
				 return "angle";
			}
		}
		
		public override string Run (string[] vasParams)
		{
			if (vasParams.Length != 2)
				return "{ERR Need two parameters}";
			Point a = ResolvePoint(vasParams[0]);
			Point b = ResolvePoint(vasParams[1]);
			if (a == null) return "{ERR Could not get point from " + vasParams[0] + "}";
			if (b == null) return "{ERR Could not get point from " + vasParams[1] + "}";
			Entity e = ResolveEntity(vasParams[0]);
			if (e == null)
				return "{ERR Could not resole entity from " + vasParams[0] + "}";
			double an = a.getAngleToShortest(b);
			if (an < 0) an += 2 * Math.PI;
			
			return (an - e.Direction).ToString();
		}
	}
}

