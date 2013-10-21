using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class SMSG_SERVERTIME : WakfuServerMessage
    {
        public SMSG_SERVERTIME(long time)
            : base(WakfuOPCode.SMSG_SERVERTIME)
        {
            base.Writer.WriteLong(time);
        }
    }
}
