using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Utils.Colour;

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
            Vector3 _newPos = _packet.ReadVector3();
            ///<summary>Update this client's server 'player' position value</summary>
            Server.clients[_fromClient].player.position = _newPos;
            ServerSend.UpdatePosition(_fromClient, _newPos);
        }

        public static void UpdateRotationReceived(int _fromClient, Packet _packet)
        {
            Quaternion _newRot = _packet.ReadQuaternion();
            ///<summary>Update this client's server 'player' rotation value</summary>
            Server.clients[_fromClient].player.rotation = _newRot;
            ServerSend.UpdateRotation(_fromClient, _newRot);
        }

        public static void TextChatReceived(int _fromClient, Packet _packet)
        {
            string _username = _packet.ReadString();
            string _msg = _packet.ReadString();
            Colour _colour = _packet.ReadColour();

            Console.WriteLine($"Player {_fromClient} ({_username}) sent message {_msg}.");
           
            //TODO: fix the worst censor ever
            if (_msg.ToLower().Contains("sublime") || _msg.ToLower().Contains("axel"))
            {    
                _msg = "";
                foreach(char c in _msg)
                {
                    _msg += "*";
                }
            }
            string _formatted = new String($"{_username}: {_msg}");

            ServerSend.TextChat(_fromClient, _formatted, _colour);
        }

        public static void RigidUpdateReceived(int _fromClient, Packet _packet)
        {
            //TODO: Object sends a "rigid ID", rigiud manager script in game would search and move the correct one
            int _rigidId = _packet.ReadInt();
            Vector3 _newPos = _packet.ReadVector3();
            ServerSend.RigidUpdate(_fromClient, _rigidId, _newPos);
            
        }

        public static void PlayerDamageReceived(int _fromClient, Packet _packet)
        {
            //(we have player who sent)
            //Player ID damaged (make a server side disctionary)
            //Float for health taken
        }
    }
}