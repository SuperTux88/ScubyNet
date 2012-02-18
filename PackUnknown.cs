using System;

namespace ScubyNet
{
	public class PackUnknown : Packet
	{
		internal int id;
		
		public PackUnknown ()
		{
		}
		
		protected override byte[] Build ()
		{
			return null;
		}
		
		protected override Packet createFromData (ref byte[] rbData)
		{
			return new PackUnknown();
		}
	}
}

