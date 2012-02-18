using System;

namespace ScubyNet.net
{
	public class PackUnknown : Packet
	{
				
		public PackUnknown ()
		{
		}
		
		internal override byte[] Build ()
		{
			return null;
		}
		
		protected override Packet createFromData (ref byte[] rbData)
		{
			return new PackUnknown();
		}
	}
}

