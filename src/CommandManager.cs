using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Utils.Colour;

namespace MHFPS_Server
{
    class CommandManager
    {
        public static async void InterpCommand() //TODO: Make a thread call this? 
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    //await ReadConsoleAsync();
                    Console.Write("> ");
                    string consoleInput = Console.ReadLine();

                    string[] consoleParts = consoleInput.Split(' ');
                    switch(consoleParts[0].ToLower())
                    {
                        case "help":
                            Console.WriteLine("Commands:\nAnnounce\nAdmin\nGive\nHealth\nDisconnect\nExit");
                            break;
                        case "announce":
                            ServerSend.TextChat(0, consoleInput.Replace("announce", "Server:"), new Colour(255, 0, 230, 255));
                            break;
                        case "admin": //Can only be issued by server, no packet
                            //TODO: give player by ID admin perms (/give command, change gamemode).
                            break;
                        case "give":
                            break;
                        case "health":
                            ///<summary> See the health of a player by ID. </summary>
                            if (consoleParts.Count() <= 2)
                            {
                                try {
                                    Console.WriteLine($"Health of player {Server.clients[int.Parse(consoleParts[1])].player.username} is: {Server.clients[int.Parse(consoleParts[1])].player.health}");
                                } catch {} ///<note> Hack to prevent crash on exception</note>
                            }
                            ///<summary> Set player health. </summary>
                            else
                            {
                                try {
                                    Server.clients[int.Parse(consoleParts[1])].player.health = int.Parse(consoleParts[2]);
                                } catch {}
                            }
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
                });
            }
        }
    }
}