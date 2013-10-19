using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class CMSG_COMMONCHATMESSAGE : WakfuClientMessage
    {
        public string Message { get; set; }

        public CMSG_COMMONCHATMESSAGE(byte[] data)
            : base(data)
        {
            this.Message = base.Reader.ReadString();
        }
    }
}
