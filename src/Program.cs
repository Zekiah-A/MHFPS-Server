using System;
using System.Threading;
using System.Collections.Generic;
/// Sublime Server
namespace MHFPS_Server
{
    class Program
    {
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
            Console.WriteLine("Enter server gamemode:\n(1)Deathmatch,\n(2)PlayersVsEnemies,\n(3)TeamDeathmatch");
            var response = Console.ReadKey(true);
            Console.SetCursorPosition(0, Console.CursorTop - 4);
            Console.Write("");
            Console.ResetColor();
            if (response.ToString() == "1")
                GameLogic.CurrentGamemode = (int)GameLogic.Gamemodes.Deathmatch;
            else if (response.ToString() == "2")
                GameLogic.CurrentGamemode = (int)GameLogic.Gamemodes.PlayersVsEnemies;
            else if (response.ToString() == "3")
                GameLogic.CurrentGamemode = (int)GameLogic.Gamemodes.TeamDeathmatch;
        }
    }
}
