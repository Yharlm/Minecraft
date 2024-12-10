//using ConsoleNewMinigame;
using System;
using System.Net.Security;
using System.Xml.XPath;

namespace Minecraft
{
    class Program
    {

        static void Entity_behaviour(Game game, Player player, int[,] grid)
        {
            try
            {
                foreach (Entity mob in game.Existing_Entities)
                {
                    string behaviour = mob.Type;
                    switch (behaviour)
                    {
                        case "Pig":

                            //try { Walk_to_player(mob, player, grid, game); }
                            //catch { }
                            if (mob.Health <= 0) { Kill_entity(game, mob); }
                            Walk_to_player(mob, player, grid, game);
                            if (GetRadius_forplayer(player, mob.cordinates, 2, 2) && game.curent_tick)
                            {
                                Explosion(game, grid, mob.cordinates, player);
                            }
                            if (grid[mob.cordinates.y + 1, mob.cordinates.x] != 0)
                            {
                                if (grid[mob.cordinates.y, mob.cordinates.x + 1] != 0 || grid[mob.cordinates.y, mob.cordinates.x - 1] != 0)
                                {

                                    WriteAt("  ", mob.cordinates.x * 2, mob.cordinates.y);
                                    mob.cordinates.y -= 2;
                                }
                            }
                            break;


                    }
                }
            }
            catch { }
        }

        private static void Kill_entity(Game game, Entity entity)
        {

            WriteAt("  ", entity.cordinates.x * 2, entity.cordinates.y);
            game.Existing_Entities.Remove(entity);
        }

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


