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
        public virtual int Speed { get { return 0; } set { } }
        public Point Position { get { return mPos; } }

	}
}

