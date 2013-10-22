using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class SMSG_CHARACTERSLIST : WakfuServerMessage
    {
        public SMSG_CHARACTERSLIST(List<Database.Models.Character> characters)
            : base(WakfuOPCode.SMSG_CHARACTERSLIST)
        {
            base.Writer.WriteByte((byte)characters.Count);
            foreach (var character in characters)
            {
                character.EncodeToCharactersList(base.Writer);
            }
        }
    }
}