            while (true)
            {

                Console.CursorVisible = false;
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                WriteAt("Minecraft v0.0.3, Now with world generation!", 1, 1);
                Console.ForegroundColor = default;

                Game overworld = new Game();
                Player player = new Player();


                Solid Default = new Solid("null", 0, null, default, default);
                Non_solid Background = new Non_solid("", 0, null, default, default);

                Default = new Solid("air", 0, "  ", ConsoleColor.DarkGray, ConsoleColor.Cyan); player.Block_list.Add(Default);
                Default = new Solid("Grass", 1, "▀▀", ConsoleColor.DarkGreen, ConsoleColor.DarkYellow); player.Block_list.Add(Default);
                Default = new Solid("Dirt", 2, "██", ConsoleColor.DarkYellow, ConsoleColor.DarkYellow); player.Block_list.Add(Default);
                Default = new Solid("Stone", 3, "██", ConsoleColor.DarkGray, ConsoleColor.DarkGray); player.Block_list.Add(Default);
                Default = new Solid("Log", 4, "||", ConsoleColor.Yellow, ConsoleColor.DarkYellow); player.Block_list.Add(Default);
                Background = new Non_solid("water", 5, "  ", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue); player.Block_Back_list.Add(Background);
                Default = new Solid("water", 5, "  ", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue); player.Block_Back_list.Add(Background);
                //Default = new Solid("water", 5, "  ", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue); player.Block_list.Add(Default);

                Default = new Solid("waterTop", 6, "▄▄", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue); player.Block_list.Add(Default);
                Default = new Solid("Leaves", 7, "▄▀", ConsoleColor.DarkGreen, ConsoleColor.Green); player.Block_list.Add(Default);
                Default = new Solid("Coal_ore", 8, "▄▀", ConsoleColor.DarkGray, ConsoleColor.Black); player.Block_list.Add(Default);
                Default = new Solid("Iron_ore", 9, "▄▀", ConsoleColor.DarkGray, ConsoleColor.Magenta); player.Block_list.Add(Default);
                Default = new Solid("Crafting_table", 10, "TT", ConsoleColor.Yellow, ConsoleColor.DarkYellow); player.Block_list.Add(Default);


                //Sprite magicMissile = new Sprite();

                //magicMissile.Sprites[0, 0] = "▀ ▄       "; 
                //magicMissile.Sprites[0, 1] = "  ▀ ▄     ";
                //magicMissile.Sprites[0, 2] = "    █▄    ";
                //magicMissile.Sprites[0, 3] = "    ▀     ";
                //magicMissile.Sprites[0, 4] = "          ";

                //string[,] Sprites =
                //    {
                //{
                //    "▀ ▄       ",
                //    "  ▀ ▄     ",
                //    "    █▄    ",
                //    "    ▀     ",
                //    "          "
                //}
                //    };




















                Entity Mob = new Entity(null, 0, null, "EE"); overworld.Entity_list.Add(Mob);

                Mob = new Entity("pig", 10, "Pig", "██"); overworld.Entity_list.Add(Mob);


                Mob = new Entity("TNT", 0, null, "██"); overworld.Entity_list.Add(Mob);

                //Entity pig = new Entity("pig", 10, null); Mob pig.gravity(grid);







                //Move this to the block method using switch case or make a new class block


                int[,] grid = new int[100, 200];
                BuildWorld(grid, player, overworld);
                double tick = 0.05;

                while (player.health > 0)
                {

                    //double timer = Math.Ceiling(overworld.time += 0.0002);
                    double timer = overworld.time += 0.0002;
                    if (overworld.time >= tick)
                    {

                        overworld.curent_tick = true;
                        overworld.time = 0;

                    }
                    else
                    {

                        overworld.curent_tick = false;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteAt(timer.ToString(), 0, 2);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    GetInput(grid, player, overworld);
                    Entity_update(grid, overworld.Existing_Entities, overworld, player);

                    BlockUpdate(grid, player, overworld);
                    PrintUI(player);

                    Cordinates PlayerPos = new Cordinates();

                    if (grid[player.y + 1, player.x] == 0 && overworld.curent_tick)
                    {

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

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                WriteAt("YOU ARE DEAD..", 50, 14);
                Thread.Sleep(1000);
                Console.WriteLine("  Idiot....");
                Thread.Sleep(6000);
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = default;

            }
        }

        private static void PrintUI(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteAt(player.health.ToString() + " Health", 3, 4);
            Console.ForegroundColor = default;
        }

        private static void Console_runE()
        {
            ////insialise the random class and tell the player what to do
            //Console.WriteLine("random Number Between 1 and 100 has been generated: try to guess it in 7 attempts");
            //Random random = new Random();
            ////generate number and initalise attempts
            //int num = random.Next(1, 100);
            //int attempts = 7;
            //int input = 0;
            ////a loop that ends once the player has 0 lives
            //while (attempts != 0)
            //{
            //    //gets player input
            //    try { input = int.Parse(Console.ReadLine()); }
            //    catch
            //    {
            //        Console.WriteLine("do i have to mention its also NOT a letter?");
            //        Console.Beep();
            //    }
            //    //a few "if" statemtents for the posibilities
            //    //wrong answers give a hint and print out how many lives you have left
            //    if (input > num) { Console.WriteLine("lower (: lives left:" + attempts); attempts--; }
            //    if (input < num) { Console.WriteLine("higher lives left:" + attempts); attempts--; }
            //    if (input == num)
            //    {
            //        Console.Clear();
            //        //the very enthusiastic Win screen
            //        Console.Beep(); Console.WriteLine("fine you win..");
            //        Thread.Sleep(4000);
            //        Environment.Exit(0);
            //    }
            //}
            //Console.Clear();
            //Console.WriteLine("you lost");
            //Console.Beep(); Thread.Sleep(2000); Console.Clear();
            //Console.WriteLine("im not surprised");
            //Console.Beep(); Thread.Sleep(2000); Console.Clear();
            //Environment.Exit(0);

            //Console.WriteLine(Console.ReadKey().Key.ToString());  █▀▄ 
            Console.ForegroundColor = ConsoleColor.Red;
            Thread.Sleep(10);
            WriteAt("▀ ▄       ", 0, 0);
            WriteAt("  ▀ ▄     ", 0, 1);
            WriteAt("    █▄    ", 0, 2);
            WriteAt("    ▀     ", 0, 3);
            WriteAt("          ", 0, 4);
            Thread.Sleep(40);
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteAt("   ▄      ", 0, 0);
            WriteAt("    ▀▄    ", 0, 1);
            WriteAt("     █▄   ", 0, 2);
            WriteAt("   ▄██▀   ", 0, 3);
            WriteAt("▄██▀▀     ", 0, 4);
            Thread.Sleep(50);
            Console.ForegroundColor = ConsoleColor.White;
            WriteAt("          ", 0, 0);
            WriteAt("     ▄    ", 0, 1);
            WriteAt("      ▄   ", 0, 2);
            WriteAt("     █▀   ", 0, 3);
            WriteAt("▄ ▄█▀▀    ", 0, 4);
            Thread.Sleep(100);
            WriteAt("          ", 0, 0);
            WriteAt("          ", 0, 1);
            WriteAt("          ", 0, 2);
            WriteAt("          ", 0, 3);
            WriteAt("          ", 0, 4);
            Console.ReadLine();
            Console.ForegroundColor = default;



        }

        private static void BlockUpdate(int[,] grid, object plr, object instance)
        {

            //each frame one plock gets updated only, fix this by updating every block at once per frame idk
            Game game = (Game)instance;
            Player player = (Player)plr;
            bool water_level = true;
            Block_ids water = new Block_ids(6, "██", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);


            for (int i = 0; i < grid.GetLength(0); i++)
            {

                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 5 || grid[i, j] == 6 && grid[i + 1, j] == 0)
                    {

                        Fill_block(j, i + 1, grid, player.Block_list[5]);
                        game.curent_tick = false;
                    }
                }
            }

        }

