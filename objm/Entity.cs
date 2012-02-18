using System;

namespace ScubyNet.obj
{
	public abstract class Entity
	{
		internal World moParent;
		private long mlID = -1;
		private double mfX = 0.0;
		private double mfY = 0.0;
		private double mfDir = 0.0;
		private int mlRadius = 0;
		
		protected Entity (World voParent, long vlID)
		{
			moParent = voParent;
			mlID = vlID;
		}
		
		public long ID { get { return mlID; } }
		public double PosX { get { return mfX; } set { mfX = value; } }
		public double PosY { get { return mfY; } set { mfY = value; } }
		public double Direction { get { return mfDir; } set { mfDir = value; } }
		public int Radius { get { return mlRadius; } set { mlRadius	= value; } }
		public int Speed { get; }
		
		public double DistanceTo(Entity voEnt) {
            return Math.Sqrt(
                       Math.Pow((voEnt.PosX - this.PosX), 2) 
                     + Math.Pow((voEnt.PosY - this.PosY), 2)
                   );
		}
	}
}

