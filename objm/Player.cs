using System;

namespace ScubyNet.obj
{
	public class Player : Entity
	{
		public delegate void PlayerEvent(Player p);
		public event PlayerEvent PlayerLeft;
		public event PlayerEvent ShotFired;
		public event PlayerEvent PlayerRenamed;
		public event PlayerEvent DirectionChanged;
		public event PlayerEvent ThrustChanged;
		
		
		
		private string msName = "unknown";
        private int mScore = 0;
		private double mfRotSpd = 0.0;
		private bool mbLeft = false;
		private bool mbRight = false;
		private bool mbThrust = false;
		private bool mbFire = false;
		private Shot moShot = null;
	
		
		public Player (World voParent, long id) : base(voParent, id)
		{
		}
				
		public void UpdateFromPacket(ScubyNet.net.PackPlayer voPack) {
			this.Position.X      = voPack.PosX;
			this.Position.Y      = voPack.PosY;
			this.Direction = voPack.Direction;

            this.Score = voPack.Score;

			this.Radius = voPack.Radius;
			this.Speed = voPack.Speed;
			this.RotationSpeed = voPack.RotationSpeed;
			this.Left = voPack.Left;
			this.Right = voPack.Right;
			this.Thrust = voPack.Thrust;
			this.Fire = voPack.Fire;
		}
	
		public void FireLeave() {
			if (PlayerLeft != null)
				PlayerLeft(this);
		}
		
		//public override int Speed { get { return 100; } set { } }
		public string Name { 
			get { return msName; } 
			set { 
				if (value != null && !value.Equals(msName)) {
					msName = value;
					if (PlayerRenamed != null) 
						PlayerRenamed(this);
				}
			} 
		}
        public int Score { get { return mScore; } set { mScore = value; } }
		public double RotationSpeed { get { return mfRotSpd; } set { mfRotSpd = value; } }
		public bool Left { 
			get { return mbLeft; } 
			internal set {
				if (value != mbLeft) {
					if (DirectionChanged != null)
						DirectionChanged(this);
					mbLeft = value;
				}
			} 
		}
		public bool Right { 
			get { return mbRight; } 
			internal set { 
				if (value != mbRight) {
					if (DirectionChanged != null)
						DirectionChanged(this);
					mbRight = value; 
				}
			} 
		}
		public bool Thrust { 
			get { return mbThrust; } 
			internal set {
				if (value != mbThrust) {
					if (ThrustChanged != null)
						ThrustChanged(this);
					mbThrust = value;
				}
			} 
		}
		public bool Fire { get { return mbFire; } set { mbFire = value; } }
		public Shot Shot { 
			get { return moShot; } 
			internal set {
				if (value != null)
					if (moShot == null || value.ID != moShot.ID) {
						moShot = value;
						if (ShotFired != null)
							ShotFired(this);
					}
			} 
		}
		public bool CanShoot { get { return Shot == null; } }
		public bool IsFriend { get { return msName.StartsWith(moParent.msOwner); } }
		
	}
}
