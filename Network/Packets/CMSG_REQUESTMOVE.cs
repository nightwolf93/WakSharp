using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class CMSG_REQUESTMOVE : WakfuClientMessage
    {
        public int X { get; set; }
        public int Y { get; set; }
        public short IB { get; set; }

        public CMSG_REQUESTMOVE(byte[] data)
            : base(data)
        {
            this.X = base.Reader.ReadInt();
            this.Y = base.Reader.ReadInt();
            this.IB = base.Reader.ReadShort();
        }
    }
}
