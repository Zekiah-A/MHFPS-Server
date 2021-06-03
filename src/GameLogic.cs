using System;
using System.Collections.Generic;
using System.Text;

namespace MHFPS_Server
{
    class GameLogic
    {
        public static int CurrentGamemode;

        public enum Gamemodes
        {
            Deathmatch,
            PlayersVsEnemies,
            TeamDeathmatch
        }

        public static void Update()
        {
            ThreadManager.UpdateMain();
        }
    }
}