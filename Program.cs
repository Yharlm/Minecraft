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
            Console.WriteLine("Minecraft v0.0.1");
            Console.ForegroundColor = default;

            Game overworld = new Game();
            Player player = new Player();

            string[] Blocks = new string[10];
            {
                //▀▄░█
                Blocks[1] = "▀▀"; // grass
                Blocks[2] = "██"; // dirt
                Blocks[3] = "██"; // stone
                Blocks[4] = "██"; // log
                Blocks[5] = "██"; // leaves
            }
            int[,] grid = new int[player.y_size, player.x_size];
            BuildWorld(grid);
            while (true)
            {
                GetInput(grid, player);


            }

        }

        private static void BuildWorld(int[,] grid)
        {

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
                WriteAt("  ", x * 2, y - 1);
                WriteAt("  ", x * 2, y);
            }





            switch (player.Input)
            {
                case "W":


                    break;
                case "A":

                    x--;

                    break;
                case "S":


                    break;
                case "D":
                    x++;

                    break;
            }
            player.Input = null;
            player.x = x;
            player.y = y;

            Console.ForegroundColor = ConsoleColor.White;
            WriteAt("██", x * 2, y - 1);
            WriteAt("██", x * 2, y);
            Console.ForegroundColor = default;
            WriteAt("", 110, 0);


        }
        static void Fill_Index(int x, int y, int[,] grid, int id,string[] Blocks)
        {

            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {
                    grid[y, x] = id;
                    WriteAt(Blocks[id], x * 2, y);
                }
            }
        }
        
    }
}
