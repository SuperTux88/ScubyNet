using System;

namespace ScubyNet
{
    class PackWorld : Packet
    {
        public PackWorld() { }

        private int miEntityCount;
        
        protected override Packet createFromData(ref byte[] rbData)
        {
            //throw new NotImplementedException ();
            PackWorld oRet = new PackWorld();
            miEntityCount = readIntFrom(ref rbData, 0);
            return oRet;
        }

        public int EntityCount { get { return miEntityCount; } }

        protected override byte[] Build() { return null; }
    }
}
