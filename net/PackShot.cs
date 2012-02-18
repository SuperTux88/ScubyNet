using System;

namespace ScubyNet.net
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
		private float mlLifetime;
		
		public PackShot () {}
		
		public long PlayerId { get { return mlPublicID; } }
		public double PosX { get { return mfX; } }
		public double PosY { get { return mfY; } }
		public double Direction { get { return mfDir; } }
		public int Radius { get { return mlRadius; } }
		public int Speed { get { return mlSpeed; } }
		public long ParentId { get { return mlParentId; } }
		public float Lifetime { get { return mlLifetime; } }
		
		protected override Packet createFromData (ref byte[] rbData)
		{
			mlPublicID = readLongFrom(ref rbData, 0);
			mfX = readFloatFrom(ref rbData, 8);
			mfY = readFloatFrom(ref rbData, 12);
			mfDir = readFloatFrom(ref rbData, 16);
			mlRadius = readShortFrom(ref rbData, 20);
			mlSpeed = readShortFrom(ref rbData, 22);
			mlParentId = readLongFrom(ref rbData, 24);
			mlLifetime = readFloatFrom(ref rbData, 32);
			return this;
		}
		
		internal override byte[] Build ()
		{
			 return null;
		}
		
		public override string ToString ()
		{
			return string.Format ("[PackShot: PlayerId={0}, PosX={1}, PosY={2}, Direction={3}, Radius={4}, Speed={5}, ParentId={6}, Lifetime={7}]", PlayerId, PosX, PosY, Direction, Radius, Speed, ParentId, Lifetime);
		}
	}
}

