using System;
using ScubyNet.inp;
using ScubyNet.obj;

namespace ScubyNet.inp.fkt
{
	public class FktDist : InpFunction
	{	
		public FktDist (){}
		
		public override string Name { get { return "dist"; } }
		
		public override string Run (string[] vasParams)
		{
			if (vasParams.Length != 2)
				return "{ERR Need two parameters}";
			Vector2D a = ResolvePoint(vasParams[0]);
			Vector2D b = ResolvePoint(vasParams[1]);
			if (a == null) return "{ERR Could not get point from " + vasParams[0] + "}";
			if (b == null) return "{ERR Could not get point from " + vasParams[1] + "}";
			return a.VirtualDistanceTo(b).ToString();
		}
	}
}

