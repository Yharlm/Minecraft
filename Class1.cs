namespace Minecraft
{

    class Player : Cordinates

    {
        public List<Recipe> Recipes = new List<Recipe>();
        public Cordinates last_popup;
        public int health = 100;
        public Entity held = null;
        public bool Holding = false;
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
        public Solid GetBlock(string name)
        {
            return Block_list.Find(x => x.Name == name);
        }
    }
    class Sprite
    {
        public string[,] Sprites;
    }
    class Projectile : Entity
    {
        public string sprite;
        public Projectile(string name, int health, string type, string sprite) : base(name, health, type, sprite)
        {

        }
    }
    class Inventory : Player
    {
        //public List<Solid> Block_list = new List<Solid>();
    }
    class Cordinates
    {
        public int x = 11;
        public int y = 11;
        public int x1 = 0;
        public int y1 = 0;
        public int x2 = 0;
        public int y2 = 0;
        public Cordinates Convert_cor(int x, int y)
        {
            Cordinates cords = new Cordinates();
            cords.y = y;
            cords.x = x;
            return cords;
        }

    }

    class Game : Player
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
        public Cordinates cordinates = new Cordinates();
        public int number = 0;

        public double time = 0;
        public bool curent_tick = false;
        public List<Entity> Entity_list = new List<Entity>();
        public List<Entity> Existing_Entities = new List<Entity>();


        public void Spawn_entity(Entity mob)
        {
            Entity_list.Add(mob);
            Existing_Entities.Add(mob);
            //number++;
        }

        protected double count = 0;
        public bool delay(double delay)
        {
            if (curent_tick)
            {
                count += 1;
            }
            if (count == delay)
            {
                count = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void gravity(int[,] grid, Game game, List<Entity> Exists)
        {
            int velocity = 0;
            foreach (Entity ent in Exists)
            {

            }
            //WriteAt("  ", cordinates.x, cordinates.y);
            if (grid[cordinates.y + 1, cordinates.x] == 0)
            {

                if (grid[cordinates.y - 2, cordinates.x] == 0 && game.curent_tick)
                {
                    WriteAt("  ", cordinates.x * 2, cordinates.y);
                    cordinates.y++;
                    cordinates.x += velocity;
                }
            }

            WriteAt("██", cordinates.x * 2, cordinates.y);

        }
    }

    class Block_ids(int Id, string texture, ConsoleColor fG, ConsoleColor bG)
    {
        public int id = Id;
        public string Texture = texture;
        public ConsoleColor FG = fG;
        public ConsoleColor BG = bG;


    }

    class Recipe
    {
        public Solid item;
        public List<Solid> required = new List<Solid>();



    }
    class Solid(string name, int Id, string texture, ConsoleColor fG, ConsoleColor bG)
    {
        public int quantity = 0;
        public string Name = name;
        public int id = Id;
        public string Texture = texture;
        public ConsoleColor FG = fG;
        public ConsoleColor BG = bG;

    }
    class Non_solid(string name, int Id, string texture, ConsoleColor fG, ConsoleColor bG)
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

        public static void Walk_to_player(Entity entity, Player player, int[,] grid, Game game)
        {

            int speed = 2;
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
        public void Shoot_WithImaginaryProjectiles()
        {

        }

    }
    class Entity(string name, int health, string type, string sprite)
    {
        public int velocity = 0;
        public string Name = name;
        public int Health = health;
        public string Type = type;
        public List<Behaviour> mob_ais = new List<Behaviour>();
        protected static int origRow;
        protected static int origCol;
        public string Sprite = sprite;
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
        public bool grounded = true;

        public Cordinates cordinates = new Cordinates();

        public void gravity(int[,] grid, bool time)
        {


            //WriteAt("  ", cordinates.x, cordinates.y);
            if (grid[cordinates.y + 1, cordinates.x] == 0 && time)
            {


                WriteAt("  ", cordinates.x * 2, cordinates.y);

                if (grid[cordinates.y + 2, cordinates.x] == 0)
                {
                    cordinates.y += velocity + 1;
                }
                else
                {
                    cordinates.y += 1;
                }
                cordinates.x += velocity;

            }
            else if (grid[cordinates.y + 1, cordinates.x] != 0)
            {
                velocity = 0;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            //WriteAt("██", cordinates.x * 2, cordinates.y - 1);
            WriteAt(sprite, cordinates.x * 2, cordinates.y);
            Console.ForegroundColor = default;
        }


    }
}
