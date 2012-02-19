using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScubyNet.net
{
    public class PackScoreboard : Packet
    {
        private Dictionary<long, int> mcoScoreBoard = new Dictionary<long, int>();
        public PackScoreboard() { }

        protected override Packet createFromData(ref byte[] rbData)
        {
            int iPos = 0;
            while (iPos + 12 < rbData.Length)
            {
                mcoScoreBoard.Add(readLongFrom(ref rbData, iPos), readIntFrom(ref rbData, iPos + 8));
                iPos += 12;
            }
            return this;
        }

        public void refreshPlayerScores(ScubyNet.obj.World vWorld)
        {
            foreach (long id in mcoScoreBoard.Keys)
            {
                vWorld.GetPlayer(id).Score = mcoScoreBoard[id];
            }
        }
        internal override byte[] Build() { return null; }
    }
}
