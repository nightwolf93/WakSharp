using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

namespace WakSharp.Database
{
    public static class Storage
    {
        public static List<Models.Character> Characters = new List<Models.Character>();

        public static void Initialize()
        {
            LoadCharacters();
        }

        private static void LoadCharacters()
        {
            Utilities.ConsoleStyle.Infos("Loading @characters@ ..");
            var query = new MySqlCommand("SELECT * FROM characters", DatabaseManager.Connection);
            var reader = query.ExecuteReader();
            while (reader.Read())
            {
                var character = new Models.Character()
                {
                    ID = reader.GetInt32("id"),
                    Account = reader.GetInt32("account"),
                    Nickname = reader.GetString("nickname"),
                    Level = reader.GetInt32("level"),
                    Experience = reader.GetInt64("experience"),
                    Sex = reader.GetInt32("sex"),
                    Breed = reader.GetInt32("breed"),
                    SkinColor = reader.GetInt32("skincolor"),
                    HairColor = reader.GetInt32("haircolor"),
                    PupilColor = reader.GetInt32("pupilcolor"),
                    SkinColorFactor = reader.GetInt32("skincolorfactor"),
                    HairColorFactor = reader.GetInt32("haircolorfactor"),
                    Cloth = reader.GetInt32("cloth"),
                    Face = reader.GetInt32("face"),
                    Title = reader.GetInt32("title"),
                };
                Characters.Add(character);
            }
            reader.Close();
            Utilities.ConsoleStyle.Infos("Loaded @'" + Characters.Count + "'@ characters !");
        }

        public static void AddCharacter(Models.Character character)
        {
            var query = new MySqlCommand("INSERT INTO characters (id, account, nickname, level, experience, sex, breed, skincolor," + 
                " haircolor, pupilcolor, skincolorfactor, haircolorfactor, cloth, face, title) VALUES " +
                "(@id, @account, @nickname, @level, @experience, @sex, @breed, @skincolor, @haircolor," +
                " @pupilcolor, @skincolorfactor, @haircolorfactor, @cloth, @face, @title)", DatabaseManager.Connection);

            query.Parameters.Add(new MySqlParameter("@id", character.ID));
            query.Parameters.Add(new MySqlParameter("@account", character.Account));
            query.Parameters.Add(new MySqlParameter("@nickname", character.Nickname));
            query.Parameters.Add(new MySqlParameter("@level", character.Level));
            query.Parameters.Add(new MySqlParameter("@experience", character.Experience));
            query.Parameters.Add(new MySqlParameter("@sex", character.Sex));
            query.Parameters.Add(new MySqlParameter("@breed", character.Breed));
            query.Parameters.Add(new MySqlParameter("@skincolor", character.SkinColor));
            query.Parameters.Add(new MySqlParameter("@haircolor", character.HairColor));
            query.Parameters.Add(new MySqlParameter("@pupilcolor", character.PupilColor));
            query.Parameters.Add(new MySqlParameter("@skincolorfactor", character.SkinColorFactor));
            query.Parameters.Add(new MySqlParameter("@haircolorfactor", character.HairColorFactor));
            query.Parameters.Add(new MySqlParameter("@cloth", character.Cloth));
            query.Parameters.Add(new MySqlParameter("@face", character.Face));
            query.Parameters.Add(new MySqlParameter("@title", character.Title));

            query.ExecuteNonQuery();

            Characters.Add(character);
        }
    }
}
