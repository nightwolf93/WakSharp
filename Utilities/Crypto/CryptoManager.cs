using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Utilities.Crypto
{
    public static class CryptoManager
    {
        public static WakSharp.Utilities.Crypto.RSA.RSAProvider RSA { get; set; }
        public static List<byte> RsaPublicKey { get; set; }

        public static void InitRSA()
        {
            RSA = new WakSharp.Utilities.Crypto.RSA.RSAProvider(1024);
            RsaPublicKey = RSA.ExportToX509().ToList();
            Utilities.ConsoleStyle.Infos("RSA @key pairs@ initialized !");
        }
    }
}
