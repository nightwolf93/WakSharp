using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class SMSG_LOGINRESULT : WakfuServerMessage
    {
        public SMSG_LOGINRESULT(Enums.LoginResultEnum result, Database.Models.Account account, int charID, bool isAdmin, string charName)
            : base(WakfuOPCode.SMSG_LOGINRESULT)
        {
            base.Writer.WriteByte((byte)result);
            if (result == Enums.LoginResultEnum.CORRECT_LOGIN)
            {
                //base.Writer.WriteShort(64);// Size of the block below
                base.Writer.MarkShort(0);
                {
                    base.Writer.WriteByte(1);
                    base.Writer.WriteByte(0);
                    base.Writer.WriteInt(6);
                    base.Writer.WriteLong(account.ID);
                    base.Writer.WriteByte(123);
                    base.Writer.WriteLong(0L);//Subscribe
                    base.Writer.WriteInt(isAdmin ? 1 : 0);
                    for (var i = 33; i < 50; i++)
                    {
                        base.Writer.WriteByte(0);
                    }
                    base.Writer.WriteString(charName);
                    base.Writer.WriteString("??");
                    base.Writer.WriteShort(0);
                }
                base.Writer.EndMarkShort(0, -2);
            }
        }
    }
}
