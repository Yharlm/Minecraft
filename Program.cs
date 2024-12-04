
//using ConsoleNewMinigame;
using System.ComponentModel.Design;
using System.Security;
using System.Timers;

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


            while (true)
            {
                
                Console.CursorVisible = false;
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                WriteAt("Minecraft v0.0.2.7, Added entities, also temporearely ability to inf jump lol", 1, 1);
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
                //Default = new Solid("water", 5, "  ", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue); player.Block_list.Add(Default);

                Default = new Solid("waterTop", 6, "▄▄", ConsoleColor.DarkBlue, ConsoleColor.DarkBlue); player.Block_list.Add(Default);
                Default = new Solid("Leaves", 7, "▄▀", ConsoleColor.DarkGreen, ConsoleColor.Green); player.Block_list.Add(Default);

                Entity Mob = new Entity(null, 0, null); overworld.Entity_list.Add(Mob);
                Mob = new Entity("pig", 0, null); overworld.Entity_list.Add(Mob);
                Mob = new Entity("TNT", 0, null); overworld.Entity_list.Add(Mob);
                //Entity pig = new Entity("pig", 10, null); Mob pig.gravity(grid);







                //Move this to the block method using switch case or make a new class block


                int[,] grid = new int[100, 100];
                BuildWorld(grid, player, overworld);
                double tick = 0.05;

                while (player.health > 0){
                    
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
                WriteAt("YOU ARE DEAD..",50,14);
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
            

            Console.ReadLine();


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

            Player player = (Player)instance;
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

        static bool GetRadius(Entity mob1, Player plr, int distance)
        {
            bool res = false;
            if (mob1.cordinates.x > plr.x - distance && mob1.cordinates.x < plr.x + distance)
            {
                res = true;
            }

            return res;
        }
        static bool GetRadius_forplayer(Cordinates object1, Cordinates object2, int distance)
        {
            bool res = false;
            if (object1.x > object2.x - distance && object1.x < object2.x + distance)
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
            Cordinates player_cords = new Cordinates();
            player_cords.x = x;
            player_cords.y = y;
            //player.Selected_block = dirt;
            switch (player.Input)
            {
                case "N":
                    try
                    {
                        foreach (Entity entity in game.Existing_Entities)
                        {
                            Explosion(game, grid, entity.cordinates,player);
                            WriteAt("  ", entity.cordinates.x * 2, entity.cordinates.y);
                            game.Existing_Entities.Remove(entity);
                        }
                    }
                    catch { }
                    break;
                case "Q":
                    if (player.Holding == true)
                    {
                        player.held = null;
                        player.Holding = false;
                    }
                    else
                    {
                        player.Holding = true;
                    }

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

                        Entity mob = game.Entity_list[0];
                        Entity Default = new Entity(mob.Name, mob.Health, mob.Type);
                        //Default.cordinates.x = random.Next(4, 55);
                        Default.cordinates.x = player.x
                        ; Default.cordinates.y = player.y - 3;



                        game.Existing_Entities.Add(Default);
                        //game.Spawn_entity(entity);

                        break;
                    }
                case "E":
                    player.hotbar++;
                    Console.ForegroundColor = ConsoleColor.Red;

                    player.Selected_block = player.Block_list[player.hotbar]; WriteAt(player.Selected_block.Name + "    ", 55, 2);
                    Console.ForegroundColor = default;
                    if (player.hotbar == player.Block_list.Count - 1) player.hotbar = 0;


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
                        if (grid[player.y + 1, player.x] == 0 && grid[player.y + 2, player.x] != 0)
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

        static void Entity_update(int[,] grid, List<Entity> Entity_list, Game game, Player player)
        {
            PlayerAbilities(grid, player, game);

            foreach (Entity entity in game.Existing_Entities)
            {

                entity.gravity(grid, game.curent_tick);
            }

            Walk_to_player(game.Existing_Entities[0], player, grid);


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
                        if (GetRadius(ent, player, 1))
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
        static void Explosion(Game game, int[,] grid, Cordinates pos,Player player)
        {
            game.delay(400);
            Solid air = new Solid("air", 0, "  ", ConsoleColor.DarkGray, ConsoleColor.Cyan);

            int range = 4;
            int range_max = 9;
            int x = pos.x;
            int y = pos.y;



            Fill_Index_Cord2(x - range, y - range, x + range + 1, y + range + 1, grid, air, 30);
            Fill_Index_Cord2(x - range_max, y - range_max - range, x + range_max + 1, y + range_max + 1 - range, grid, air, 2);

            if(GetRadius_forplayer(pos, Convert_cor(player.x, player.y),range_max)) { player.health -= 50; }
            if (GetRadius_forplayer(pos, Convert_cor(player.x, player.y), range)) { player.health -= 50; }
        }

        static void Walk_to_player(Entity entity, Player player, int[,] grid)
        {
            if (player.x < entity.cordinates.x)
            {
                if (grid[player.y, entity.cordinates.x - 1] == 0) {
                    entity.cordinates.x--;
                        }
            }
        }




    }
}
