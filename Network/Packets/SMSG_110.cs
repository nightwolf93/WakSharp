using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class SMSG_110 : WakfuServerMessage
    {
        public SMSG_110()
            : base(WakfuOPCode.SMSG_110)
        {
            base.Writer.WriteByte(82);
            base.Writer.WriteByte(229);
            base.Writer.WriteByte(170);
            base.Writer.WriteByte(131);
        }
    }
}
