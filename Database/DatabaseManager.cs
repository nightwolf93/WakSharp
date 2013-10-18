using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

namespace WakSharp.Database
{
    public static class DatabaseManager
    {
        public static MySqlConnection Connection { get; set; }

        public static void Initialize()
        {
            try
            {
                Utilities.ConsoleStyle.Infos("Connecting to @database@ ..");
                Connection = new MySqlConnection("Server='"
                    + Utilities.Settings.ConfigurationManager.Server.Database.Host + "';Database='"
                    + Utilities.Settings.ConfigurationManager.Server.Database.Name + "';Uid='"
                    + Utilities.Settings.ConfigurationManager.Server.Database.Username + "';Pwd='"
                    + Utilities.Settings.ConfigurationManager.Server.Database.Password + "';");
                Connection.Open();
                Utilities.ConsoleStyle.Infos("Connected to @database@ !");
            }
            catch (Exception e)
            {
                Utilities.ConsoleStyle.Error("Can't connect to Database : " + e.ToString());
            }
        }
    }
}
