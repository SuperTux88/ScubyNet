using System;
using System.Collections.Generic; 
using ScubyNet.inp;

namespace ScubyNet.inp.cmd
{
	public class CmdStart : InpCommand
	{
		public CmdStart ()
		{
		}
		
		public override string Name { get { return "start" ; } } 
		
		public override void Run (List<string> vlsParams, ScubyNet.net.Connection voConn)
		{
			lock (voConn) {
				
				foreach (string param in vlsParams) {
					if (param.Equals("move")) {
						voConn.NextAction.Thrust = true;
					}
					if (param.Equals("left")) {
						voConn.NextAction.Left = true;
						voConn.NextAction.Right = false;
					}
					if (param.Equals("right")) {
						voConn.NextAction.Left = false;
						voConn.NextAction.Right = true;
					}
					if (param.Equals("fire")) {
						voConn.NextAction.Fire = true;
					}
						
				}
			}
		}
	}
}

