using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class SMSG_LOGINRESULT : WakfuServerMessage
    {
        public SMSG_LOGINRESULT(Enums.LoginResultEnum result, int charID, bool isAdmin, string charName)
            : base(WakfuOPCode.SMSG_LOGINRESULT)
        {
            base.Writer.WriteByte((byte)result);
            base.Writer.WriteShort(50);
            base.Writer.WriteByte(1);
            base.Writer.WriteByte(0);
            base.Writer.WriteInt(6);
            base.Writer.WriteByte(0);
            base.Writer.WriteLong(charID);
            base.Writer.WriteByte(0);
            base.Writer.WriteLong(1000);//Subscribe
            base.Writer.WriteInt(isAdmin ? 1 : 0);
            base.Writer.WriteString(charName);
            base.Writer.WriteString("??");
            base.Writer.WriteShort(00);
        }
    }
}
