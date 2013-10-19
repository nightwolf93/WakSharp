using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class CMSG_WORLDSELECT : WakfuClientMessage
    {
        public int WorldID { get; set; }

        public CMSG_WORLDSELECT(byte[] data)
            : base(data)
        {
            this.WorldID = base.Reader.ReadInt();
        }
    }
}
