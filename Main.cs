using System;

using System.Threading;
using ScubyNet.net;
using ScubyNet.obj;
using ScubyNet.inp;

using System.Collections.Generic;


namespace ScubyNet
{
	class MainClass
	{
		private Connection c;
		private Dictionary<long, Connection> mcConnections = new Dictionary<long, Connection>();
		private World moWorld;
		public class DummyReader {
			private Connection moConn;
			public DummyReader(Connection voConn) { moConn = voConn; }
			public void Read() {
				byte[] dummy = new byte[1024];
				while (moConn.RetreiveBytes(ref dummy) >= 0);
				Console.WriteLine("ouch: null packet");
			}
		}
		
		public static void Main (string[] args)
		{
			string sName = "nyan";
			string sURL = "10.1.1.19";
			//string sURL = "test.scubywars.de";
			int lPort = 1337;
			
			MainClass mc = new MainClass();
			mc.moWorld = new World(sName);
			
			mc.moWorld.PlayerEntered += mc.PlayerEntered;
			InpScript oScript = InpScript.TryParse("/home/uditaren/td/src/ScubyNet/testbot.inp");
			
			mc.c = new Connection(sName, sURL, lPort);
			mc.moWorld.RegisterBot(oScript, mc.c);
			
			mc.mcConnections.Add(mc.c.ID, mc.c);
			for (int i=0;i<	4; i++) {
				Connection oC = new Connection(sName, sURL, lPort);
				mc.moWorld.RegisterBot(oScript, oC);
				mc.mcConnections.Add(oC.ID, oC);
				new Thread(new ThreadStart(new DummyReader(oC).Read)).Start();
			}
					
			new Thread(new ThreadStart(mc.ProcessPackages)).Start();
			
		}
		
		private void PlayerEntered(Player p) { 
			Console.WriteLine("Player " + p.Name + "(" + p.ID + ") entered the game"); 
			p.PlayerLeft += PlayerLeft;
			p.ShotFired += PlayerShot;
			p.PlayerRenamed += PlayerRenamed;
		}
		
		private void PlayerLeft(Player p) { 
			Console.WriteLine("Player " + p.Name + "(" + p.ID + ") left the game"); 
		}	
		
		private void PlayerShot(Player p) {
			//if (!p.IsFriend)
			//	Console.WriteLine("Player " + p.Name + "(" + p.ID + ") fired a shot"); 
			//else
				p.Shot.ShotCeased += ShotCease;
		}
		
		private void ShotCease(Shot s) {
			//Console.WriteLine(s.Parent.Name + "'s shot ceased");
		}
		
		private void PlayerRenamed(Player p) {
			Console.WriteLine("Player " + p.ID + " renamed to " + p.Name); 
		}
		
		
		public void ProcessPackages() {
			Packet p;
			while ((p = Packet.Read(c)) != null) {
				// Packet = 3
				if (p is PackWorld) {
					moWorld.WorldTrip();
									
					foreach (Connection o in mcConnections.Values) {
						lock (o) {
							o.FireNextAction();
						}
					}
					
					// finally process world events and feed the dsl – njomnjom
					//if (!initdone) { // alles dummy: hier müssen erst mal listen gesammelt und events generiert werden
					//	ScubyNet.inp.InpCommand.RunCommand("move");
					//	initdone = true;
					//}
					
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
					oShot.Parent.Shot = oShot;

                // Packet = 6
                }else if (p is PackScoreboard) {
                    PackScoreboard oSB = p as PackScoreboard;
                    oSB.refreshPlayerScores(moWorld);

                // Packet = 7
                } else if (p is PackPlayerJoinedMessage) {
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
