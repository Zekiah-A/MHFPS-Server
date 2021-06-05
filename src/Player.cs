using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MHFPS_Server
{
    public class Player
    {
        public int id;
        public string username;

        ///<summary> Current position of player </summary>
        public Vector3 position;
        ///<summary> Current rotation of player </summary>
        public Quaternion rotation;

        ///<summary> Player health and status. </summary>
        //TODO: Make a field for current holding item, to verify if they didn't cheat
        public float health;
        public bool isDead;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position =  _spawnPosition;
            rotation = Quaternion.Identity;
            
        }
    }
}