using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SilverSock;

namespace WakSharp.Network.Realm
{
    public class RealmServer
    {
        public static SilverServer Server { get; set; }

        public static void Initialize()
        {
            Utilities.ConsoleStyle.Infos("Intializing @RealmServer@ ..");

            Server = new SilverServer(Utilities.Settings.ConfigurationManager.Server.Realm.Host, Utilities.Settings.ConfigurationManager.Server.Realm.Port);
            Server.OnListeningEvent += new SilverEvents.Listening(Server_OnListeningEvent);
            Server.OnAcceptSocketEvent += new SilverEvents.AcceptSocket(Server_OnAcceptSocketEvent);
            Server.OnListeningFailedEvent += new SilverEvents.ListeningFailed(Server_OnListeningFailedEvent);
            Server.WaitConnection();
        }

        private static void Server_OnListeningFailedEvent(Exception ex)
        {
            Utilities.ConsoleStyle.Error("@Can't listen@ for RealmServer");
        }

        private static void Server_OnAcceptSocketEvent(SilverSocket socket)
        {
            Utilities.ConsoleStyle.Debug("Input connection on @realm server@");
            var session = new RealmSession(socket);

        }

        private static void Server_OnListeningEvent()
        {
            Utilities.ConsoleStyle.Infos("@RealmServer@ wait for connection !");
        }
    }
}
