using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class CMSG_SECRETANSWERSUBMIT : WakfuClientMessage
    {
        public long CharacterID { get; set; }
        public string Answer { get; set; }

        public CMSG_SECRETANSWERSUBMIT(byte[] data)
            : base(data)
        {
            this.CharacterID = base.Reader.ReadLong();
            base.Reader.ReadByte();
            this.Answer = base.Reader.ReadString();
        }
    }
}
