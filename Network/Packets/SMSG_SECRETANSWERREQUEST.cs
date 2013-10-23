using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class SMSG_SECRETANSWERREQUEST : WakfuServerMessage
    {
        public SMSG_SECRETANSWERREQUEST(long characterID, string question)
            : base(WakfuOPCode.SMSG_SECRETANSWERREQUEST)
        {
            base.Writer.WriteLong(characterID);
            base.Writer.WriteByte(0);
            base.Writer.WriteString(question);
        }
    }
}
