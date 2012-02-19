using System;

namespace ScubyNet.net
{
	public class PackPlayer : Packet
	{
		private long mlPublicID;
        private int miScore;
		private double mfX;
		private double mfY;
		private double mfDir;
		private int mlRadius;
		private int mlSpeed;
		private double mfRot;
		private bool mbLeft;
		private bool mbRight;
		private bool mbThrust;
		private bool mbFire;
		
		public PackPlayer () {}
		
		public long PlayerId { get { return mlPublicID; } }
        public int Score { get { return miScore; } set { miScore = value; } }
		public double PosX { get { return mfX; } }
		public double PosY { get { return mfY; } }
		public double Direction { get { return mfDir; } }
		public int Radius { get { return mlRadius; } }
		public int Speed { get { return mlSpeed; } }
		public double RotationSpeed { get { return mfRot; } }
		public bool Left { get { return mbLeft; } }
		public bool Right { get { return mbRight; } }
		public bool Thrust { get { return mbThrust; } }
		public bool Fire { get { return mbFire; } }
		
		protected override Packet createFromData (ref byte[] rbData)
		{
			mlPublicID = readLongFrom(ref rbData, 0);
			mfX = readFloatFrom(ref rbData, 8);
			mfY = readFloatFrom(ref rbData, 12);
			mfDir = readFloatFrom(ref rbData, 16);
			mlRadius = readShortFrom(ref rbData, 20);
			mlSpeed = readShortFrom(ref rbData, 22);
			mfRot = readFloatFrom(ref rbData, 24);
			mbLeft = readByteFrom(ref rbData, 28) != 0;
			mbRight = readByteFrom(ref rbData, 29) != 0;
			mbThrust = readByteFrom(ref rbData, 30) != 0;
			mbFire = readByteFrom(ref rbData, 31) != 0;
			return this;
		}
		
		internal override byte[] Build ()
		{
			 return null;
		}
		
		public override string ToString ()
		{
			return string.Format ("[PackPlayer: PlayerId={0}, PosX={1}, PosY={2}, Direction={3}, Radius={4}, RotationSpeed={5}, Left={6}, Right={7}, Thrust={8}, Fire={9}]", PlayerId, PosX, PosY, Direction, Radius, RotationSpeed, Left, Right, Thrust, Fire);
		}
	}
}

