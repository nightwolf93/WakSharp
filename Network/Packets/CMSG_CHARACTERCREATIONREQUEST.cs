using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class CMSG_CHARACTERCREATIONREQUEST : WakfuClientMessage
    {
        public byte Sex { get; set; }
        public byte SkinColor { get; set; }
        public byte HairColor { get; set; }
        public byte PupilColor { get; set; }
        public byte SkinColorFactor { get; set; }
        public byte HairColorFactor { get; set; }
        public byte Cloth { get; set; }
        public byte Face { get; set; }
        public short Breed { get; set; }
        public string Name { get; set; }

        public CMSG_CHARACTERCREATIONREQUEST(byte[] data)
            : base(data)
        {
            base.Reader.ReadLong();//??
            this.Sex = base.Reader.ReadByte();
            this.SkinColor = base.Reader.ReadByte();
            this.HairColor = base.Reader.ReadByte();
            this.PupilColor = base.Reader.ReadByte();
            this.SkinColorFactor = base.Reader.ReadByte();
            this.HairColorFactor = base.Reader.ReadByte();
            this.Cloth = base.Reader.ReadByte();
            this.Face = base.Reader.ReadByte();
            this.Breed = base.Reader.ReadShort();
            this.Name = base.Reader.ReadString();
        }
    }
}
