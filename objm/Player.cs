using System;

namespace ScubyNet.obj
{
	public class Player : Entity
	{
		private string msName = "unknown";
        private int mScore = 0;
		private double mfRotSpd = 0.0;
		private bool mbLeft = false;
		private bool mbRight = false;
		private bool mbThrust = false;
		private bool mbFire = false;
		internal Shot moShot = null;
		
		public Player (World voParent, long id) : base(voParent, id)
		{
			voParent.NewShot += HandleNewShot;
		}
		
		private void HandleNewShot(long id) {
			//Console.WriteLine("new shot(" + id + "for player " + this.ID);
			//boom
		}
		
		public void UpdateFromPacket(ScubyNet.net.PackPlayer voPack) {
			this.Position.PosX      = voPack.PosX;
			this.Position.PosY      = voPack.PosY;
			this.Position.Direction = voPack.Direction;

            this.Score = voPack.Score;

			this.Radius = voPack.Radius;
			this.RotationSpeed = voPack.RotationSpeed;
			this.Left = voPack.Left;
			this.Right = voPack.Right;
			this.Thrust = voPack.Thrust;
			this.Fire = voPack.Fire;
		}
		
		public override int Speed { get { return 100; } set { } }
		public string Name { get { return msName; } set { msName = value; } }
        public int Score { get { return mScore; } set { mScore = value; } }
		public double RotationSpeed { get { return mfRotSpd; } set { mfRotSpd = value; } }
		public bool Left { get { return mbLeft; } set { mbLeft = value; } }
		public bool Right { get { return mbRight; } set { mbRight = value; } }
		public bool Thrust { get { return mbThrust; } set { mbThrust = value; } }
		public bool Fire { get { return mbFire; } set { mbFire = value; } }
		public Shot Shot { get { return moShot; } }
		public bool CanShoot { get { return Shot == null; } }
		
	}
}
