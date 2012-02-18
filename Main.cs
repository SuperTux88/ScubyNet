using System;

using System.Threading;
using ScubyNet.net;
using ScubyNet.obj;

namespace ScubyNet
{
	class MainClass
	{
		private Connection c;
		private World moWorld = new World();
		
		public static void Main (string[] args)
		{
			MainClass mc = new MainClass();
			//mc.c = new Connection("Player", "gen", 1337);
			mc.c = new Connection("Player", "test.scubywars.de", 1337);
					
			new Thread(new ThreadStart(mc.ProcessPackages)).Start();
		}
		
		public void ProcessPackages() {
			Packet p;
			while ((p = Packet.Read(c)) != null) {
				
				if (p is PackWorld) {
					moWorld.WorldTrip();
				} else if (p is PackPlayer) {
					PackPlayer oPP = p as PackPlayer;
					Player oPlayer = moWorld.GetPlayer(oPP.PlayerId);
					oPlayer.UpdateFromPacket(oPP);
				} else if (p is PackShot) {
					PackShot oPS = p as PackShot;
					Shot oShot = moWorld.GetShot(oPS.PlayerId);
					oShot.UpdateFromPacket(oPS);
					oShot.Parent.moShot = oShot;
				}
				
			}
			Console.WriteLine("got null package. exiting");
		}
	}
}
