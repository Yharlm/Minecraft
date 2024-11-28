
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

        public List<Non_solid> Block_Back_list = new List<Non_solid>();
        public List<Solid> Block_list = new List<Solid>();
        public bool is_swiming = false;
        public int hotbar;
        //public Block_ids Selected_block = null;
        public Solid Selected_block = null;
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
        //public List<Solid> Block_list = new List<Solid>();
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
        public bool curent_tick = false;


    }

    class Block_ids(int Id, string texture, ConsoleColor fG, ConsoleColor bG)
    {
        public int id = Id;
        public string Texture = texture;
        public ConsoleColor FG = fG;
        public ConsoleColor BG = bG;
    }
    class Solid(string name, int Id, string texture, ConsoleColor fG, ConsoleColor bG)
    {
        public string Name = name;
        public int id = Id;
        public string Texture = texture;
        public ConsoleColor FG = fG;
        public ConsoleColor BG = bG;
    }
    class Non_solid(string name,int Id, string texture, ConsoleColor fG, ConsoleColor bG)
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
    internal class Behaviour
    {
        class pig : Behaviour
        {

        }
        public void Walk_around()
        {
            //code here lol
        }
        public void Walk_To_player(int speed)
        {
            //code here lol
        }
        public void Shoot_WithImaginaryProjectiles()
        {

        }

    }
    class Entity(string name, int Health, Behaviour type)
    {

    }
}
