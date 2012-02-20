using System;

namespace ScubyNet.obj
{
	public abstract class Entity
	{
		public static double vShootspeed = 0.1;
		
		internal World moParent;
		private long mlID = -1;
        private Vector2D mPos = new Vector2D();
		private double mfDir = 0.0;
		private int mlRadius = 0;
		private int mlSpeed = 0;
		
		protected Entity (World voParent, long vlID)
		{
			moParent = voParent;
			mlID = vlID;
		}
		
		public Vector2D GetHitpoint(Entity voTarget, bool vbShoot) {
			double dist = this.Position.VirtualDistanceTo(voTarget.Position);
			double time = dist / (vbShoot?vShootspeed:this.Speed);
			
			Vector2D p = new Vector2D(voTarget.mlSpeed);
			p.Angle = voTarget.Direction;
			p.Mult(time);
			Vector2D target = voTarget.Position.Add(p);
			
			return target;
		}
		
		public long ID { get { return mlID; } }
        
		public int Speed { get { return mlSpeed; } set { mlSpeed = value; } }
		public double Direction { get { return mfDir; } set { mfDir = value; } }
		public int Radius { get { return mlRadius; } set { mlRadius	= value; } }
	    public Vector2D Position { get { return mPos; } }


	}
}

