using System;
using ScubyNet.inp;

namespace ScubyNet.inp.cmd
{
	public class CmdStop : InpCommand
	{
		public CmdStop ()
		{
			
		}
		
		public override string Name {
			get {
				return "stop";
			}
		}
		
		public override void Run ()
		{
			throw new NotImplementedException ();
		}
	}
}

