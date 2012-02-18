using System;
using ScubyNet.inp;

namespace ScubyNet.inp.cmd
{
	public class CmdMove : InpCommand
	{
		public CmdMove ()
		{
		}
		
		public override string Name {
			get {
				return "move";
			}
		}
		
		public override void Run ()
		{
			throw new NotImplementedException ();
		}
	}
}

