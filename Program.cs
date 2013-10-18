using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Wakfu Engine";
            Utilities.Settings.ConfigurationManager.LoadConfiguration();

            Utilities.ConsoleStyle.DrawAscii();

            Database.DatabaseManager.Initialize();

            Utilities.Crypto.CryptoManager.InitRSA();
            Network.Realm.RealmServer.Initialize();

            //TODO : Console commands handler
            while (true) { Console.ReadLine(); }
        }
    }
}
