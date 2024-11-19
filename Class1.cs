using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    class Player
    {
        public string last_key = "";
        public string Input = "";
        public int x = 4;
        public int y = 11;
        public int x_size = 70;
        public int y_size = 34;
        public bool grounded = false;
    }

    class cordinates : Player
    {
        public int x = 4;
        public int y = 4;


    }

    class Game
    {
        public int x_size = 30;
        public int y_size = 13;

    }

    class Block_ids(int Id, string texture, ConsoleColor fG, ConsoleColor bG)
    {
        public int id = Id;
        public string Texture = texture;
        public ConsoleColor FG = fG;
        public ConsoleColor BG = bG;
    }
    //▀▄░█
}
