using System;


using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ScubyNet
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//Application.Init ();
			//MainWindow win = new MainWindow ();
			//win.Show ();
			//Application.Run ();
			Console.WriteLine("test");
			
			Socket 	s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			s.Connect("test.scubywars.de", 1337);
			byte[] buf = new byte[1024];
			int ret = s.Receive(buf);
			string sret = System.Text.Encoding.ASCII.GetString(buf, 0, ret);
			Console.WriteLine(sret);
			
			
			
			
		}
	}
}
