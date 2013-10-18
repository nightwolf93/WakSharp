using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class SMSG_RSAKEY : WakfuServerMessage
    {
        public const ulong RSA_VERIFICATION_LONG = 0x8000000000000000L;

        public SMSG_RSAKEY(byte[] salt)
            : base(WakfuOPCode.SMSG_RSAKEY)
        {
            base.Writer.WriteULong(RSA_VERIFICATION_LONG);
            base.Writer.WriteBytes(salt);
        }
    }
}
