using System;
using ScubyNet.inp;

namespace ScubyNet.inp.fkt
{
	public class FktHit : InpFunction   
	{
		public FktHit () {}
		
		public override string Name {
			get {
				 return "hit";
			}
		}
		
		public override string Run (string[] vasParams)
		{
			return " (1.21|22.1) ";
		}
		
	}
}

