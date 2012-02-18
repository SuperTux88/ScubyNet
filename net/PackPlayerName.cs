using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScubyNet.net
{
    public class PackPlayerName : Packet
    {
        private long mlPublicId;
        private string msPlayerName;

        public PackPlayerName() { }

        protected override Packet createFromData(ref byte[] rbData)
        {
            mlPublicId = readLongFrom(ref rbData, 0);

            return this;
        }

        internal override byte[] Build() { return null; }

    }
}
