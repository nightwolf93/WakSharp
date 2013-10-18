using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.Packets
{
    public class CMSG_LOGINREQUEST : WakfuClientMessage
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public CMSG_LOGINREQUEST(byte[] data)
            : base(data)
        {
            var b = new byte[data.Length - 5];
            b = this.Reader.ReadBytes(data.Length - 5);
            b = Utilities.Crypto.CryptoManager.RSA.Decrypt(b, false);
            var decoded = new IO.BigEndianReader(b);
            var rsaVerification = decoded.ReadULong();
            var username = decoded.ReadString();
            var password = decoded.ReadString();

            this.Username = username;
            this.Password = password;
        }
    }
}
