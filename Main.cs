using System;

using System.Threading;

namespace ScubyNet
{
	class MainClass
	{
		private Connection c 
		public static void Main (string[] args)
		{
			
			c = new Connection("Player", "gen", 1337);
					
			new Thread(new ThreadStart())
		}
		
		public void ProcessPackages() {
			Packet p;
			while ((p = Packet.Read(c)) != null) {
								
			}
		}
	}
}
