using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class CMSG_VERSION : WakfuClientMessage
    {
        public byte Version { get; set; }
        public short Revision { get; set; }
        public byte Change { get; set; }
        public string Build { get; set; }

        public CMSG_VERSION(byte[] data)
            : base(data)
        {
            this.Version = base.Reader.ReadByte();
            this.Revision = base.Reader.ReadShort();
            this.Change = base.Reader.ReadByte();
            this.Build = base.Reader.ReadString();
        }

        public override string ToString()
        {
            return this.Version + "." + this.Revision + "." + this.Change;
        }
    }
}
