using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Utilities.Settings
{
    public class ServerConfiguration
    {
        public string ServerVersion { get; set; }
        public string WakfuVersion { get; set; }
        public DatabaseNode Database { get; set; }
        public ServerNode Realm { get; set; }
        public List<Network.WakfuWorld> Worlds = new List<Network.WakfuWorld>();
    }

    public class ServerNode
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class DatabaseNode
    {
        public string Host { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
