using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Utils.Colour;
/// Sublime Server
namespace MHFPS_Server
{
    class Program
    {
        private static string consoleInput;

        private static bool isRunning = false;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sublime-Server v0.0.4"); 
            Console.WriteLine("By zekiahepic");
            Console.ResetColor();
            SelectGamemode();

            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            Server.Start(50, 19130);

            InterpCommand();
        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SECOND} ticks per second");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);
                }
                
                ///<summary>Done for performance, thread not left doing nothing.</summary>
                if (_nextLoop > DateTime.Now)
                {
                    Thread.Sleep(_nextLoop - DateTime.Now);
                }
            }
        }

        private static void SelectGamemode()
        {
            Console.WriteLine($"Enter server gamemode:\n(1){GameLogic.Gamemodes.Deathmatch},\n(2){GameLogic.Gamemodes.PlayersVsEnemies},\n(3){GameLogic.Gamemodes.TeamDeathmatch}");
            var response = Console.ReadKey(true);
            Console.SetCursorPosition(0, Console.CursorTop - 4);
            Console.Write("");
            Console.ResetColor();

            switch (response.ToString())
            {
                case "1":
                    GameLogic.CurrentGamemode = (int)GameLogic.Gamemodes.Deathmatch;
                    break;
                case "2":
                    GameLogic.CurrentGamemode = (int)GameLogic.Gamemodes.PlayersVsEnemies;
                    break;
                case "3":
                 GameLogic.CurrentGamemode = (int)GameLogic.Gamemodes.TeamDeathmatch;
                    break;
                default:
                    GameLogic.CurrentGamemode = (int)GameLogic.Gamemodes.Deathmatch;
                    break;
            }
        }

        private static async void InterpCommand()
        {
            while(true)
            {
                await ReadConsoleAsync();
                
                string[] consoleParts = consoleInput.Split(' ');
                switch(consoleParts[0].ToLower())
                {
                    case "help":
                        Console.WriteLine("Commands:\nAnnounce\nAdmin\nGive\nDisconnect\nExit");
                        break;
                    case "announce":
                        ServerSend.TextChat(0, consoleInput.Replace("announce", "Server:"), new Colour(255, 0, 230, 255));
                        break;
                    case "admin": //Can only be issued by server, no packet
                        //TODO: give player by ID admin perms (/give command, change gamemode).
                        break;
                    case "give":
                        break;
                    case "disconnect":
                        //TODO: Disconnect player by ID from server.
                        break;
                    case "exit":
                        //TODO: disconnect all players first.
                        throw new Exception("\nServer Shutdown Called!");
                    default:
                        if (consoleInput != "")
                            Console.WriteLine("Invalid command. Run 'help' to view list of commands.");
                        break;
                }
            } 
        }

        private static async Task ReadConsoleAsync()
        {
            await Task.Run(() =>
            {
                consoleInput = Console.ReadLine();
                return;
            });
            return;            
        }
    }
}
