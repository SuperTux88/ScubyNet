using System;

namespace ScubyNet
{
	public class PackShot : Packet
	{
		private long mlPublicID;
		private double mfX;
		private double mfY;
		private double mfDir;
		private int mlRadius;
		private int mlSpeed;
		private long mlParentId;
		private int mlLifetime;
		
		public PackShot () {}
		
		public long PlayerId { get { return mlPublicID; } }
		public double PosX { get { return mfX; } }
		public double PosY { get { return mfY; } }
		public double Direction { get { return mfDir; } }
		public int Radius { get { return mlRadius; } }
		public int Speed { get { return mlSpeed; } }
		public long ParentId { get { return mlParentId; } }
		public int Lifetime { get { return mlLifetime; } }
		
		protected override Packet createFromData (ref byte[] rbData)
		{
			mlPublicID = readLongFrom(ref rbData, 0);
			mfX = readIntFrom(ref rbData, 8);
			mfY = readIntFrom(ref rbData, 12);
			mfDir = readIntFrom(ref rbData, 16);
			mlRadius = readShortFrom(ref rbData, 20);
			mlSpeed = readShortFrom(ref rbData, 22);
			mlParentId = readLongFrom(ref rbData, 24);
			mlLifetime = readIntFrom(ref rbData, 32);
			return this;
		}
		
		protected override byte[] Build ()
		{
			 return null;
		}
	}
}

