using System;

namespace ScubyNet.net
{
    public class PackPlayerLeftMessage : Packet
    {
        private long mlPublicID;

        public PackPlayerLeftMessage() { }

        protected override Packet createFromData(ref byte[] rbData)
        {
            mlPublicID = readLongFrom(ref rbData, 0);
            return this;
        }

        internal override byte[] Build() { return null; }

        public long PublicID { get { return mlPublicID; } }

    }
}
