using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class SMSG_WORLDSELECTRESULT : WakfuServerMessage
    {
        public SMSG_WORLDSELECTRESULT(int worldID, bool success)
            : base(WakfuOPCode.SMSG_WORLDSELECTRESULT)
        {
            base.Writer.WriteInt(worldID);
            base.Writer.WriteByte((byte)(success ? 0 : 1));
        }
    }
}
