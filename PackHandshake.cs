using System;



namespace ScubyNet
{
	public class PackHandshake : Packet
	{
		private string msPlayerName;
		private int mlPlayerId;
				
		public PackHandshake (string vsPlayerName)
		{
			if (vsPlayerName == null)
				throw new ArgumentException("no Name!");
			msPlayerName = vsPlayerName;
		}
		internal PackHandshake() {}
		protected override Packet createFromData (ref byte[] rbData)
		{
			//throw new NotImplementedException ();
			PackHandshake oRet = new PackHandshake();
			return oRet;
		}
		
		internal PackHandshake(string vsPlayerName, int vlPlayerId) {
			msPlayerName = vsPlayerName;
			mlPlayerId = vlPlayerId;
		}
		
		public string PlayerName { get { return msPlayerName; } }
		public int PlayerId { get { return mlPlayerId; } }
		
		protected override byte[] Build ()
		{
			byte[] abName = System.Text.Encoding.Convert(System.Text.Encoding.Default, System.Text.Encoding.BigEndianUnicode, System.Text.Encoding.Default.GetBytes(PlayerName));
			byte[] abRet = new byte[8 + abName.Length];
			writeShortAt(ref abRet, 0, (int)PacketType.Handshake);
			writeIntAt(ref abRet, 2, abName.Length);
			writeShortAt(ref abRet, 6, 0);
			writeBytesAt(ref abRet, 8, abName);
			return abRet;
		}
		
		public bool DoHandshake(Connection c) {
			c.SendBytes(this.Build());
			Packet p = Packet.Read(c);
			return false;
		}
		
	}
}

