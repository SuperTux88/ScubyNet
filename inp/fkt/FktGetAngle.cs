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
			Vector2D a = ResolvePoint(vasParams[0]);
			Vector2D b = ResolvePoint(vasParams[1]);
			if (a == null) return "{ERR Could not get point from " + vasParams[0] + "}";
			if (b == null) return "{ERR Could not get point from " + vasParams[1] + "}";
			Entity e = ResolveEntity(vasParams[0]);
			if (e == null)
				return "{ERR Could not resole entity from " + vasParams[0] + "}";
			double an = a.VirtualAngleTo(b) - a.Angle;
			//if (an < 0) an += 2 * Math.PI;
			
			return an.ToString();
		}
	}
}

