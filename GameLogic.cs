using System;
using System.Collections.Generic;
using System.Text;

namespace MHFPS_Server
{
    class GameLogic
    {
        public static void Update()
        {
            ThreadManager.UpdateMain();
        }
    }
}