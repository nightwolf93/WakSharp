using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Enums
{
    public enum LoginResultEnum
    {
        CORRECT_LOGIN = 0,
        INVALID_LOGIN = 2,
        ALREADY_CONNECTED = 3,
        SAVE_IN_PROGRESS = 4,
        ACCOUNT_BANNED = 5,
        ACCOUNT_LOCKED = 9,
        LOGIN_SERVER_DOWN = 10,
        TOO_MANY_CONNECTIONS = 11,
        INVALID_PARTNER = 12,
        INVALID_EMAIL = 20,
        ACCOUNT_UNDER_MODERATION = 21,
        CLOSED_BETA = 127,
    }
}
