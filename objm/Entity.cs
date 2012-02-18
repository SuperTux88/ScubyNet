using System;

namespace ScubyNet.obj
{
	public abstract class Entity
	{
		internal World moParent;
		private long mlID = -1;
        private Point mPos = new Point(0.0, 0.0);
		private double mfDir = 0.0;
		private int mlRadius = 0;
		
		protected Entity (World voParent, long vlID)
		{
			moParent = voParent;
			mlID = vlID;
		}
		
		public long ID { get { return mlID; } }
        
		public double Direction { get { return mfDir; } set { mfDir = value; } }
		public int Radius { get { return mlRadius; } set { mlRadius	= value; } }
<<<<<<< HEAD
		public abstract int Speed { get; set; }
		
		public double DistanceTo(Entity voEnt) {
            return Math.Sqrt(
                       Math.Pow((voEnt.PosX - this.PosX), 2) 
                     + Math.Pow((voEnt.PosY - this.PosY), 2)
                   );
		}
=======
        public virtual int Speed { get { return 0; } set { } }
        public Point Position { get { return mPos; } }

>>>>>>> e4ec72eeb63d300e3c2e4d668a614ec16407e5d5
	}
}

