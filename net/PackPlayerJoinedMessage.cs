using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScubyNet.net
{
    public class PackPlayerJoinedMessage : Packet
    {
        private long mlPublicId;
        private string msPlayerName;

        public PackPlayerJoinedMessage() { }

        protected override Packet createFromData(ref byte[] rbData)
        {
            mlPublicId = readLongFrom(ref rbData, 0);
            byte[] obData = new byte[rbData.Length - 8];
            // Playernamen auspacken
            readBytesFrom(ref rbData, 8, ref obData);
            msPlayerName = System.Text.Encoding.BigEndianUnicode.GetString(obData);
            return this;
        }

        internal override byte[] Build() { return null; }

        public long PublicId { get { return mlPublicId; } }
        public string PlayerName { get { return msPlayerName; } }
    }
}
