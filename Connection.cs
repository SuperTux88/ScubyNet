using System;


using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ScubyNet
{
	public class Connection
	{
		Socket moSocket = null;
		
		public Connection (string vsHost, int vlPort)
		{
			moSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			
			moSocket.Connect("gen", 1337);
			
			PackHandshake hs = new PackHandshake("dasbinich üöäpqß");
			if (hs.DoHandshake(this)) {
				Console.WriteLine("ha");
				//alles gut
			} else { 
				Console.WriteLine("hs fail");
				throw new Exception("rgs");
			}
		}
		
		internal void SendBytes(byte[] vbData) {
			moSocket.Send(vbData);
		}
		internal int RetreiveBytes(ref byte[] rbData) {
			return moSocket.Receive(rbData);
		}
		
		~Connection() {
			if (moSocket != null) 
				moSocket.Close();
		}
	}
}

