using System;

namespace ScubyNet.obj
{
	public class Player : Entity
	{
		private string msName = "unknown";
		private double mfRotSpd = 0.0;
		private bool mbLeft = false;
		private bool mbRight = false;
		private bool mbThrust = false;
		private bool mbFire = false;
		internal Shot moShot = null;
		
		public Player (World voParent, long id) : base(voParent, id)
		{
			
		}
		
		public void UpdateFromPacket(ScubyNet.net.PackPlayer voPack) {
			this.PosX = voPack.PosX;
			this.PosY = voPack.PosY;
			this.Direction = voPack.Direction;
			this.Radius = voPack.Radius;
			this.RotationSpeed = voPack.RotationSpeed;
			this.Left = voPack.Left;
			this.Right = voPack.Right;
			this.Thrust = voPack.Thrust;
			this.Fire = voPack.Fire;
		}
		
		public string Name { get { return msName; } set { msName = value; } }
		public double RotationSpeed { get { return mfRotSpd; } set { mfRotSpd = value; } }
		public bool Left { get { return mbLeft; } set { mbLeft = value; } }
		public bool Right { get { return mbRight; } set { mbRight = value; } }
		public bool Thrust { get { return mbThrust; } set { mbThrust = value; } }
		public bool Fire { get { return mbFire; } set { mbFire = value; } }
		public Shot Shot { get { return moShot; } }
		
	}
}