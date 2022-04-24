using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
namespace SimplestGamePossible
{
    internal static class Time
    {
        static Clock clock;
        static float frameTime;
        static double gameTime;
        public static float FrameTime { get => frameTime; }
        static Time() 
        {
            clock = new Clock();
            frameTime = 0;
            gameTime = 0;
        }
        public static void Click() 
        {
            frameTime = clock.Restart().AsSeconds();
            gameTime += frameTime;
        }

    }
}
