using System;


using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ScubyNet.net
{
	public class Connection
	{
		Socket moSocket = null;
		private string msName = "";
		private long mlID = -1;
		
		public Connection (string vsName, string vsHost, int vlPort)
		{
			moSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			moSocket.NoDelay = true;
			//moSocket.Blocking = false;
			moSocket.Connect(vsHost, vlPort);
			
			PackHandshake hs = new PackHandshake(vsName);
			if (hs.DoHandshake(this)) {
				Console.WriteLine("Connection established");
				msName = vsName;
				mlID = hs.PlayerId;
				// Connected
			} else { 
				Console.WriteLine("hs fail");
				throw new Exception("rgs");
			}
		}
		 
		public string Name { get { return msName; } }
		public long ID { get { return mlID; } }
		
		internal void SendBytes(ref byte[] vbData) {
			moSocket.Send(vbData);
		}
		internal int RetreiveBytes(ref byte[] rbData) {
			return moSocket.Receive(rbData);
		}
		internal int RetreiveBytes(ref byte[] rbData, int vlPos) {
			return moSocket.Receive(rbData, vlPos, rbData.Length - vlPos, SocketFlags.None);
		}
		
		~Connection() {
			if (moSocket != null) 
				moSocket.Close();
		}
	}
}

