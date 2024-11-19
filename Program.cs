//using ConsoleNewMinigame;
using System;

namespace Minecraft
{
    class Program
    {

        protected static int origRow;
        protected static int origCol;

        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);

                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {

            }
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            WriteAt("Minecraft v0.0.2, now with grass", 1, 1);
            Console.ForegroundColor = default;

            Game overworld = new Game();
            Player player = new Player();
            //Move this to the block method using switch case or make a new class block

            
            int[,] grid = new int[player.y_size, player.x_size];
            BuildWorld(grid);
            while (true)
            {
                GetInput(grid, player);
                if (grid[player.y + 1, player.x] == 0)
                {
                    player.grounded = false;
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteAt("  ", player.x * 2, player.y-1);
                    //WriteAt("  ", player.x * 2, player.y);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    player.y++;
                    Thread.Sleep(100);


                }
                else
                {
                    player.grounded = true;
                }

            }

        }

        private static void BuildWorld(int[,] grid)
        {
            Block_ids air = new Block_ids(0, "  ", ConsoleColor.DarkGray, ConsoleColor.Cyan);
            Block_ids Grass = new Block_ids(1, "▀▀",ConsoleColor.DarkGreen,ConsoleColor.DarkYellow);
            Block_ids dirt = new Block_ids(2, "██", ConsoleColor.DarkYellow, ConsoleColor.DarkYellow);
            Block_ids stone = new Block_ids(3, "██", ConsoleColor.DarkGray, ConsoleColor.DarkGray);
            Block_ids wood = new Block_ids(4, "██", ConsoleColor.DarkGray, ConsoleColor.DarkRed);


            Fill_Index(63, 30, grid, dirt);
            //Fill_Index(63, 21, grid, Grass);
            
            Fill_Index(60,20, grid, air);
            Fill_Index_Cord(10,10,20,20, grid, stone);
        }

        private static void GetInput(int[,] grid, object instance)
        {

            Player player = (Player)instance;
            grid[player.y, player.x] = 0;
            int x = player.x;
            int y = player.y;
            if (Console.KeyAvailable == true)
            {

                player.Input = Console.ReadKey().Key.ToString();
                player.last_key = player.Input;
                WriteAt("  ", x * 2, y - 1);
                WriteAt("  ", x * 2, y);
                WriteAt("  ", 110, 0);
            }
            
            switch (player.Input)
            {
                case "W":

                    if(player.grounded == true)
                    {
                        y-=2;
                        WriteAt("██", x * 2, y - 1);
                        WriteAt("██", x * 2, y);
                        grid[player.y-1, player.x] = 0;
                        player.grounded = false;
                    }
                    
                    
                    break;
                case "A":

                    x--;
                    if(player.grounded == false)
                    {
                        WriteAt("██", x * 2, y - 1);
                        WriteAt("██", x * 2, y);
                        x -=1;
                    }

                    break;
                case "S":

                    grid[player.y+1, player.x] = 0;
                    WriteAt("  ", x * 2, y + 1);
                    break;
                case "D":
                    x++;
                    if (player.grounded == false)
                    {
                        WriteAt("██", x * 2, y - 1);
                        WriteAt("██", x * 2, y);
                        x += 1;
                    }
                    break;
            }
            player.Input = null;
            player.x = x;
            player.y = y;

            Console.ForegroundColor = ConsoleColor.White;
            WriteAt("██", x * 2, y - 1);
            WriteAt("██", x * 2, y);
            Console.ForegroundColor = default;
            WriteAt(" ", 110, 0);


        }
        static void Fill_Index(int x, int y, int[,] grid, Block_ids Block)
        {

            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {
                    Console.ForegroundColor = Block.FG;
                    Console.BackgroundColor = Block.BG;
                    grid[j, i] = Block.id;
                    WriteAt(Block.Texture, i * 2, j);
                    

                }
            }
            Console.ForegroundColor = default;
            Console.BackgroundColor = ConsoleColor.Cyan;
        }
        static void Fill_Index_Cord(int x1, int y1,int x2,int y2, int[,] grid, Block_ids Block)
        {

            for (int j = y1; j < y2; j++)
            {
                for (int i = x1; i < x2; i++)
                {
                    Console.ForegroundColor = Block.FG;
                    Console.BackgroundColor = Block.BG;
                    grid[j, i] = Block.id;
                    WriteAt(Block.Texture, i * 2, j);


                }
            }
            Console.ForegroundColor = default;
            Console.BackgroundColor = ConsoleColor.Cyan;
        }

    }
}
