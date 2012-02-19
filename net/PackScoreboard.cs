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
            int iScore = 0;
            long lID = 0;

            while (iPos + 12 < rbData.Length)
            {
                lID = readLongFrom(ref rbData, iPos);
                iScore = readIntFrom(ref rbData, iPos + 8);

                if (mcoScoreBoard.ContainsKey(lID))
                    mcoScoreBoard[lID] = iScore;
                else
                    mcoScoreBoard.Add(lID, iPos);

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
