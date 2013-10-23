using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class CMSG_DELETECHARACTERREQUEST : WakfuClientMessage
    {
        public long CharacterID { get; set; }
        public string Lang { get; set; }

        public CMSG_DELETECHARACTERREQUEST(byte[] data)
            : base(data)
        {
            this.CharacterID = base.Reader.ReadLong();
            base.Reader.ReadByte();//??
            this.Lang = base.Reader.ReadString();
        }
    }
}
