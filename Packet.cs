using System;
using System.Collections.Generic;

namespace ScubyNet
{
	public abstract class Packet
	{
		protected byte[] mabData = null;
			
		private static Dictionary<int, Type> types = new Dictionary<int, Type>();  
		
		static Packet() {
			types.Add((int)PacketType.Handshake, typeof(PackHandshake));
		}
		
		public enum PacketType { 
			Player = 0,
			Shot, 
			PowerUp, 
			World, 
			Handshake, 
			Action, 
			Scoreboard, 
			PlayerJoined, 
			PlayerLeft
		}
		
		protected abstract byte[] Build();
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
			lRet += rbData[vlPos] << 16;
			lRet += rbData[vlPos] << 8;
			lRet += rbData[vlPos + 1];
			return lRet;
		}
		
		public static Packet Read(Connection c) {
			byte[] abHead = new byte[6];
			if (c.RetreiveBytes(ref abHead) != 6)
				throw new Exception("ouch: malformed head");
			
			int id = readShortFrom(ref abHead, 0);
			int len = readIntFrom(ref abHead, 2);
			
			byte[] abData = new byte[len];
			c.RetreiveBytes(ref abData);
			
			
			
			return null;
		}
		
	}
}

