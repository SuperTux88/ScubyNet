using System;


using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ScubyNet.net
{
	public class Connection
	{
		Socket moSocket = null;
		
		public Connection (string vsName, string vsHost, int vlPort)
		{
			moSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			
			moSocket.Connect(vsHost, vlPort);
			
			PackHandshake hs = new PackHandshake(vsName);
			if (hs.DoHandshake(this)) {
				Console.WriteLine("Connection established");

				// Connected

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

