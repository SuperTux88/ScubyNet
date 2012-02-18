using System;

namespace ScubyNet.obj
{
	public class Shot : Entity
	{
		private int mlSpeed = 0;
		private long mlParentId = -1;
		private double mfLifetime = 0.0;
		private bool mbCease = false;
		
		public Shot (World voParent, long id) : base(voParent, id){}
		
		internal void UpdateFromPacket(ScubyNet.net.PackShot voPacket) {
			mbCease = false;
			this.Position.PosX = voPacket.PosX;
			this.Position.PosY = voPacket.PosY;
			this.Direction  = voPacket.Direction;
			this.Radius = voPacket.Radius;
			this.Speed = voPacket.Speed;
			//this.ParentId = voPacket.ParentId; << sinnfrei, bleibt immer gleich
			this.Lifetime = voPacket.Lifetime;
		}
		
		internal bool cease() {
			if (!mbCease)
				mbCease = true;
			else
				return true;
			return false;
		}
		
		public override int Speed { get { return mlSpeed; } set { mlSpeed = value; } }
		public long ParentId { get { return mlParentId; } set { mlParentId = value; } }
		public Player Parent { get { return moParent.GetPlayer(ParentId); } }
		public double Lifetime { get { return mfLifetime; } set { mfLifetime = value; } }
	}
}

