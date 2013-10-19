using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class SMSG_LISTWORLDS : WakfuServerMessage
    {
        public SMSG_LISTWORLDS(List<WakfuWorld> worlds)
            : base(WakfuOPCode.SMSG_LISTWORLDS)
        {
            base.Writer.WriteByte((byte)(worlds.Count));
            foreach (var world in worlds)
            {
                world.Encode(this);
            }
        }
    }
}
