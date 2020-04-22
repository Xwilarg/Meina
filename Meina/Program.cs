using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using TerrariaBot;
using TerrariaBot.Client;
using TerrariaBot.Entity;
using TerrariaBot.Steam.Client;

namespace Meina
{
    class Program
    {
        private AClient client;
        private readonly AutoResetEvent autoEvent = new AutoResetEvent(false);
        private readonly PlayerInformation myInfos = new PlayerInformation("Meina", 11, new Color(255, 248, 220), new Color(245, 245, 220), new Color(0, 0, 255),
            new Color(255, 0, 0), new Color(127, 0, 0), new Color(255, 248, 220), new Color(0, 0, 0), PlayerDifficulty.Easy);
        private Random rand = new Random();
        static void Main(string[] _)
            => new Program().Start();

        public Program()
        {
            try
            {
                Console.WriteLine("Enter your connection method:");
                Console.WriteLine("1. IP");
                Console.WriteLine("2. Steam");
                char choice;
                do
                {
                    choice = Console.ReadKey().KeyChar;
                } while (choice == '1' || choice == '2');
                string ip;
                if (choice == '1')
                {
                    client = new IPClient();
                    Console.WriteLine("Enter IP address:");
                    ip = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Enter Steam ID to join:");
                    ip = Console.ReadLine();
                }
                client.ServerJoined += Ai;
                client.Log += Log;
                client.ChatMessageReceived += Chat;
                Console.WriteLine("Enter password if any:");
                string password = Console.ReadLine();
                if (choice == '1')
                {
                    ((IPClient)client).ConnectWithIP(ip, myInfos, password);
                }
                else
                {
                    ((SteamClient)client).ConnectWithSteamId(ulong.Parse(ip), myInfos, password);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("Can't connect to server: " + se.Message + Environment.NewLine + "Make sure that your Terraria server is online.");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return;
            }
        }

        public void Start()
        {
            autoEvent.WaitOne();
        }

        private void Chat(Player author, string message)
        {
            var me = client.GetPlayerSelf();
            if (message.ToLower().StartsWith("meina"))
            {
                message = message.Substring(5).Trim().ToLower();
                if (message.StartsWith("go to"))
                {
                    message = message.Substring(5).Trim();
                    var player = client.GetAllPlayers().Where(x => x.GetName().ToLower() == message).FirstOrDefault();
                    if (player == null) me.SendChatMessage("There is nobody with that name here");
                    else
                    {
                        SendSuccessMessage(me);
                        me.Teleport(player.GetPosition());
                    }
                }
            }
        }

        private void SendSuccessMessage(PlayerSelf me)
        {
            me.SendChatMessage(okayLines[rand.Next(0, okayLines.Length)]);
        }

        private void Ai(PlayerSelf me)
        {
            me.SendChatMessage("I'm connected!");
            me.JoinTeam(Team.Red);
            me.TogglePVP(true);
        }

        private void Log(LogLevel logLevel, string message)
        {
            var color = Console.ForegroundColor;
            switch (logLevel)
            {
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;

                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }

        private string[] okayLines = new[]
        {
            "Sure", "Okay", "Roger", "Yup", "Right away"
        };
    }
}
