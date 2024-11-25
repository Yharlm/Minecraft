//using ConsoleNewMinigame;
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
            //Console_runE();


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
            BlockUpdate(grid);

            while (true)
            {
                double timer = Math.Ceiling(overworld.time += 0.0002);
                Console.ForegroundColor = ConsoleColor.White;
                WriteAt(timer.ToString(), 0, 2);
                Console.ForegroundColor = ConsoleColor.Cyan;
                GetInput(grid, player);
                if (grid[player.y + 1, player.x] == 0)
                {
                    Thread.Sleep(100);
                    player.grounded = false;
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteAt("  ", player.x * 2, player.y - 1);
                    //WriteAt("  ", player.x * 2, player.y);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    player.y++;



                }
                else
                {
                    player.grounded = true;
                }

            }

        }

        private static void Console_runE()
        {


            int n = int.Parse(Console.ReadLine());
            double[] p = new double[5];
            int input;
            for (int i = 0; i < n; i++)
            {
                input = int.Parse(Console.ReadLine());

                if (input < 200) p[0]++;
                else if (input <= 399) p[1]++;
                else if (input <= 599) p[2]++;
                else if (input <= 799) p[3]++;
                else if (input >= 800) p[4]++;
            }
            int PNum = 1;
            foreach (double i in p)
            {
                double result = i / n;
                result = result * 100;
                Console.WriteLine("P" + PNum + ":" + result);
                PNum++;
            }
            Console.ReadLine();
        }

        private static void BlockUpdate(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                int water_level = 0;
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    //if (grid[j, i] == 0)  
                }
            }
        }

        private static void BuildWorld(int[,] grid)
        {
            Game game = new Game();
            Block_ids air = new Block_ids(0, "  ", ConsoleColor.DarkGray, ConsoleColor.Cyan);
            Block_ids Grass = new Block_ids(1, "▀▀", ConsoleColor.DarkGreen, ConsoleColor.DarkYellow);
            Block_ids dirt = new Block_ids(2, "██", ConsoleColor.DarkYellow, ConsoleColor.DarkYellow);
            Block_ids stone = new Block_ids(3, "██", ConsoleColor.DarkGray, ConsoleColor.DarkGray);
            Block_ids wood = new Block_ids(4, "██", ConsoleColor.DarkRed, ConsoleColor.DarkRed);
            Block_ids water = new Block_ids(5, "██", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);
            Block_ids waterTop = new Block_ids(6, "▄▄", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);




            Fill_Index_Cord(0, 20, 60, 30, grid, dirt);
            Fill_Index_Cord(0, 19, 60, 20, grid, Grass);
            structure(31, grid, stone);
        }

        static void structure(int Local_x, int[,] grid, object Block)
        {
            Block_ids block = (Block_ids)Block;
            //Structure tree = new Structure();

            int[,] str =
            {
                {1,1,1,1 },
                {1,1,1,1 },
                {1,1,1,1 },
                {1,1,1,1 }
            };
            int x = str.GetLength(1);
            int y = str.GetLength(0);

            int Local_y = 0;
            while (grid[Local_y, Local_x] == 0)
            {
                Local_y++;
            }
            for (int i = Local_y; i < Local_y + y; i++)
            {
                for (int j = Local_x; j < Local_x + x; j++)
                {
                    Console.ForegroundColor = block.FG;
                    Console.BackgroundColor = block.BG;
                    grid[i, j] = block.id;
                    WriteAt(block.Texture, j * 2, i);
                    Console.ForegroundColor = default;
                    Console.BackgroundColor = ConsoleColor.Cyan;

                }
            }


        }


        //static double velocity() 
        private static void GetInput(int[,] grid, object instance)
        {
            Game game = new Game();
            Block_ids air = new Block_ids(0, "  ", ConsoleColor.DarkGray, ConsoleColor.Cyan);
            Block_ids Grass = new Block_ids(1, "▀▀", ConsoleColor.DarkGreen, ConsoleColor.DarkYellow);
            Block_ids dirt = new Block_ids(2, "██", ConsoleColor.DarkYellow, ConsoleColor.DarkYellow);
            Block_ids stone = new Block_ids(3, "██", ConsoleColor.DarkGray, ConsoleColor.DarkGray);
            Block_ids wood = new Block_ids(4, "██", ConsoleColor.DarkRed, ConsoleColor.DarkRed);
            Block_ids water = new Block_ids(5, "██", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);
            Block_ids waterTop = new Block_ids(6, "▄▄", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);
            Player player = (Player)instance;
            grid[player.y, player.x] = 0;
            int x = player.x;
            int y = player.y;
            if (Console.KeyAvailable == true)
            {

                player.Input = Console.ReadKey().Key.ToString();
                if (player.Input != "W" && player.Input != "L" && player.Input != "K")
                {
                    player.last_key = player.Input;

                }
                if (player.Input == "W")
                {
                    player.special_key = "W";
                }

                WriteAt("  ", x * 2, y - 1);
                WriteAt("  ", x * 2, y);
                WriteAt("  ", 110, 0);
            }
            else { player.Input = null; }

            switch (player.Input)
            {
                case "K":
                    if (player.special_key == "W")
                    {
                        if (player.special_key == "W")
                        {
                            Fill_block(player.x, player.y - 2, grid, air); player.special_key = null;
                        }
                        else if (player.last_key == "D")
                        {
                            Fill_block(player.x + 1, player.y - 2, grid, air);
                        }
                        else if (player.last_key == "A")
                        {
                            Fill_block(player.x - 1, player.y - 2, grid, air);
                        }
                    }

                    else if (player.last_key == "D")
                    {
                        if (grid[player.y - 1, player.x + 1] != 0)
                            Fill_block(player.x + 1, player.y - 1, grid, air);
                        else if (grid[player.y, player.x + 1] != 0)
                            Fill_block(player.x + 1, player.y, grid, air);
                        else if (grid[player.y - 2, player.x + 1] != 0)
                            Fill_block(player.x + 1, player.y - 2, grid, air);
                    }
                    else if (player.last_key == "A")
                    {
                        if (grid[player.y - 1, player.x - 1] != 0)
                            Fill_block(player.x - 1, player.y - 1, grid, air);
                        else if (grid[player.y, player.x - 1] != 0)
                            Fill_block(player.x - 1, player.y, grid, air);
                        else if (grid[player.y - 2, player.x - 1] != 0)
                            Fill_block(player.x - 1, player.y - 2, grid, air);
                    }
                    else if (player.last_key == "S")
                    {
                        Fill_block(player.x, player.y + 1, grid, air);
                    }

                    break;
                case "L":
                    if (player.last_key == "D")
                    {
                        Fill_block(player.x + 1, player.y, grid, stone);
                    }
                    if (player.last_key == "A")
                    {
                        Fill_block(player.x - 1, player.y, grid, stone);
                    }
                    break;
                case "W":
                    if (player.grounded == false)
                    {
                        if (player.last_key == "D")
                        {
                            x += 2;
                        }
                        if (player.last_key == "A")
                        {
                            x -= 2;
                        }
                    }
                    if (player.grounded == true /*&& grid[player.y - 3, player.x] == 0*/)
                    {
                        if (grid[player.y - 2, player.x] == 0)
                        { y -= 1; }
                        else if (grid[player.y - 3, player.x] == 0)
                        { y -= 2; }
                        player.grounded = false;
                    }



                    break;
                case "A":


                    if (player.grounded == false)
                    {
                        WriteAt("██", x * 2, y - 1);
                        WriteAt("██", x * 2, y);

                    }

                    //grid[player.y , player.x - 1] = 0;
                    //WriteAt("  ", (x-1) * 2, y );
                    if (grid[player.y, player.x - 1] == 0 && grid[player.y - 1, player.x - 1] == 0)
                    {

                        x--;
                    }

                    break;
                case "S":

                    if (grid[player.y + 1, player.x] == 0)
                    {

                        y++;
                    }
                    break;
                case "D":

                    if (player.grounded == false)
                    {
                        WriteAt("██", x * 2, y - 1);
                        WriteAt("██", x * 2, y);

                    }
                    if (grid[player.y, player.x + 1] == 0 && grid[player.y - 1, player.x + 1] == 0)
                    {

                        x++;
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
        static void Fill_Index_Cord(int x1, int y1, int x2, int y2, int[,] grid, Block_ids Block)
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

        static void Fill_block(int x, int y, int[,] grid, Block_ids Block)
        {
            Console.ForegroundColor = Block.FG;
            Console.BackgroundColor = Block.BG;
            grid[y, x] = Block.id;
            WriteAt(Block.Texture, x * 2, y);
            Console.ForegroundColor = default;
            Console.BackgroundColor = ConsoleColor.Cyan;
        }

    }
}
