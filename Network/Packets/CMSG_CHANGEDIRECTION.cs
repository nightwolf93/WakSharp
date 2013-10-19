using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class CMSG_CHANGEDIRECTION : WakfuClientMessage
    {
        public byte Direction { get; set; }

        public CMSG_CHANGEDIRECTION(byte[] data)
            : base(data)
        {
            this.Direction = base.Reader.ReadByte();
        }
    }
}
