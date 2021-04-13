using System;
using System.Collections.Generic;
using System.Text;

namespace MHFPS_Server
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIDCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIDCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assigned the wrong client ID ({_clientIDCheck})!");
            }
            //TODO: Send player into game!
        }
    }
}