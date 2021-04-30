using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

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

            Server.clients[_fromClient].SendIntoGame(_username);
        }

        public static void UDPTestReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();

            Console.WriteLine($"Received packet via UDP. Contains message: {_msg}");
        }

        public static void UpdatePositionReceived(int _fromClient, Packet _packet)
        {
            //int _playerId = _packet.ReadInt(); //more clineid i guess
            Vector3 _newPos = _packet.ReadVector3();
            Client _client = Server.clients[_fromClient];
            
            ServerSend.UpdatePosition(_fromClient, _newPos); //THIS WAS THE PROPER WAY
        }

        public static void UpdateRotationReceived(int _fromClient, Packet _packet) //on recieve, send back!
        {
            //int _playerId = _packet.ReadInt();
            Quaternion _newRot = _packet.ReadQuaternion();
            Client _client = Server.clients[_fromClient];
            
            ServerSend.UpdateRotation(_fromClient, _newRot);
        }
    }
}