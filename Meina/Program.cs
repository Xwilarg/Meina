using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using TerrariaBot;
using TerrariaBot.Entity;

namespace Meina
{
    class Program
    {
        private static TerrariaClient client;
        private static AutoResetEvent autoEvent = new AutoResetEvent(false);
        static void Main(string[] _)
        {
            try
            {
                client = new TerrariaClient(LogLevel.Debug);
                client.ServerJoined += Ai;
                client.Connect("localhost", new PlayerInformation("Meina", 11, new Color(255, 248, 220), new Color(245, 245, 220), new Color(0, 0, 255),
                    new Color(255, 0, 0), new Color(127, 0, 0), new Color(255, 248, 220), new Color(0, 0, 0), PlayerDifficulty.Easy), File.Exists("password.txt") ? File.ReadAllText("password.txt") : "");
            }
            catch (SocketException se)
            {
                Console.WriteLine("Can't connect to server: " + se.Message + Environment.NewLine + "Make sure that your Terraria server is online.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }
            autoEvent.WaitOne();
        }

        private static void Ai(PlayerSelf me)
        {
            me.JoinTeam(Team.Red);
            me.TogglePVP(true);
        }
    }
}
