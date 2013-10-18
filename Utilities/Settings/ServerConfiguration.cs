using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Utilities.Settings
{
    public class ServerConfiguration
    {
        public string ServerVersion { get; set; }
        public ServerNode Realm { get; set; }
        public ServerNode World { get; set; }
    }

    public class ServerNode
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
