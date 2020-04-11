using System;
using System.IO;
using System.Net.Sockets;
using TerrariaBot;

namespace Meina
{
    class Program
    {
        private static TerrariaClient client;
        static void Main(string[] args)
        {
            try
            {
                client = new TerrariaClient("localhost", new PlayerInformation("Meina", 11, new Color(255, 248, 220), new Color(255, 255, 153), new Color(0, 0, 255),
                    new Color(255, 0, 0), new Color(127, 0, 0), new Color(255, 248, 220), new Color(0, 0, 0), PlayerDifficulty.Easy), File.Exists("password.txt") ? File.ReadAllText("password.txt") : "");
            }
            catch (SocketException se)
            {
                Console.WriteLine("Can't connect to server: " + se.Message + Environment.NewLine + "Make sure that your Terraria server is online.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }
            client.ServerJoined += Ai;
            Console.WriteLine("Bot is connected");
            while (true)
            { }
        }

        private static void Ai()
        {
            client.JoinTeam(Team.Red);
        }
    }
}
