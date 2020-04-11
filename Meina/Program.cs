using System;
using System.Net.Sockets;
using TerrariaBot;

namespace Meina
{
    class Program
    {
        static void Exit()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Environment.Exit(1);
        }

        static void Main(string[] args)
        {
            TerrariaClient client;
            try
            {
                client = new TerrariaClient("localhost", new PlayerInformation("Meina", 8, new Color(255, 248, 220), new Color(255, 255, 153), new Color(0, 0, 255),
                    new Color(255, 0, 0), new Color(127, 0, 0), new Color(255, 248, 220), new Color(0, 0, 0), PlayerDifficulty.Easy));
            }
            catch (SocketException se)
            {
                Console.WriteLine("Can't connect to server: " + se.Message + Environment.NewLine + "Make sure that your Terraria server is online.");
                Exit();
            }
            Console.WriteLine("Bot is connected");
            while (true)
            { }
        }
    }
}
