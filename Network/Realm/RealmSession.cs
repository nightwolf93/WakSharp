using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SilverSock;

namespace WakSharp.Network.Realm
{
    public class RealmSession
    {
        #region Fields

        private SilverSocket _socket { get; set; }

        public Database.Models.Account Account { get; set; }
        public WakfuWorld World { get; set; }

        #endregion

        #region Builders

        public RealmSession(SilverSocket socket)
        {
            this._socket = socket;
            this._socket.OnDataArrivalEvent += new SilverEvents.DataArrival(_socket_OnDataArrivalEvent);
            this._socket.OnSocketClosedEvent += new SilverEvents.SocketClosed(_socket_OnSocketClosedEvent);
            this.Send(new Packets.SMSG_110());
        }

        #endregion

        #region Events

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
                Utilities.ConsoleStyle.Debug("<< OPCODE : " + packet.OPCode.ToString() + "(" + (int)packet.OPCode + "), size : " + packet.Size);

                switch (packet.OPCode)
                {
                    case WakfuOPCode.CMSG_VERSION:
                        this.Handle_CMSG_VERSION(new Packets.CMSG_VERSION(data));
                        break;

                    case WakfuOPCode.CMSG_LOGINREQUEST:
                        this.Handle_CMSG_LOGINREQUEST(new Packets.CMSG_LOGINREQUEST(data));
                        break;

                    case WakfuOPCode.CMSG_WORLDSELECT:
                        this.Handle_CMSG_WORLDSELECT(new Packets.CMSG_WORLDSELECT(data));
                        break;

                    case WakfuOPCode.CMSG_CHARACTERCREATIONREQUEST:
                        this.Handle_CMSG_CHARACTERCREATIONREQUEST(new Packets.CMSG_CHARACTERCREATIONREQUEST(data));
                        break;
                }
            }
            catch (Exception e)
            {
                Utilities.ConsoleStyle.Error("Can't read packet : " + e.ToString());
            }
        }

        #endregion

        #region Public Methods

        public void Send(WakfuServerMessage packet)
        {
            try
            {
                this._socket.Send(packet.Build());
                Utilities.ConsoleStyle.Debug(">> OPCODE : " + packet.OPCode.ToString() + "(" + (int)packet.OPCode + "), size : " + packet.Size);
                packet.Dump(packet.OPCode.ToString());
            }
            catch (Exception e)
            {
                Utilities.ConsoleStyle.Error("Can't send packet : " + e.ToString());
            }
        }

        public List<Database.Models.Character> GetCharacters()
        {
            var characters = new List<Database.Models.Character>();
            if (this.Account != null)
            {
                lock (Database.Storage.Characters) { characters = Database.Storage.Characters.FindAll(x => x.Account == this.Account.ID); }
            }
            return characters;
        }

        #endregion

        #region Parsing / Handling

        /// <summary>
        /// Check client version if is outdated
        /// </summary>
        /// <param name="packet"></param>
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

        /// <summary>
        /// Check username and password and authentificate him
        /// </summary>
        /// <param name="packet"></param>
        private void Handle_CMSG_LOGINREQUEST(Packets.CMSG_LOGINREQUEST packet)
        {
            Utilities.ConsoleStyle.Debug("Check client account .. ");
            var account = Database.Models.Account.FindOne(packet.Username);
            if (account != null)
            {
                if (account.Password == packet.Password)//Check password
                {
                    this.Account = account;//Client is logged
                    this.Send(new Packets.SMSG_LOGINRESULT(Enums.LoginResultEnum.CORRECT_LOGIN, account, account.ID, account.IsOp(), account.Pseudo));
                    Utilities.ConsoleStyle.Infos("Player @'" + account.Username + "'@ connected !");

                    this.Send_SMSG_LISTWORLDS();
                }
                else
                {
                    Utilities.ConsoleStyle.Error("Password don't match");
                    this.Send(new Packets.SMSG_LOGINRESULT(Enums.LoginResultEnum.INVALID_LOGIN, null, account.ID, account.IsOp(), account.Pseudo));
                }
            }
            else
            {
                Utilities.ConsoleStyle.Error("Can't found the account @'" + packet.Username + "'@");
                this.Send(new Packets.SMSG_LOGINRESULT(Enums.LoginResultEnum.INVALID_LOGIN, null, -1, false, ""));
            }
        }

        /// <summary>
        /// Display the list of worlds
        /// </summary>
        public void Send_SMSG_LISTWORLDS()
        {
            this.Send(new Packets.SMSG_LISTWORLDS(Utilities.Settings.ConfigurationManager.Server.Worlds));
        }

        /// <summary>
        /// Switch beetwen login and world
        /// </summary>
        /// <param name="packet"></param>
        private void Handle_CMSG_WORLDSELECT(Packets.CMSG_WORLDSELECT packet)
        {
            Utilities.ConsoleStyle.Debug("Select world : " + packet.WorldID);
            this.World = Utilities.Settings.ConfigurationManager.Server.Worlds.FirstOrDefault(x => x.ID == packet.WorldID);

            this.Send(new Packets.SMSG_WORLDSELECTRESULT(this.World.ID, true));
            this.Send_SMSG_SERVERTIME();
            this.Send(new Packets.SMSG_CHARACTERSLIST(this.GetCharacters()));
        }

        /// <summary>
        /// Send server time
        /// </summary>
        public void Send_SMSG_SERVERTIME()
        {
            DateTime Yesterday = DateTime.Now.AddDays(-1);
            TimeSpan TS = (TimeSpan)(DateTime.Now - Yesterday);
            this.Send(new Packets.SMSG_SERVERTIME((long)TS.TotalMilliseconds));
        }

        /// <summary>
        /// Handle the character creation, check limit, nickname etc..
        /// </summary>
        /// <param name="packet"></param>
        private void Handle_CMSG_CHARACTERCREATIONREQUEST(Packets.CMSG_CHARACTERCREATIONREQUEST packet)
        {
            Utilities.ConsoleStyle.Debug("Player @'" + this.Account.Username + "'@ try to create a character named @'" + packet.Name + "'@");
            if (packet.Name.Length <= 15)
            {
                //TODO: Check if character exist
                var character = new Database.Models.Character()
                {
                    ID = 1, //TODO: ID Generator
                    Nickname = packet.Name,
                    Level = 1,
                    Experience = 0,
                    Sex = packet.Sex,
                    Breed = packet.Breed,
                    SkinColor = packet.SkinColor,
                    HairColor = packet.HairColor,
                    PupilColor = packet.PupilColor,
                    SkinColorFactor = packet.SkinColorFactor,
                    HairColorFactor = packet.HairColorFactor,
                    Cloth = packet.Cloth,
                    Face = packet.Face,
                    Title = -1,
                    Account = this.Account.ID,
                };

                Database.Storage.AddCharacter(character);
                Utilities.ConsoleStyle.Infos("Player @'" + this.Account.Username + "'@ create the character @'" + character.Nickname + "'@ with success");
                //TODO: Send ok message
            }
            else
            {
                //TODO: Error message
            }
        }

        #endregion
    }
}
