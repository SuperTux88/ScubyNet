using System;



namespace ScubyNet.net
{
	public class PackHandshake : Packet
	{
		private string msPlayerName;
		private long mlPlayerId;
				
		public PackHandshake (string vsPlayerName)
		{
			if (vsPlayerName == null)
				throw new ArgumentException("no Name!");
			msPlayerName = vsPlayerName;
		}
		public PackHandshake() {}
		protected override Packet createFromData (ref byte[] rbData)
		{
			int ack = readByteFrom(ref rbData, 0);
			mlPlayerId = readLongFrom(ref rbData, 1);
			return this;
		}
		
		internal PackHandshake(string vsPlayerName, int vlPlayerId) {
			msPlayerName = vsPlayerName;
			mlPlayerId = vlPlayerId;
		}
		
		public string PlayerName { get { return msPlayerName; } }
		public long PlayerId { get { return mlPlayerId; } }
		
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
			if (p == null || !p.GetType().Equals(typeof(PackHandshake)))
				return false;
			this.mlPlayerId = ((PackHandshake)p).PlayerId;
			return true;
		}
		
	}
}

