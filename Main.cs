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
		public class DummyReader {
			private Connection moConn;
			public DummyReader(Connection voConn) { moConn = voConn; }
			public void Read() {
				while (Packet.Read(moConn) != null);
				Console.WriteLine("ouch: null packet");
			}
		}
		
		public static void Main (string[] args)
		{
			string sName = "Borg";
			string sURL = "10.1.1.19";
			//string sURL = "test.scubywars.de";
			int lPort = 1337;
			
			MainClass mc = new MainClass();
			mc.moWorld.PlayerEntered += mc.PlayerEntered;
			
			mc.c = new Connection(sName, sURL, lPort);
			mc.mcConnections.Add(mc.c.ID, mc.c);
			for (int i=0;i<	4; i++) {
				Connection oC = new Connection(sName, sURL, lPort);
				mc.mcConnections.Add(oC.ID, oC);
				new Thread(new ThreadStart(new DummyReader(oC).Read)).Start();
			}
			
			
			new Thread(new ThreadStart(mc.ProcessPackages)).Start();
			
		}
		
		private void PlayerEntered(Player p) { 
			Console.WriteLine("Player " + p.Name + "(" + p.ID + ") entered the game"); 
			p.PlayerLeft += PlayerLeft; 
		}
		private void PlayerLeft(Player p) { 
			Console.WriteLine("Player " + p.Name + "(" + p.ID + ") left the game"); 
		}	
		
		public void ProcessPackages() {
			Packet p;
			bool initdone=false;
			while ((p = Packet.Read(c)) != null) {
				// Packet = 3
				if (p is PackWorld) {
					moWorld.WorldTrip();
					
					// dummy code
					// entfernen, sobald dsl im ansatz funzt >> DAS IST HEUTE
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
				
				
				// finally process world events and feed the dsl – njomnjom
				if (!initdone) { // alles dummy: hier müssen erst mal listen gesammelt und events generiert werden
					ScubyNet.inp.InpCommand.RunCommand("move");
					initdone = true;
				}
			}
			Console.WriteLine("got null package. exiting");
		}
	}
}