        private static void BuildWorld(int[,] grid, object instance, Game game)
        {
            Random random = new Random();
            Player player = (Player)instance;
            ConsoleColor Default = ConsoleColor.Cyan;


            Structure tree = new Structure();
            tree.Struct = new int[,]{
                { 0,7,7,7,0 },
                { 0,7,7,7,0 },
                { 7,7,7,7,7 },
                { 7,7,7,7,7 },
                { 0,0,4,0,0 },
                { 0,0,4,0,0 }
            };

            Structure House = new Structure();
            House.Struct = new int[,]{
                { 4,4,4,4,4,4,4,4 },
                { 4,0,0,0,0,0,0,4 },
                { 0,0,0,0,0,0,0,4 },
                { 0,0,0,0,0,0,0,4 },
                { 3,3,3,3,3,3,3,3 }
            };



            GenerateWorld(game, grid, player);

            int tree_rate = 24
           ;
            int Tree_r = 0;
            for (int i = 0; i < 100; i++)
            {
                Tree_r = random.Next(1, tree_rate);
                if (Tree_r >= tree_rate - 2)
                {
                    structure(tree, i, grid, player);
                    i += 5;
                }
            }


            //structure(House, 31, grid, player);
        }

        private static void GenerateWorld(Game game, int[,] grid, Player player)
        {
            Random random = new Random();


            int flatness = random.Next(3, 17)
            ;

            int min = 17
            ; int max = min + 3
            ; int Height_min = 4
            ; int underground_level = 30;
            int num = 0;
            int curf = 0;
            for (int j = 0; j < 100;)
            {
                num = random.Next(min, max);
                curf = random.Next(1, flatness) + j;
                while (curf > j)
                {

                    Fill_block(j, num, grid, player.GetBlock("Grass"));

                    int counter = 1;
                    while (counter < 5)
                    {
                        Fill_block(j, num + counter, grid, player.GetBlock("Dirt"));
                        counter++;
                    }
                    while (counter < underground_level)
                    {
                        Fill_block(j, num + counter, grid, player.GetBlock("Stone"));
                        counter++;
                    }

                    j++;
                }


            }
            for (int j = 0; j < 100;)
            {
                int coalN = random.Next(14, 24);
                int vein = random.Next(1, 6);

                if (random.Next(1, 30) < 4)
                {
                    Fill_Index_Cord2(j, num + coalN - vein, j + vein, num + coalN, grid, player.Block_list[7], 5);

                }
                int vein2 = random.Next(1, 4);
                if (random.Next(1, 90) < 4)
                {
                    Fill_Index_Cord2(j, num + coalN - vein2, j + vein2, num + coalN, grid, player.Block_list[8], 5);

                }
                j++;
            }
        }



