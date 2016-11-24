using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueSnakeGame
{
    class Util
    {
        private static Random random = new Random();

        public static int Range(int min, int max)
        {
            return random.Next(max - min) + min;
        }
    }
}
