using System;
using System.Collections.Generic;
using ScubyNet.inp;
using ScubyNet.net;

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
		
		public override void Run (List<string> vlsParams, Connection voConn)
		{
			lock (voConn) {
				voConn.NextAction.Thrust = true;
			}
		}
	}
}

