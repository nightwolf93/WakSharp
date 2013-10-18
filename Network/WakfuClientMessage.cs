using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network
{
    public class WakfuClientMessage
    {
        public WakfuOPCode OPCode = WakfuOPCode.UNKNOWN;
        public IO.BigEndianReader Reader { get; set; }
        public ushort Size { get; set; }
        public byte Type { get; set; }

        public WakfuClientMessage(byte[] data)
        {
            this.Reader = new IO.BigEndianReader(data);
            var size = this.Reader.ReadUShort();
            var type = this.Reader.ReadByte();
            var opcode = (WakfuOPCode)this.Reader.ReadUShort();

            this.Size = size;
            this.Type = type;
            this.OPCode = opcode;
        }
    }
}
