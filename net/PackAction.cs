using System;

namespace ScubyNet.net
{
	public class PackAction : Packet
	{
		private bool mbLeft;
		private bool mbRight;
		private bool mbThrust;
		private bool mbFire;

        public PackAction()
        {
        }

		public PackAction (bool vbLeft, bool vbRight, bool vbThrust, bool vbFire)
		{
			mbLeft = vbLeft;
			mbRight = vbRight;
			mbThrust = vbThrust;
			mbFire = vbFire;
		}
		
		protected override Packet createFromData (ref byte[] rbData)
		{
			return null;
		}
		
		internal override byte[] Build ()
		{
			byte[] abRet = new byte[10];
			writeShortAt(ref abRet, 0, (int)PacketType.Action);
			writeIntAt(ref abRet, 2, 4);
			writeByteAt(ref abRet, 6, (byte)(mbLeft?1:0));
			writeByteAt(ref abRet, 7, (byte)(mbRight?1:0));
			writeByteAt(ref abRet, 8, (byte)(mbThrust?1:0));
			writeByteAt(ref abRet, 9, (byte)(mbFire?1:0));
			return abRet;
		}
	}
}

