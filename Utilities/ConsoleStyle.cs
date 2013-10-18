using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakSharp.Utilities
{
    public class ConsoleStyle
    {
        public static object Locker = new object();
        public static bool DebugMode = true;

        public static void DrawAscii()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n________________________________________________________________________________");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("     Thank for KevinSupertramp, Velocity, LittleScaraby, DozenOfElites.com");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"                    _ _ _     _   _____ _               ");
            Console.WriteLine(@"                   | | | |___| |_|   __| |_ ___ ___ ___ ");
            Console.WriteLine(@"                   | | | | .'| '_|__   |   | .'|  _| . | By NightWolf");
            Console.WriteLine(@"                   |_____|__,|_,_|_____|_|_|__,|_| |  _| Alpha");
            Console.WriteLine(@"                                                   |_|   v" + Utilities.Settings.ConfigurationManager.Server.ServerVersion);
            Console.WriteLine("________________________________________________________________________________");
        }

        public static void Append(string header, string message, ConsoleColor headcolor)
        {
            lock (Locker)
            {
                Console.ForegroundColor = headcolor;
                Console.Write(header);
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Gray;
                foreach (var c in message)
                {
                    if (c == '@')
                    {
                        if (Console.ForegroundColor == ConsoleColor.Gray)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                    else
                    {
                        Console.Write(c);
                    }
                }
                Console.Write("\n");
            }
        }

        public static void Infos(string message)
        {
            Append("[Infos]", message, ConsoleColor.Green);
        }

        public static void Error(string message)
        {
            Append("[Error]", message, ConsoleColor.Red);
        }

        public static void Debug(string message)
        {
            if (DebugMode)
            {
                Append("[Debug]", message, ConsoleColor.Magenta);
            }
        }

        public static void Warning(string message)
        {
            Append("[Warning]", message, ConsoleColor.Yellow);
        }
    }
}
