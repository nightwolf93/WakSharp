using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WakSharp.Network
{
    public class WakfuServerMessage
    {
        public WakfuOPCode OPCode = WakfuOPCode.UNKNOWN;
        public IO.BigEndianWriter Writer { get; set; }

        public WakfuServerMessage(WakfuOPCode opcode)
        {
            this.OPCode = opcode;
            this.Writer = new IO.BigEndianWriter();
            this.Writer.WriteShort(0);//Placeholder
            this.Writer.WriteShort((short)this.OPCode);
        }

        public long Size
        {
            get
            {
                return this.Writer.BaseStream.Length;
            }
        }

        public byte[] Build()
        {
            this.Writer.Seek(0, System.IO.SeekOrigin.Begin);
            this.Writer.WriteShort((short)this.Writer.BaseStream.Length);
            return this.Writer.Data;
        }

        public void PrintBuffer(bool hex)
        {
            var bufstr = BitConverter.ToString(this.Writer.Data).Replace("-", " ");       
            Utilities.ConsoleStyle.Debug(bufstr);
        }

        public void Dump(string file)
        {
            var writer = new BinaryWriter(new StreamWriter("Dumps/" + file + ".dat").BaseStream);
            writer.Write(this.Writer.Data.Length);
            writer.Write(this.Writer.Data);
            writer.BaseStream.Close();
            writer.Close();
        }
    }
}
