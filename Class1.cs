using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    class Player : cordinates
    {
        public Block_ids Selected_block;
        public string special_key = "";
        public string last_key = "";
        public string Input = "";
        //public int x = 4;
        //public int y = 11;
        public int x_size = 70;
        public int y_size = 34;
        public bool grounded = false;
    }
    class Inventory : Player
    {
        private int[] inventory = new int[4];
        public Block_ids Selected_block;
        public void addItem(object block)
        {
            Block_ids item = (Block_ids)block;
            int index = item.id;
            
        }
    }
    class cordinates
    {
        public int x = 4;
        public int y = 11;


    }

    class Game : Player
    {
        public int x_size = 30;
        public int y_size = 13;
        public double time = 0;



    }

    class Block_ids(int Id, string texture, ConsoleColor fG, ConsoleColor bG)
    {
        public int id = Id;
        public string Texture = texture;
        public ConsoleColor FG = fG;
        public ConsoleColor BG = bG;
    }
    //▀▄░█
    class Structure
    {
        public int[,] Struct;
        public void Fill_Index_Cord(int x1, int y1, int x2, int y2, int[,] grid, int id)
        {



        }
    }
    

}
