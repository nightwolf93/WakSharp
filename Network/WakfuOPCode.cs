using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network
{
    //SMG, CMSG
    public enum WakfuOPCode
    {
        UNKNOWN = -1,
        CMSG_VERSION = 7,
        SMSG_RSAKEY = 1032,
    }
}
