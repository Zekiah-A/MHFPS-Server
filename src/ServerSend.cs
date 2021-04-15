using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

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
            for (int i  = 0; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
        
        public static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i  = 0; i <= Server.MaxPlayers; i++) //i = 1?
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }
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

            UpdatePosition();
        }

        public static void UpdatePosition(/*int _exceptClient, Vector3 _newPos, Player _player*/) //updates player pos
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
            { //playerPosition
                _packet.Write(1/*_player.id*/);
                _packet.Write(new Vector3(10,10,10)/*_newPos*/);
                SendTCPData(1, _packet/*_exceptClient, _packet*/);
                //SendTCPData(0, _packet/*_exceptClient, _packet*/);
                //SendTCPData(3, _packet/*_exceptClient, _packet*/);
                ///////////////
                Console.WriteLine("SENT A POSITION PACKET");
            }
        }

        public static void UpdateRotation(int _exceptClient, Quaternion _newRot, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
            { //playerRotation
                _packet.Write(_player.id);
                _packet.Write(_newRot); ///we also need to  sendd the player that has moved
                SendUDPDataToAll(_exceptClient, _packet);
            }
        }
        #endregion
    }
}