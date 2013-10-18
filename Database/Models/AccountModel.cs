using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

namespace WakSharp.Database.Models
{
    public class AccountModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Pseudo { get; set; }
        public int Rank { get; set; }

        public static AccountModel FindOne(string username)
        {
            AccountModel account = null;
            var command = new MySqlCommand("SELECT * FROM accounts WHERE username=@username", DatabaseManager.Connection);
            command.Parameters.Add(new MySqlParameter("@username", username));
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                account = new AccountModel()
                {
                    ID = reader.GetInt32("id"),
                    Username = reader.GetString("username"),
                    Password = reader.GetString("password"),
                    Pseudo = reader.GetString("pseudo"),
                    Rank = reader.GetInt32("rank"),
                };
            }
            reader.Close();
            return account;
        }

        public bool IsOp()
        {
            return this.Rank > 0;
        }
    }
}
