using System;

namespace ScubyNet.net
{
    class PackWorld : Packet
    {
        public PackWorld() { }

        private int miEntityCount;
        
        protected override Packet createFromData(ref byte[] rbData)
        {
            miEntityCount = readIntFrom(ref rbData, 0);
            return this;
        }

        public int EntityCount { get { return miEntityCount; } }

        internal override byte[] Build() { return null; }
		
		public override string ToString ()
		{
			return string.Format ("[PackWorld: EntityCount={0}]", EntityCount);
		}
    }
}
