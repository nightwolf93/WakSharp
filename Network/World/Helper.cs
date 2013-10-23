using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Network.World
{
    public class Helper
    {
        public static Database.Models.Character GetCharacter(long id)
        {
            lock (Database.Storage.Characters)
            {
                if (Database.Storage.Characters.FindAll(x => x.ID == id).Count > 0)
                {
                    return Database.Storage.Characters.FirstOrDefault(x => x.ID == id);
                }
                else
                {
                    return null;
                }
            }
        }

        public static Database.Models.Character GetCharacter(string name)
        {
            lock (Database.Storage.Characters)
            {
                if (Database.Storage.Characters.FindAll(x => x.Nickname.ToLower() == name.ToLower()).Count > 0)
                {
                    return Database.Storage.Characters.FirstOrDefault(x => x.Nickname.ToLower() == name.ToLower());
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
