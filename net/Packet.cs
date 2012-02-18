using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScubyNet.net
{
	public abstract class Packet
	{
		protected byte[] mabData = null;
			
		private static Dictionary<int, Type> types = new Dictionary<int, Type>();  
		
		static Packet() {
			types.Add((int)PacketType.Player, typeof(PackPlayer));
			types.Add((int)PacketType.Shot, typeof(PackShot));
			types.Add((int)PacketType.World, typeof(PackWorld));
			types.Add((int)PacketType.Handshake, typeof(PackHandshake));
			types.Add((int)PacketType.Action, typeof(PackAction));
            types.Add((int)PacketType.PlayerLeft, typeof(PackPlayerLeftMessage));
			//types.Add((int)PacketType.Handshake, typeof(PackHandshake));
			//types.Add((int)PacketType.Handshake, typeof(PackHandshake));
		}
		
		public enum PacketType { 
			Player = 0,
			Shot, 
			PowerUp, 
			World, 
			Handshake, 
			Action = 5, 
			Scoreboard, 
			PlayerJoined, 
			PlayerLeft,
            PlayerName
		}
		
		internal abstract byte[] Build();

		protected abstract Packet createFromData(ref byte[] rbData);
		
		protected static void writeByteAt(ref byte[] rbData, int vlPos, byte vbValue) {
			rbData[vlPos] = vbValue;
		}
		
		protected static void writeShortAt(ref byte[] rbData, int vlPos, int vlValue) {
			rbData[vlPos    ] = (byte)((vlValue >> 8) & 0xFF);
			rbData[vlPos + 1] = (byte)( vlValue       & 0xFF);
		}
		
		protected static void writeIntAt(ref byte[] rbData, int vlPos, int vlValue) {
			rbData[vlPos    ] = (byte)((vlValue >> 24) & 0xFF);
			rbData[vlPos + 1] = (byte)((vlValue >> 16) & 0xFF);
			rbData[vlPos + 2] = (byte)((vlValue >>  8) & 0xFF);
			rbData[vlPos + 3] = (byte)( vlValue        & 0xFF);
		}
		
		protected static void writeLongAt(ref byte[] rbData, int vlPos, long vlValue) {
			rbData[vlPos    ] = (byte)((vlValue >> 56) & 0xFF);
			rbData[vlPos + 1] = (byte)((vlValue >> 48) & 0xFF);
			rbData[vlPos + 2] = (byte)((vlValue >> 40) & 0xFF);
			rbData[vlPos + 3] = (byte)((vlValue >> 32) & 0xFF);
			rbData[vlPos + 4] = (byte)((vlValue >> 24) & 0xFF);
			rbData[vlPos + 5] = (byte)((vlValue >> 16) & 0xFF);
			rbData[vlPos + 6] = (byte)((vlValue >>  8) & 0xFF);
			rbData[vlPos + 7] = (byte)( vlValue        & 0xFF);
		}
		
		protected static void writeBytesAt(ref byte[] rbData, int vlPos, byte[] vbValue) {
			for (int i=0 ; i<vbValue.Length; i++) 
				rbData[vlPos + i] = vbValue[i];
		}
		
		protected static byte readByteFrom(ref byte[] rbData, int vlPos) {
			return rbData[vlPos];
		}
		
		protected static int readShortFrom(ref byte[] rbData, int vlPos) {
			int lRet = 0;
			lRet += rbData[vlPos] << 8;
			lRet += rbData[vlPos + 1];
			return lRet;
		}
		
		protected static int readIntFrom(ref byte[] rbData, int vlPos) {
			int lRet = 0;
			lRet += rbData[vlPos] << 24;
			lRet += rbData[vlPos + 1] << 16;
			lRet += rbData[vlPos + 2] << 8;
			lRet += rbData[vlPos + 3];
			return lRet;
		}
		
		protected static long readLongFrom(ref byte[] rbData, int vlPos) {
			int lRet = 0;
			lRet += rbData[vlPos] << 56;
			lRet += rbData[vlPos + 1] << 48;
			lRet += rbData[vlPos + 2] << 40;
			lRet += rbData[vlPos + 3] << 32;
			lRet += rbData[vlPos + 4] << 24;
			lRet += rbData[vlPos + 5] << 16;
			lRet += rbData[vlPos + 6] << 8;
			lRet += rbData[vlPos + 7];
			return lRet;
		}
		
		protected static float readFloatFrom(ref byte[] rbData, int vlPos) {
			byte[] conv = new byte[4];
			conv[0] = rbData[vlPos + 3];
			conv[1] = rbData[vlPos + 2];
			conv[2] = rbData[vlPos + 1];
			conv[3] = rbData[vlPos];
			float fRet = BitConverter.ToSingle(conv, 0);
			return fRet;
		}
		
		public static Packet Read(Connection c) {
			byte[] abHead = new byte[6];
			int len = 0;
			while (len < 6)
				len += c.RetreiveBytes(ref abHead, len);
   			//if (c.RetreiveBytes(ref abHead) != 6)
			//	throw new Exception("ouch: malformed head");
			
			int id = readShortFrom(ref abHead, 0);
			len = readIntFrom(ref abHead, 2);
			
			byte[] abData = new byte[len];
			len = c.RetreiveBytes(ref abData);
			
			Packet oRet = null;
			if (types.ContainsKey(id)) {
				oRet = (Packet)Assembly.GetExecutingAssembly().CreateInstance(types[id].FullName);
				oRet.createFromData(ref abData);
			} else {
				if (id != 0 && id != 3)
				Console.WriteLine("Packetid " + id + " not implemented");
				oRet = new PackUnknown();
				
			}
			return oRet;
		}
		
	}
}

