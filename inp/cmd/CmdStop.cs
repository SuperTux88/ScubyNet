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
			lock (voConn) {	
				foreach (string param in vlsParams) {
					if (param.Equals("move")) {
						voConn.NextAction.Move = false;
					}
					if (param.Equals("left")) {
						voConn.NextAction.Left = false;
					}
					if (param.Equals("right")) {
						voConn.NextAction.Right = false;
					}
					if (param.Equals("fire")) {
						voConn.NextAction.Fire = false;
					}
						
				}
			}
		}
	}
}

