using System;

using System.Threading;
using ScubyNet.net;

namespace ScubyNet
{
	class MainClass
	{
		private Connection c;
		
		public static void Main (string[] args)
		{
			MainClass mc = new MainClass();
			mc.c = new Connection("Player", "gen", 1337);
					
			new Thread(new ThreadStart(mc.ProcessPackages)).Start();
		}
		
		public void ProcessPackages() {
			Packet p;
			while ((p = Packet.Read(c)) != null) {
				
				//Console.WriteLine(p.ToString());
			}
			Console.WriteLine("got null package. exiting");
		}
	}
}
