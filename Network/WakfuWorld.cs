using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network
{
    public class WakfuWorld
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public int Population { get; set; }

        public List<Realm.RealmSession> Players = new List<Realm.RealmSession>();

        public void Encode(WakfuServerMessage packet)
        {
            var buffer = packet.Writer;
            buffer.WriteByte(40);
            buffer.WriteByte(3);
            buffer.WriteByte(0);
            buffer.WriteInt(16);
            buffer.WriteByte(1);
            buffer.WriteInt(24);
            buffer.WriteByte(2);
            buffer.WriteInt(36);

            //World
            buffer.WriteByte(0);
            buffer.WriteInt(this.ID);
            buffer.WriteString("1W");
        
            buffer.WriteByte(1);
            buffer.WriteString(this.Name);
            buffer.WriteString(this.Language);

            buffer.WriteByte(2);
            buffer.WriteByte(1);
            buffer.WriteByte(0);
            buffer.WriteByte((byte)this.Population);
        }
    }
}
