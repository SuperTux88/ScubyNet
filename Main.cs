using System;



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
			
			Connection c = new Connection("test.scubywars.de", 1337);
			
			
			
		}
	}
}
