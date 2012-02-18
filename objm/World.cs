using System;
using System.Collections.Generic;

namespace ScubyNet.obj
{
	public class World
	{
		public delegate void NewShotHandler(long id);
		
		public event NewShotHandler NewShot;
		
		private Dictionary<long, Player> mcoPlayers = new Dictionary<long, Player>();
		private Dictionary<long, Shot> mcoShots = new Dictionary<long, Shot>();
		
		public World () { }
		
		public Player GetPlayer(long id) {
			Player oRet = null;
			if (mcoPlayers.ContainsKey(id)) {
				oRet = mcoPlayers[id];
			} else { 
				oRet = new Player(this, id);
				mcoPlayers.Add(id, oRet);
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
				NewShot(id);
			}
			return oRet;
		}

        public void removePlayer(long vlPublicID)
        {
            if (mcoPlayers.ContainsKey(vlPublicID))                 
              mcoPlayers.Remove(vlPublicID);
        }

        public void setPlayerName(long vlPublicID, string vlPlayerName)
        {
            this.GetPlayer(vlPublicID).Name = vlPlayerName;
        }
		// Collisions
		
		// 
		
		// all players als List<Player>
		
		// all shots als List<Shot>
		
		// all * by Condition
		
		internal void WorldTrip() {
			List<long> ids = new List<long>();
			foreach (Shot oShot in mcoShots.Values) {
				if (oShot.cease()) {
					ids.Add(oShot.ID);
					oShot.Parent.moShot = null;
				}
			}
			foreach (long id in ids)
				mcoShots.Remove(id);
		}
		
	}
}
