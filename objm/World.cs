using System;
using System.Collections.Generic;

using ScubyNet.net;
using ScubyNet.inp;

namespace ScubyNet.obj
{
	public class World
	{
		public class InpScriptEventRunner {
			private InpScriptEvent moEvent;
			private Connection moConn; 
			public InpScriptEventRunner(InpScriptEvent voSEvent, Connection voConn) {
				moEvent = voSEvent;
				moConn = voConn;
			}
			
			public bool ConsumeEvent() {
				Console.WriteLine("Triggering " + moEvent.Name);
				moEvent.Trigger(moConn);
				return true;
			}
		}
			
			
		private static World goWorld = null;
		public static World TheWorld { get { return goWorld; } }

        public static double RESET_BOUNDERY = 1000.0;
		
		public delegate void ShotEvent(Shot s);
		public delegate void PlayerEvent(Player p);
		
		public event ShotEvent ShotFired;
		public event PlayerEvent PlayerEntered;
		
		
		private Dictionary<long, Player> mcoPlayers = new Dictionary<long, Player>();
		private Dictionary<long, Shot> mcoShots = new Dictionary<long, Shot>();
		private List<InpScriptEventRunner> mcoRunners = new List<InpScriptEventRunner>();
		internal string msOwner;
		
		public World (string vsPlayername) {
			goWorld = this;
			msOwner = vsPlayername; 
			InpEvent.ConsumeWorld(this);
		}
		
		public void RegisterBot(InpScript voScript, Connection voConn) {
			foreach (InpScriptEvent oSE in voScript.Events.Values) {
				InpEvent e = InpEvent.GetEvent(oSE.Name);
				if (e != null) {
					InpScriptEventRunner oRunner = new InpScriptEventRunner(oSE, voConn);
					e.FireEvent += oRunner.ConsumeEvent;
					mcoRunners.Add(oRunner);
					Console.WriteLine("Event " + oSE.Name + " registered");
				} else {
					Console.WriteLine("Event " + oSE.Name + " not found");
				}
			}
		}
		
		public Player GetPlayer(long id) {
			Player oRet = null;
			if (mcoPlayers.ContainsKey(id)) {
				oRet = mcoPlayers[id];
			} else { 
				oRet = new Player(this, id);
				mcoPlayers.Add(id, oRet);
				PlayerEntered(oRet);
			}
			return oRet;
		}
				
		public Shot GetShot(long id) {
			Shot oRet = null;
			if (mcoShots.ContainsKey(id)) { 
				oRet = mcoShots[id];
			} else { 
				oRet = new Shot(this, id);
				mcoShots.Add(id, oRet);
				if (ShotFired != null)
					ShotFired(oRet);
			}
			return oRet;
		}
		
		public Entity GetEntity(long id) {
			if (mcoPlayers.ContainsKey(id)) return mcoPlayers[id];
			if (mcoShots.ContainsKey(id)) return mcoShots[id];
			return null;
		}
		
        public void removePlayer(long vlPublicID)
        {
            if (mcoPlayers.ContainsKey(vlPublicID)) {
				mcoPlayers[vlPublicID].FireLeave();
            	mcoPlayers.Remove(vlPublicID);
			}
        }

        public void setPlayerName(long vlPublicID, string vlPlayerName)
        {
            this.GetPlayer(vlPublicID).Name = vlPlayerName;
        }
		
		internal void WorldTrip() {
			List<long> ids = new List<long>();
			foreach (Shot oShot in mcoShots.Values) {
				if (oShot.cease()) {
					ids.Add(oShot.ID);
					oShot.Parent.Shot = null;
				}
			}
			foreach (long id in ids)
				mcoShots.Remove(id);
		}
		
		public Dictionary<long, Player> Players { get { return mcoPlayers; } } 
		public Dictionary<long, Player> Friends { 
			get { 
				Dictionary<long, Player> oRet = new Dictionary<long, Player>();
				lock (mcoPlayers) {
					foreach (Player p in mcoPlayers.Values)
						if (p.IsFriend)
							oRet.Add(p.ID, p);
				}
				return oRet;
			}
		}
		public Dictionary<long, Player> Enemies { 
			get { 
				Dictionary<long, Player> oRet = new Dictionary<long, Player>();
				lock (mcoPlayers) {
					foreach (Player p in mcoPlayers.Values)
						if (!p.IsFriend)
							oRet.Add(p.ID, p);
				}
				return oRet;
			}
		}
		
	}
}
