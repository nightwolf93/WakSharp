using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class SMSG_DELETECHARACTERRESULT : WakfuServerMessage
    {
        public SMSG_DELETECHARACTERRESULT(byte result)
            : base(WakfuOPCode.SMSG_DELETECHARACTERRESULT)
        {
            base.Writer.WriteByte(result);
        }
    }
}
