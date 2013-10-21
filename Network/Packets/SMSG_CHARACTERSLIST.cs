using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class SMSG_CHARACTERSLIST : WakfuServerMessage
    {
        public SMSG_CHARACTERSLIST()
            : base(WakfuOPCode.SMSG_CHARACTERSLIST)
        {
            //TODO
            base.Writer.WriteByte(0);//Characters count
        }
    }
}
