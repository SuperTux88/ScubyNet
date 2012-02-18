using System;

using System.Threading;
using ScubyNet.net;
using ScubyNet.obj;

using System.Collections.Generic;


namespace ScubyNet
{
	class MainClass
	{
		private Connection c;
		private Dictionary<long, Connection> mcConnections = new Dictionary<long, Connection>();
		private World moWorld = new World();
		
		
		public static void Main (string[] args)
		{
			string sName = "Player";
			string sURL = "10.1.1.128";
			//string sURL = "test.scubywars.de";
			int lPort = 1337;
			
			MainClass mc = new MainClass();
			
			mc.c = new Connection(sName, sURL, lPort);
			mc.mcConnections.Add(mc.c.ID, mc.c);
			for (int i=0;i<2; i++) {
				Connection oC = new Connection(sName, sURL, lPort);
				mc.mcConnections.Add(oC.ID, oC);	
			}
					
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
