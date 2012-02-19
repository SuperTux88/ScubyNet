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
			string sName = "kannix";
			string sURL = "gen";
			//string sURL = "test.scubywars.de";
			int lPort = 1337;
			
			MainClass mc = new MainClass();
			
			mc.c = new Connection(sName, sURL, lPort);
			byte[] buf = new PackAction(true,false,true,true).Build();
			mc.c.SendBytes(ref buf);
			
			mc.mcConnections.Add(mc.c.ID, mc.c);
			for (int i=0;i<6; i++) {
				Connection oC = new Connection(sName, sURL, lPort);
				mc.mcConnections.Add(oC.ID, oC);		
			}
			
			
			new Thread(new ThreadStart(mc.ProcessPackages)).Start();
			
		}
		
		public void ProcessPackages() {
			Packet p;
			while ((p = Packet.Read(c)) != null) {
				// Packet = 3
				if (p is PackWorld) {
					moWorld.WorldTrip();
					foreach (Connection conn in mcConnections.Values) {
						int x = (int)(DateTime.Now.Ticks % 3);
						bool l = x == 1;
						bool r = x == 2;
						PackAction pa = new PackAction(l, r, true, true);
						byte[] buf = pa.Build();
						conn.SendBytes(ref buf);
					}		
				// Packet = 0
				} else if (p is PackPlayer) {
					PackPlayer oPP = p as PackPlayer;
					Player oPlayer = moWorld.GetPlayer(oPP.PlayerId);
					oPlayer.UpdateFromPacket(oPP);

                // Packet = 1
				} else if (p is PackShot) {
					PackShot oPS = p as PackShot;
					Shot oShot = moWorld.GetShot(oPS.PlayerId);
					oShot.UpdateFromPacket(oPS);
					oShot.Parent.moShot = oShot;

                // Packet = 6
                }else if (p is PackScoreboard) {
                    PackScoreboard oSB = p as PackScoreboard;
                    oSB.refreshPlayerScores(moWorld);

                // Packet = 7
                } else if (p is PackScoreboard) {
                    PackPlayerJoinedMessage oPJ = p as PackPlayerJoinedMessage;
                    moWorld.setPlayerName(oPJ.PublicId, oPJ.PlayerName);
                // Packet = 8
                } else if (p is PackPlayerLeftMessage) {
                    PackPlayerLeftMessage oPL = p as PackPlayerLeftMessage;
                    moWorld.removePlayer(oPL.PublicID);
                // Packet = 9
                } else if (p is PackPlayerName) {
                    PackPlayerName oPN = p as PackPlayerName;
                    moWorld.setPlayerName(oPN.PublicId, oPN.PlayerName);
                }
				
				
				
			}
			Console.WriteLine("got null package. exiting");
		}
	}
}
