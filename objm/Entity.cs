using System;

namespace ScubyNet.obj
{
	public abstract class Entity
	{
		public static double vShootspeed = 0.1;
		
		internal World moParent;
		private long mlID = -1;
        private Point mPos = new Point(0.0, 0.0);
		private double mfDir = 0.0;
		private int mlRadius = 0;
		private int mlSpeed = 0;
		
		protected Entity (World voParent, long vlID)
		{
			moParent = voParent;
			mlID = vlID;
		}

        public Point getPositionNextFrame ()
        {
            Point retPoint = new Point(0.0,0.0);
            

            return retPoint;
        }
		
		
		public Point GitHitpoint(Entity voTarget, bool vbShoot) {
			double dist = this.Position.getShortestDistanceTo(voTarget.Position);
			double time = dist / vbShoot?vShootspeed:this.Speed;
			
			Point p = new Point(voTarget.Direction);
			p.PosX *= time * voTarget.Speed;
			p.PosY *= time * voTarget.Speed;
			Point target = voTarget.Position.Add(p);
			
			return target;
		}
		
		
		public long ID { get { return mlID; } }
        
		public int Speed { get { return mlSpeed; } set { mlSpeed = value; } }
		public double Direction { get { return mfDir; } set { mfDir = value; } }
		public int Radius { get { return mlRadius; } set { mlRadius	= value; } }
	    public Point Position { get { return mPos; } }


	}
}

