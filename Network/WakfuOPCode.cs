﻿using System;
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
        SMSG_LISTWORLDS = 1200,
        CMSG_WORLDSELECT = 1201,
        SMSG_WORLDSELECTRESULT = 1202,
        CMSG_LOGINREQUEST = 1025,
        SMSG_LOGINRESULT = 1024,
        SMSG_RSAKEY = 1032,
        SMSG_COMMONCHATMESSAGE = 3152,
        CMSG_COMMONCHATMESSAGE = 3153,
        CMSG_TRADECHATMESSAGE = 3159,
        CMSG_CHANGEDIRECTION = 4117,
        CMSG_REQUESTMOVE = 4113,
        SMSG_SERVERTIME = 2063,
        SMSG_CHARACTERSLIST = 2048,
        SMSG_110 = 110,
    }
}
