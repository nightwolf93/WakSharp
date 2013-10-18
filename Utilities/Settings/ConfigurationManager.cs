using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace WakSharp.Utilities.Settings
{
    public class ConfigurationManager
    {
        public static ServerConfiguration Server { get; set; }

        public static void LoadConfiguration()
        {
            try
            {
                var configName = "config.properties";
                var reader = new StreamReader(configName);
                var data = reader.ReadToEnd();
                reader.Close();
                Server = JsonConvert.DeserializeObject<ServerConfiguration>(data);

                Utilities.ConsoleStyle.Infos("@Configuration@ loaded !");
            }
            catch (FileNotFoundException e)
            {
                Utilities.ConsoleStyle.Error("Configuration file @not found@ : " + e.ToString());
            }
            catch (Exception e)
            {
                Utilities.ConsoleStyle.Error("@Can't read@ the configuration file : " + e.ToString());
            }
        }
    }
}
