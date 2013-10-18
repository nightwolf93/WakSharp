using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SilverSock;

namespace WakSharp.Network.Realm
{
    public class RealmSession
    {
        private SilverSocket _socket { get; set; }

        public Database.Models.AccountModel Account { get; set; }

        public RealmSession(SilverSocket socket)
        {
            this._socket = socket;
            this._socket.OnDataArrivalEvent += new SilverEvents.DataArrival(_socket_OnDataArrivalEvent);
            this._socket.OnSocketClosedEvent += new SilverEvents.SocketClosed(_socket_OnSocketClosedEvent);
        }

        private void _socket_OnSocketClosedEvent()
        {
            RealmServer.Clients.Remove(this);
            Utilities.ConsoleStyle.Infos("Client disconnected !");
        }

        private void _socket_OnDataArrivalEvent(byte[] data)
        {
            try
            {
                var packet = new WakfuClientMessage(data);
                Utilities.ConsoleStyle.Debug("<< OPCODE : " + packet.OPCode.ToString() + ", size : " + packet.Size);

                switch (packet.OPCode)
                {
                    case WakfuOPCode.CMSG_VERSION:
                        this.Handle_CMSG_VERSION(new Packets.CMSG_VERSION(data));
                        break;

                    case WakfuOPCode.CMSG_LOGINREQUEST:
                        this.Handle_CMSG_LOGINREQUEST(new Packets.CMSG_LOGINREQUEST(data));
                        break;
                }
            }
            catch (Exception e)
            {
                Utilities.ConsoleStyle.Error("Can't read packet : " + e.ToString());
            }
        }

        public void Send(WakfuServerMessage packet)
        {
            try
            {
                this._socket.Send(packet.Build());
                Utilities.ConsoleStyle.Debug(">> OPCODE : " + packet.OPCode.ToString() + ", size : " + packet.Size);
            }
            catch (Exception e)
            {
                Utilities.ConsoleStyle.Error("Can't send packet : " + e.ToString());
            }
        }

        private void Handle_CMSG_VERSION(Packets.CMSG_VERSION packet)
        {
            Utilities.ConsoleStyle.Debug("Check client version .. " + packet.ToString());
            if (packet.ToString() == Utilities.Settings.ConfigurationManager.Server.WakfuVersion)
            {
                Utilities.ConsoleStyle.Debug("Versions are the same");
                this.Send(new Packets.SMSG_RSAKEY(Utilities.Crypto.CryptoManager.RsaPublicKey.ToArray()));
            }
            else
            {
                Utilities.ConsoleStyle.Error("Versions don't match, kick the client");
            }
        }

        private void Handle_CMSG_LOGINREQUEST(Packets.CMSG_LOGINREQUEST packet)
        {
            Utilities.ConsoleStyle.Debug("Check client account .. ");
            var account = Database.Models.AccountModel.FindOne(packet.Username);
            if (account != null)
            {
                if (account.Password == packet.Password)//Check password
                {
                    this.Account = account;//Client is logged
                    this.Send(new Packets.SMSG_LOGINRESULT(Enums.LoginResultEnum.CORRECT_LOGIN, account.ID, account.IsOp(), account.Pseudo));

                    Utilities.ConsoleStyle.Infos("Player @'" + account.Username + "'@ connected !");
                }
                else
                {
                    Utilities.ConsoleStyle.Error("Password don't match");
                }
            }
            else
            {
                Utilities.ConsoleStyle.Error("Can't found the account @'" + packet.Username + "'@");
            }
        }
    }
}