        static void structure(object struc, int Local_x, int[,] grid, Player game)
        {
            Structure structure = (Structure)struc;
            //Block_ids block = (Block_ids)Block;
            //Solid tile = (Solid)Block;
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
                    if (structure.Struct[i - Local_y, j - Local_x] == 0)
                    {
                        continue;
                    }
                    int ID = structure.Struct[i - Local_y, j - Local_x];


                    Solid block = game.Block_list.Find(x => x.id == structure.Struct[i - Local_y, j - Local_x]);
                    Fill_block(j, i, grid, block);

                    //Console.ForegroundColor = block.FG;
                    //Console.BackgroundColor = block.BG;
                    //grid[i, j] = block.id;
                    //WriteAt(block.Texture, j * 2, i);
                    //Console.ForegroundColor = default;
                    //Console.BackgroundColor = ConsoleColor.Cyan;


                    //grid[i, j] = structure.Struct[i - Local_y, j - Local_x];
                    //WriteAt(game.Block_list(1).t, j * 2, i);
                    //Console.ForegroundColor = default;
                    //Console.BackgroundColor = ConsoleColor.Cyan;

                }
            }


        }


        static bool GetRadius(Entity mob1, Player plr, int x, int y)
        {
            bool res = false;
            if (mob1.cordinates.x > plr.x - x && mob1.cordinates.x < plr.x + x)
            {
                res = true;
            }
            if (mob1.cordinates.y > plr.y - y && mob1.cordinates.y < plr.y + y)
            {
                res = true;
            }

            return res;
        }
        static bool GetRadius_forplayer(Cordinates object1, Cordinates object2, int x, int y)
        {
            bool res = false;
            if (object1.x > object2.x - x && object1.x < object2.x + x && object1.y > object2.y - y && object1.y < object2.y + y)
            {
                res = true;
            }


            return res;
        }

        private static Cordinates Convert_cor(int x, int y)
        {
            Cordinates cords = new Cordinates();
            cords.y = y;
            cords.x = x;
            return cords;
        }
        private static void GetInput(int[,] grid, object instance, Game game)
        {

            //Block_ids air = new Block_ids(0, "  ", default, ConsoleColor.Cyan);
            //Block_ids Grass = new Block_ids(1, "▀▀", ConsoleColor.DarkGreen, ConsoleColor.DarkYellow);
            //Block_ids dirt = new Block_ids(2, "██", ConsoleColor.DarkYellow, ConsoleColor.DarkYellow);
            //Block_ids stone = new Block_ids(3, "██", ConsoleColor.DarkGray, ConsoleColor.DarkGray);
            //Block_ids wood = new Block_ids(4, "||", ConsoleColor.Yellow, ConsoleColor.DarkYellow);
            //Block_ids water = new Block_ids(5, "██", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);
            //Block_ids waterTop = new Block_ids(6, "▄▄", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue);
            //Block_ids leaves = new Block_ids(7, "▄▀", ConsoleColor.DarkGreen, ConsoleColor.Green);
            Random random = new Random();

            Player player = (Player)instance;

            Solid air = player.Block_list[0];



            grid[player.y, player.x] = 0;
            int x = player.x;
            int y = player.y;

            if (Console.KeyAvailable == true)
            {

                player.Input = Console.ReadKey().Key.ToString();
                if (player.Input != "Spacebar" && player.Input != "L" && player.Input != "K" && player.Input != "R")
                {
                    player.last_key = player.Input;

                }
                if (player.Input == "Spacebar")
                {
                    player.special_key = "Spacebar";
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
            Cordinates player_cords = new Cordinates();
            player_cords.x = x;
            player_cords.y = y;
            //player.Selected_block = dirt;
            switch (player.Input)
            {


                case "R":
                    Attack(game, player, grid, 4, 5, 2);
                    Slash(player, game, grid);


                    break;
                case "N":

                    try
                    {

                        foreach (Entity entity in game.Existing_Entities)
                        {


                            WriteAt("  ", entity.cordinates.x * 2, entity.cordinates.y);
                            Cordinates cordinates = entity.cordinates;
                            game.Existing_Entities.Remove(entity);
                            Explosion(game, grid, cordinates, player);

                        }
                    }
                    catch { }

                    break;
                case "Q":
                    

                    
                    

                    break;
                case "Y":
                    foreach (Entity ent in game.Existing_Entities)
                    {
                        WriteAt("  ", ent.cordinates.x * 2, ent.cordinates.y);
                    }
                    game.Existing_Entities.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    WriteAt(game.Existing_Entities.Count().ToString() + "  ", 24, 3);
                    Console.ForegroundColor = default;
                    break;
                case "T":
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        WriteAt(game.Existing_Entities.Count().ToString(), 24, 3);
                        Console.ForegroundColor = default;

                        Entity mob = game.Entity_list[1];
                        Entity Default = new Entity(mob.Name, mob.Health, mob.Type, mob.Sprite);
                        //Default.cordinates.x = random.Next(4, 55);
                        Default.cordinates.x = player.x
                        ; Default.cordinates.y = player.y - 10;



                        game.Existing_Entities.Add(Default);
                        //game.Spawn_entity(entity);

                        break;
                    }
                case "E":
                    player.hotbar++;
                    player.Selected_block = player.Block_list[player.hotbar];

                    Console.ForegroundColor = ConsoleColor.Red;
                    Print_window(player);
                    
                    
                    if (player.hotbar == player.Block_list.Count - 1) player.hotbar = 0;
                    

                    break;
                case "K":

                    {

                        if (player.special_key == "Spacebar")
                        {
                            if (player.special_key == "Spacebar")
                            {
                                Break_block(player.x, player.y - 2, grid, air, player); player.special_key = null;

                            }
                            else if (player.last_key == "D")
                            {
                                Break_block(player.x + 1, player.y - 2, grid, air, player);
                            }
                            else if (player.last_key == "A")
                            {
                                Break_block(player.x - 1, player.y - 2, grid, air, player);
                            }
                        }

                        else if (player.last_key == "D")
                        {
                            if (grid[player.y - 1, player.x + 1] != 0)
                                Break_block(player.x + 1, player.y - 1, grid, air, player);
                            else if (grid[player.y, player.x + 1] != 0)
                                Break_block(player.x + 1, player.y, grid, air, player);
                            else if (grid[player.y - 2, player.x + 1] != 0)
                                Break_block(player.x + 1, player.y - 2, grid, air, player);
                        }
                        else if (player.last_key == "A")
                        {
                            if (grid[player.y - 1, player.x - 1] != 0)
                                Break_block(player.x - 1, player.y - 1, grid, air, player);
                            else if (grid[player.y, player.x - 1] != 0)
                                Break_block(player.x - 1, player.y, grid, air, player);
                            else if (grid[player.y - 2, player.x - 1] != 0)
                                Break_block(player.x - 1, player.y - 2, grid, air, player);
                        }
                        else if (player.last_key == "S")
                        {
                            Break_block(player.x, player.y + 1, grid, air,player);
                        }
                    }

                    Print_window(player);

                    break;
                case "L":
                    if (player.Selected_block.quantity > 0)
                    {
                        if (player.special_key == "Spacebar")
                            if (grid[player.y + 1, player.x] == 0 && grid[player.y + 2, player.x] != 0)
                            {
                                Fill_block(player.x, player.y + 1, grid, player.Selected_block);
                                player.last_key = null;
                                player.Selected_block.quantity--;
                            }
                            else if (player.last_key == "S")
                            {
                                player.last_key = "D"; player.Selected_block.quantity--;
                            }
                            else if (player.last_key == "D")
                            {
                                if (grid[player.y + 1, player.x + 1] == 0)
                                    Fill_block(player.x + 1, player.y + 1, grid, player.Selected_block);
                                else if (grid[player.y, player.x + 1] == 0)
                                    Fill_block(player.x + 1, player.y, grid, player.Selected_block);
                                else if (grid[player.y - 1, player.x + 1] == 0)
                                    Fill_block(player.x + 1, player.y - 1, grid, player.Selected_block);
                                player.Selected_block.quantity--;
                            }
                            else if (player.last_key == "A")
                            {
                                if (grid[player.y + 1, player.x - 1] == 0)
                                    Fill_block(player.x - 1, player.y + 1, grid, player.Selected_block);
                                else if (grid[player.y, player.x - 1] == 0)
                                    Fill_block(player.x - 1, player.y, grid, player.Selected_block);
                                else if (grid[player.y - 1, player.x - 1] == 0)
                                    Fill_block(player.x - 1, player.y - 1, grid, player.Selected_block);
                                player.Selected_block.quantity--;
                            }
                        Print_window(player);
                    }
                    break;
                case "Spacebar":
                    if (grid[player.y + 1, player.x] != 0)
                    {

                        if (grid[player.y - 2, player.x] == 0)
                        {
                            y -= 1;
                        }
                        if (grid[player.y - 3, player.x] == 0 && grid[player.y - 2, player.x] == 0)
                        {
                            y -= 1;
                        }
                        if (grid[player.y - 3, player.x] == 0 && grid[player.y - 2, player.x] == 0 && grid[player.y - 4, player.x] == 0)
                        {
                            y -= 1;
                        }


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
                    if (grid[player.y - 1, player.x - 1] == 6)
                    {
                        x--; player.is_swiming = true;
                    }
                    else
                    {
                        player.is_swiming = false;
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
                    if (grid[player.y - 1, player.x + 1] == 6)
                    {
                        x++; player.is_swiming = true;
                    }
                    else
                    {
                        player.is_swiming = false;
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
        static void Fill_Index_Cord(int x1, int y1, int x2, int y2, int[,] grid, Solid Block)
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

        static void Print_Index_Cord(int x1, int y1, int x2, int y2, string text, ConsoleColor FG, ConsoleColor BG)
        {

            for (int j = y1; j < y2; j++)
            {
                for (int i = x1; i < x2; i++)
                {
                    Console.ForegroundColor = FG;
                    Console.BackgroundColor = BG;

                    WriteAt(text, i * 2, j);


                }
            }
            Console.ForegroundColor = default;
            Console.BackgroundColor = ConsoleColor.Cyan;
        }
        static void Fill_Index_Cord2(int x1, int y1, int x2, int y2, int[,] grid, Solid Block, int randomiser)
        {
            Random random = new Random();
            for (int j = y1; j < y2; j++)
            {
                for (int i = x1; i < x2; i++)
                {
                    int e = random.Next(0, randomiser);
                    if (e == 0)
                    {
                        continue;
                    }
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

        static void Break_block(int x, int y, int[,] grid, Solid Block,Player player)
        {

            Console.ForegroundColor = Block.FG;
            Console.BackgroundColor = Block.BG;
            player.Block_list.Find(i => i.id == grid[y, x]).quantity++ ;
            grid[y, x] = Block.id;
            WriteAt(Block.Texture, x * 2, y);
            Console.ForegroundColor = default;
            Console.BackgroundColor = ConsoleColor.Cyan;
        }

        static void Entity_update(int[,] grid, List<Entity> Entity_list, Game game, Player player)
        {
            Entity_behaviour(game, player, grid);

            PlayerAbilities(grid, player, game);
            if (game.Existing_Entities.Count != 0)
            {
                foreach (Entity entity in game.Existing_Entities)
                {
                    entity.gravity(grid, game.curent_tick);

                    //try { Walk_to_player(entity, player, grid, game); }
                    //catch { }
                }

            }
        }

        private static void PlayerAbilities(int[,] grid, Player player, Game game)
        {
            Entity entity = player.held;
            if (player.Holding == true && player.held == null)
            {

                try
                {
                    foreach (Entity ent in game.Existing_Entities)
                    {
                        if (GetRadius(ent, player, 1, 0))
                        {
                            player.held = ent;

                        }
                    }
                }
                catch { }
            }
            else if (player.held != null)
            {
                //WriteAt("  ", entity.cordinates.x * 2, entity.cordinates.y - 1);
                WriteAt("  ", entity.cordinates.x * 2, entity.cordinates.y);
                entity.cordinates.y = player.y - 2;
                entity.cordinates.x = player.x + 0;
            }


        }
        private static Block_ids ConvertToVar(Solid s)
        {
            Block_ids item = new Block_ids(s.id, s.Texture, s.FG, s.BG);
            return item;
        }

        private static Solid block(string name, Player player)
        {
            Solid item = player.Block_list.Find(x => x.Name == name);
            return item;
        }
        static void Explosion(Game game, int[,] grid, Cordinates pos, Player player)
        {


            Solid air = new Solid("air", 0, "  ", ConsoleColor.White, ConsoleColor.Cyan);


            int range = 4;
            int range_max = 9;
            int x = pos.x;
            int y = pos.y;

            Cordinates cordinates = new Cordinates();
            cordinates.x = range_max;
            cordinates.y = range_max;
            cordinates.x1 = x + range_max + 1;
            cordinates.y1 = y + range_max + 1;

            Fill_Index_Cord2(x - range, y - range, x + range + 1, y + range + 1, grid, air, 30);
            Fill_Index_Cord2(x - range_max, y - range_max - range, x + range_max + 1, y + range_max + 1 - range, grid, air, 2);
            //Refresh_area_not(game, player, grid, cordinates, pos);

            if (GetRadius_forplayer(pos, Convert_cor(player.x, player.y), range_max, range_max)) { player.health -= 50; }
            if (GetRadius_forplayer(pos, Convert_cor(player.x, player.y), range, range)) { player.health -= 50; }

        }

        static void Walk_to_player(Entity entity, Player player, int[,] grid, Game game)
        {

            int speed = 1;
            
            if (player.x < entity.cordinates.x)
            {

                if (grid[entity.cordinates.y, entity.cordinates.x - 1] == 0 && game.delay(speed))
                {
                    WriteAt("  ", entity.cordinates.x * 2, entity.cordinates.y);
                    entity.cordinates.x--;
                }

                

            }
            else if (player.x > entity.cordinates.x)
            {

                if (grid[entity.cordinates.y, entity.cordinates.x + 1] == 0 && game.delay(speed))
                {
                    WriteAt("  ", entity.cordinates.x * 2, entity.cordinates.y);
                    entity.cordinates.x++;
                }

            }
            

        }

        private static bool Attack(Game game, Player player, int[,] grid, int knockback, int range, int dmg)
        {
            bool is_there = false;
            foreach (Entity entity in game.Existing_Entities)
            {
                if (GetRadius(entity, player, 5, 3) && entity.cordinates.x > player.x)
                {
                    WriteAt("  ", entity.cordinates.x * 2, entity.cordinates.y);
                    entity.Health -= dmg;
                    if (grid[entity.cordinates.y, entity.cordinates.x + range] == 0) { entity.cordinates.x += knockback; }
                    is_there = true;
                    entity.cordinates.y -= knockback;
                    entity.velocity += 1;
                }
                else if (GetRadius(entity, player, 5, 3) && entity.cordinates.x < player.x)
                {
                    WriteAt("  ", entity.cordinates.x * 2, entity.cordinates.y);
                    entity.Health -= dmg;
                    if (grid[entity.cordinates.y, entity.cordinates.x - range] == 0) { entity.cordinates.x -= knockback; }
                    is_there = true;
                    entity.cordinates.y -= knockback;
                    entity.velocity += -1;
                }


            }
            return is_there;
        }

        static void Slash(Player player, Game game, int[,] grid)
        {
            int x = 0;
            int y = player.y - 4;
            if (player.last_key == "D")
            {
                x = (player.x + 2) * 2;
                Cordinates cordinates = new Cordinates();
                cordinates.x = 5;
                cordinates.y = 5;
                cordinates.x1 = -2;
                cordinates.y1 = 4;
                Console.ForegroundColor = ConsoleColor.Red;

                Thread.Sleep(20);
                WriteAt("▀ ▄       ", x, y + 0);
                WriteAt("  ▀ ▄     ", x, y + 1);
                WriteAt("    █▄    ", x, y + 2);
                WriteAt("    ▀     ", x, y + 3);
                WriteAt("          ", x, y + 4);
                Thread.Sleep(20);
                Console.ForegroundColor = ConsoleColor.Yellow;
                WriteAt("   ▄      ", x, y + 0);
                WriteAt("    ▀▄    ", x, y + 1);
                WriteAt("     █▄   ", x, y + 2);
                WriteAt("   ▄██▀   ", x, y + 3);
                WriteAt("▄██▀▀     ", x, y + 4);
                Thread.Sleep(40);
                Console.ForegroundColor = ConsoleColor.White
                    ;
                WriteAt("          ", x, y + 0);
                WriteAt("     ▄    ", x, y + 1);
                WriteAt("      ▄   ", x, y + 2);
                WriteAt("     █▀   ", x, y + 3);
                WriteAt("▄ ▄█▀▀    ", x, y + 4);
                Thread.Sleep(10);
                WriteAt("          ", x, y + 0);
                WriteAt("          ", x, y + 1);
                WriteAt("          ", x, y + 2);
                WriteAt("          ", x, y + 3);
                WriteAt("          ", x, y + 4);
                Refresh_area(game, player, grid, cordinates);
                Console.ForegroundColor = default;
            }
            else if (player.last_key == "A")
            {
                Cordinates cordinates = new Cordinates();
                cordinates.x = 5;
                cordinates.y = 5;
                cordinates.x1 = 6;
                cordinates.y1 = 4;
                x = (player.x - 6
                    ) * 2;
                Console.ForegroundColor = ConsoleColor.Red;
                Thread.Sleep(20);
                WriteAt("       ▄ ▀", x, y + 0);
                WriteAt("     ▄ ▀  ", x, y + 1);
                WriteAt("    ▄█    ", x, y + 2);
                WriteAt("     ▀    ", x, y + 3);
                WriteAt("          ", x, y + 4);
                Thread.Sleep(20);
                Console.ForegroundColor = ConsoleColor.Yellow;
                WriteAt("      ▄   ", x, y + 0);
                WriteAt("    ▄▀    ", x, y + 1);
                WriteAt("   ▄█     ", x, y + 2);
                WriteAt("   ▀██▄   ", x, y + 3);
                WriteAt("     ▀▀██▄", x, y + 4);
                Thread.Sleep(40);
                Console.ForegroundColor = ConsoleColor.White;
                WriteAt("          ", x, y + 0);
                WriteAt("    ▄     ", x, y + 1);
                WriteAt("   ▄      ", x, y + 2);
                WriteAt("   ▀█     ", x, y + 3);
                WriteAt("    ▀▀█▄ ▄", x, y + 4);
                Thread.Sleep(10);
                WriteAt("          ", x, y + 0);
                WriteAt("          ", x, y + 1);
                WriteAt("          ", x, y + 2);
                WriteAt("          ", x, y + 3);
                WriteAt("          ", x, y + 4);
                Refresh_area(game, player, grid, cordinates);
            }

        }

        static void Refresh_area(Game game, Player player, int[,] grid, Cordinates cords)
        {
            int x = player.x;
            int y = player.y;
            int[,] local = new int[cords.y, cords.x];
            for (int i = 0; i < local.GetLength(0); i++)
            {
                for (int j = 0; j < local.GetLength(1); j++)
                {
                    int Id = grid[i + y - cords.y1, j + x - cords.x1];
                    var selected = player.Block_list.Find(x => x.id == Id);

                    Console.BackgroundColor = selected.BG;
                    Console.ForegroundColor = selected.FG;
                    WriteAt(selected.Texture, (j + x - cords.x1) * 2, i + y - cords.y1);
                    //WriteAt("EE", (j + x - cords.x1) * 2, i + y - cords.y1);
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = default;
                }
            }
        }

        static void Refresh_area_not(Game game, Player player, int[,] grid, Cordinates cords, Cordinates pos)
        {
            int y = cords.y;
            int x = cords.x;
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    int Id = grid[i + y - cords.y1, j + x - cords.x1];
                    var selected = player.Block_list.Find(x => x.id == Id);

                    Console.BackgroundColor = selected.BG;
                    Console.ForegroundColor = selected.FG;
                    WriteAt("██", (cords.x1 + j) * 2, cords.y1 + i);
                    //WriteAt("EE", (j + x - cords.x1) * 2, i + y - cords.y1);
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = default;
                }
            }
        }


        static void Print_window(Player player)
        {
            int index = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            
            foreach (var item in player.Block_list)
            {
                Console.BackgroundColor = ConsoleColor.White;
                WriteAt(item.Name + " " + item.quantity.ToString(), 3, 30 + index);
                WriteAt("* ", 1, 30 + index);
                if (item == player.Selected_block)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    WriteAt(item.Name + " " + item.quantity.ToString(), 3, 30 + index);
                    WriteAt(">",2, 30 + index);
                }

                index++;
                Console.ForegroundColor = default;
                Console.BackgroundColor = ConsoleColor.Cyan;
            }
            
        }

    }

}
