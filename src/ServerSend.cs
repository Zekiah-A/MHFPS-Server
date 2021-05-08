using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Utils.Colour;

namespace MHFPS_Server
{
    class ServerSend
    {
        #region Send Methods
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet  _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        public static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i  = 0; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }

        public static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i  = 0; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        public static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i  = 1; i <= Server.MaxPlayers; i++) //fixed bruh
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
        
        public static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i  = 1; i <= Server.MaxPlayers; i++) //i = 1?
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet); //client 0 doesn't exist!
                } //maybe add a try/catch here? ^ 
            }
        }
        ///TODO: Issue with sending data to all (exceptions cause socket closure)
        ///Unhandled exception.
        ///System.Collections.Generic.KeyNotFoundException:
        ///The given key '0' was not present in the dictionary.
        #endregion

        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void UDPTest(int _toClient)
        {
            using (Packet _packet = new Packet((int)ServerPackets.udpTest))
            {
                _packet.Write("Test packet for UDP connection.");

                SendUDPData(_toClient, _packet);
            }
        }


        public static void SpawnPlayer(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.position);
                _packet.Write(_player.rotation);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void UpdatePosition(int _exceptClient, Vector3 _newPos)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
            {
                _packet.Write(_exceptClient);
                _packet.Write(_newPos);
                SendUDPDataToAll(_exceptClient, _packet);
            }
        }

        public static void UpdateRotation(int _exceptClient, Quaternion _newRot)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
            {
                _packet.Write(_exceptClient);
                _packet.Write(_newRot);
                SendUDPDataToAll(_exceptClient, _packet);
            }
        }

        public static void TextChat(int _fromClient, string _formatted, Colour _colour)
        {
            using (Packet _packet = new Packet((int)ServerPackets.textChat))
            {
                _packet.Write(_fromClient);
                _packet.Write(_formatted);
                _packet.Write(_colour);
                SendUDPDataToAll(_packet);
            }
        }
        #endregion
    }
}