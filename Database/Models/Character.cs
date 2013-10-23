using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Database.Models
{
    public class Character : Interfaces.IIdentificable
    {
        public int ID { get; set; }
        public int Account { get; set; }
        public string Nickname { get; set; }
        public int Level { get; set; }
        public long Experience { get; set; }
        public int Sex { get; set; }
        public int Breed { get; set; }
        public int SkinColor { get; set; }
        public int HairColor { get; set; }
        public int PupilColor { get; set; }
        public int SkinColorFactor { get; set; }
        public int HairColorFactor { get; set; }
        public int Cloth { get; set; }
        public int Face { get; set; }
        public int Title { get; set; }

        /// <summary>
        /// Write data of the character into the stream for display him
        /// </summary>
        /// <param name="bigEndianWriter">Stream</param>
        public void EncodeToCharactersList(IO.BigEndianWriter buffer)
        {
            buffer.MarkShort(this.ID);
            {
                buffer.WriteByte(4);//Block type

                buffer.WriteLong(this.ID);
                buffer.WriteByte(0);
                buffer.WriteLong(this.Account);

                buffer.WriteBigString(this.Nickname);
                buffer.WriteShort((short)this.Breed);

                buffer.WriteByte((byte)this.Sex);
                buffer.WriteByte((byte)this.SkinColor);
                buffer.WriteByte((byte)this.HairColor);
                buffer.WriteByte((byte)this.PupilColor);
                buffer.WriteByte((byte)this.SkinColorFactor);
                buffer.WriteByte((byte)this.HairColorFactor);
                buffer.WriteByte((byte)this.Cloth);
                buffer.WriteByte((byte)this.Face);
                buffer.WriteShort((short)this.Title);

                buffer.WriteByte(0);//Stuff

                buffer.WriteByte(1);

                buffer.WriteLong(this.Experience);
                buffer.WriteShort(0);//Freepoints
                buffer.WriteShort(0);
                buffer.WriteShort(0);
                buffer.WriteInt(0);

                buffer.WriteInt(1);//Nation
            }
            buffer.EndMarkShort(this.ID, -2);
        }
    }
}
