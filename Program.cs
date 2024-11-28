
//using ConsoleNewMinigame;
using Minecraft;
using System;
using System.Numerics;

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
            Console_runE();


            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            WriteAt("Minecraft v0.0.2.5, now with Inventory... sort of", 1, 1);
            Console.ForegroundColor = default;

            Game overworld = new Game();
            Player player = new Player();


            //Block_ids air = new Block_ids(0, "  ", ConsoleColor.DarkGray, ConsoleColor.Cyan);
            //Block_ids Grass = new Block_ids(1, "▀▀", ConsoleColor.DarkGreen, ConsoleColor.DarkYellow);
            //Block_ids dirt = new Block_ids(2, "██", ConsoleColor.DarkYellow, ConsoleColor.DarkYellow);
            //Block_ids stone = new Block_ids(3, "██", ConsoleColor.DarkGray, ConsoleColor.DarkGray);
            //Block_ids wood = new Block_ids(4, "||", ConsoleColor.Yellow, ConsoleColor.DarkYellow);
            //Block_ids water = new Block_ids(5, "██", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);
            //Block_ids waterTop = new Block_ids(6, "▄▄", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);
            //Block_ids leaves = new Block_ids(7, "▄▀", ConsoleColor.DarkGreen, ConsoleColor.Green);
            Solid Default = new Solid("null", 0, null, default, default);

            Default = new Solid("air", 0, "  ", ConsoleColor.DarkGray, ConsoleColor.Cyan); player.Block_list.Add(Default);
            Default = new Solid("Grass", 1, "▀▀", ConsoleColor.DarkGreen, ConsoleColor.DarkYellow); player.Block_list.Add(Default);
            Default = new Solid("Dirt", 2, "██", ConsoleColor.DarkYellow, ConsoleColor.DarkYellow); player.Block_list.Add(Default);
            Default = new Solid("Stone",3, "██", ConsoleColor.DarkGray, ConsoleColor.DarkGray); player.Block_list.Add(Default);
            Default = new Solid("Log",3, "||", ConsoleColor.DarkYellow, ConsoleColor.Yellow); player.Block_list.Add(Default);
            Default = new Solid("water", 5, "██", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue); player.Block_list.Add(Default);
            Default = new Solid("waterTop", 6, "▄▄", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue); player.Block_list.Add(Default);
            Default = new Solid("Leaves", 7, "▄▀", ConsoleColor.DarkGreen, ConsoleColor.Green); player.Block_list.Add(Default);
            //Move this to the block method using switch case or make a new class block


            int[,] grid = new int[player.y_size, player.x_size];
            BuildWorld(grid,player);


            while (true)
            {
                double timer = Math.Ceiling(overworld.time += 0.0002);
                Console.ForegroundColor = ConsoleColor.White;
                WriteAt(timer.ToString(), 0, 2);
                Console.ForegroundColor = ConsoleColor.Cyan;
                GetInput(grid, player);
                BlockUpdate(grid, player);
                cordinates PlayerPos = new cordinates();

                if (grid[player.y + 1, player.x] == 0)
                {
                    Thread.Sleep(100);
                    if (grid[player.y - 2, player.x] == 0)
                    {
                        WriteAt("  ", player.x * 2, player.y - 1);
                    }
                    WriteAt("  ", player.x * 2, player.y - 1);

                    player.y++;
                    player.grounded = false;
                    Console.ForegroundColor = ConsoleColor.White;

                    //WriteAt("  ", player.x * 2, player.y);

                    Console.ForegroundColor = ConsoleColor.Cyan;

                    //Thread.Sleep(100);


                }
                else
                {
                    player.grounded = true;
                }

            }

        }

        private static void Console_runE()
        {
            int[] arr = { 1, 2, 3, 3, 3, 4, 5, 4, 3, 3, 4 };
            int counter = 0;
              
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] == arr[i - 1]) { counter++; }
                else
                {
                    
                   
                }
                Console.WriteLine(counter);
            }
            Console.ReadLine();
            
            
        }

        private static void BlockUpdate(int[,] grid,object plr)
        {
            Player player = (Player)plr;
            Block_ids water = new Block_ids(6, "██", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                int water_level = 0;
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 5 || grid[i, j] == 6 && grid[i + 1, j] == 0)
                    {
                        Fill_block(j, i + 1, grid, player.Block_list[5]);
                    }
                }
            }
        }

        private static void BuildWorld(int[,] grid,object instance)
        {
            Player player = (Player) instance;
            ConsoleColor Default = ConsoleColor.Cyan;
            Block_ids air = new Block_ids(0, "  ", ConsoleColor.DarkGray, ConsoleColor.Cyan);
            Block_ids Grass = new Block_ids(1, "▀▀", ConsoleColor.DarkGreen, ConsoleColor.DarkYellow);
            Block_ids dirt = new Block_ids(2, "██", ConsoleColor.DarkYellow, ConsoleColor.DarkYellow);
            Block_ids stone = new Block_ids(3, "██", ConsoleColor.DarkGray, ConsoleColor.DarkGray);
            Block_ids wood = new Block_ids(4, "||", ConsoleColor.Yellow, ConsoleColor.DarkYellow);
            Block_ids water = new Block_ids(5, "██", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);
            Block_ids waterTop = new Block_ids(6, "▄▄", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);
            Block_ids leaves = new Block_ids(7, "▄▀", ConsoleColor.DarkGreen, ConsoleColor.Green);



            Structure tree = new Structure();
            tree.Struct = new int[,]{
                { 0,0,1,0,0 },
                { 0,0,1,0,0 },
                { 0,0,1,0,0 },
                { 0,0,1,0,0 },
                { 0,0,1,0,0 }
            };
            Structure Leaves = new Structure();
            Leaves.Struct = new int[,]{
                { 0,1,1,1,0 },
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 0,0,0,0,0 },
                { 0,0,0,0,0 }
            };
            Structure House = new Structure();
            House.Struct = new int[,]{
                { 1,1,1,1,1,1,1,1 },
                { 1,0,0,0,0,0,0,1 },
                { 1,0,0,0,0,0,0,1 },
                { 1,0,0,0,0,0,0,1 },
                { 1,0,0,0,0,0,0,1 }
            };


            Fill_Index_Cord(0, 20, 60, 30, grid, dirt);
            Fill_Index_Cord(0, 19, 60, 20, grid, Grass);
            Fill_block(54, 6, grid, player.Block_list[5]);
            structure(tree, 11, grid, wood);
            structure(Leaves, 11, grid, leaves);
            structure(House, 31, grid, stone);
        }
        
        static void structure(object struc, int Local_x, int[,] grid, object Block)
        {
            Structure structure = (Structure)struc;
            Block_ids block = (Block_ids)Block;

            //int[,] str =
            //{
            //    {0,0,1,0,0 },
            //    {0,0,1,0,0 },
            //    {0,1,1,0,0 },
            //    {0,0,1,0,0 },
            //    {0,0,1,0,0 }
            //};

            int x = structure.Struct.GetLength(1);
            int y = structure.Struct.GetLength(0);

            int Local_y = 0;
            while (grid[Local_y, Local_x] == 0)
            {
                Local_y++;
            }
            Local_y -= y;
            for (int i = Local_y; i < Local_y + y; i++)
            {
                for (int j = Local_x; j < Local_x + x; j++)
                {


                    if (structure.Struct[i - Local_y, j - Local_x] == 1)
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


        }

        //static double velocity() 
        private static void GetInput(int[,] grid, object instance)
        {
            
            //Block_ids air = new Block_ids(0, "  ", default, ConsoleColor.Cyan);
            //Block_ids Grass = new Block_ids(1, "▀▀", ConsoleColor.DarkGreen, ConsoleColor.DarkYellow);
            //Block_ids dirt = new Block_ids(2, "██", ConsoleColor.DarkYellow, ConsoleColor.DarkYellow);
            //Block_ids stone = new Block_ids(3, "██", ConsoleColor.DarkGray, ConsoleColor.DarkGray);
            //Block_ids wood = new Block_ids(4, "||", ConsoleColor.Yellow, ConsoleColor.DarkYellow);
            //Block_ids water = new Block_ids(5, "██", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);
            //Block_ids waterTop = new Block_ids(6, "▄▄", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);
            //Block_ids leaves = new Block_ids(7, "▄▀", ConsoleColor.DarkGreen, ConsoleColor.Green);
            

            Player player = (Player)instance;

            Solid air = player.Block_list[0];









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
                //if(grid[player.y - 1, player.x + 1] == 5)
                //{
                //    Fill_block(x, y,grid,water);
                //    Fill_block(x, y-1, grid, water);
                //}
                //else if(grid[player.y - 1, player.x + 1] == 0)
                //{
                //    Fill_block(x, y, grid, air);
                //    Fill_block(x, y - 1, grid, air);
                //}

                WriteAt("  ", x * 2, y - 1);
                WriteAt("  ", x * 2, y);
                WriteAt("  ", 110, 0);
            }
            else { player.Input = null; }

            //player.Selected_block = dirt;
            switch (player.Input)
            {
                case "E":
                    player.hotbar++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    
                            player.Selected_block = player.Block_list[player.hotbar]; WriteAt( player.Selected_block.Name +"    ", 55, 2);
                    Console.ForegroundColor = default;
                    if (player.hotbar == player.Block_list.Count-1) player.hotbar = 0;


                    break;
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
                    if (player.special_key == "W")
                        if (grid[player.y + 1, player.x] == 0)
                        {
                            Fill_block(player.x, player.y + 1, grid, player.Selected_block);
                            player.last_key = null;
                        }
                        else if (player.last_key == "S")
                        {
                            player.last_key = "D";
                        }
                        else if (player.last_key == "D")
                        {
                            if (grid[player.y + 1, player.x + 1] == 0)
                                Fill_block(player.x + 1, player.y + 1, grid, player.Selected_block);
                            else if (grid[player.y, player.x + 1] == 0)
                                Fill_block(player.x + 1, player.y, grid, player.Selected_block);
                            else if (grid[player.y - 1, player.x + 1] == 0)
                                Fill_block(player.x + 1, player.y - 1, grid, player.Selected_block);
                        }
                        else if (player.last_key == "A")
                        {
                            if (grid[player.y + 1, player.x - 1] == 0)
                                Fill_block(player.x - 1, player.y + 1, grid, player.Selected_block);
                            else if (grid[player.y, player.x - 1] == 0)
                                Fill_block(player.x - 1, player.y, grid, player.Selected_block);
                            else if (grid[player.y - 1, player.x - 1] == 0)
                                Fill_block(player.x - 1, player.y - 1, grid, player.Selected_block);
                        }

                    break;
                case "W":
                    if (player.grounded == false)
                    {
                        //if (player.last_key == "D")
                        //{
                        //    x += 2;
                        //}
                        //if (player.last_key == "A")
                        //{
                        //    x -= 2;
                        //}
                    }
                    if (player.grounded == true && grid[player.y - 3, player.x] == 0)
                    {
                        y -= 2;
                        //WriteAt("██", x * 2, y - 1);
                        //WriteAt("██", x * 2, y);
                        //grid[player.y - 1, player.x] = 0;
                        player.grounded = false;
                    }
                    else if (player.grounded == true && grid[player.y - 2, player.x] == 0)
                    {
                        y -= 1;
                        //WriteAt("██", x * 2, y - 1);
                        //WriteAt("██", x * 2, y);
                        //grid[player.y - 1, player.x] = 0;
                        player.grounded = false;
                    }
                    else if (player.grounded == true && grid[player.y - 1, player.x] == 0)
                    {

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
            Console.ForegroundColor = ConsoleColor.Red;
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

        static void Fill_block(int x, int y, int[,] grid, Solid Block)
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
