using System;
using System.Collections.Generic;
using ScubyNet.inp;
using ScubyNet.net;

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
		
		public override void Run (List<string> vlsParams, Connection voConn) 
		{
			
		}
	}
}

