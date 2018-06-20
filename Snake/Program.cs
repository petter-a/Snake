using System;

namespace Snake
{
    // Describes direction
    // ======================================
    // Used to determine the direction of the player
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    // Describes a coordinate on the screen
    // ======================================
    // The console is made up by rows and columns
    struct Coordinate
    {
        public int X;
        public int Y;

        public bool Equals(Coordinate b)
        {
            return (X == b.X && Y == b.Y);
        }
    }
    // Represent the level
    // ======================================
    // This class encapsulates the logic of 
    // rendering the level of the game
    class Level
    {
        Coordinate[] m_apples;
        int m_current_index;
        int m_limit;

        // Constructor / Initialize object
        public Level(int limit)
        {
            this.m_limit = limit;
            m_apples =
                new Coordinate[limit];
            this.Reset();
        }
        // Checks if an apple exists at the provided
        // position and if so, remove it
        // ======================================
        public bool EatApple(Coordinate position)
        {
            if (m_current_index < m_limit)
            {
                if (position.Equals(m_apples[m_current_index]))
                {
                    m_current_index++;
                    return true;
                }
            }
            // No apple at position
            return false;
        }
        // Randomize map
        // ======================================
        public void Reset()
        {
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                m_apples[i].X = (int)Math.Floor(rnd.NextDouble() * (Screen.MAX_X - 1));
                m_apples[i].Y = (int)Math.Floor(rnd.NextDouble() * (Screen.MAX_Y - 1));
            }
            // Set current index
            m_current_index = 0;
        }
        // Draw the level on the screen
        // ======================================
        public void Draw(Screen sc)
        {
            sc.setAt(m_apples[m_current_index], '');
        }
    }
    // Represent the player
    // ======================================
    // This class encapsulates the logic of 
    // the player
    class Player
    {
        const int m_initial_size = 8;

        int m_size;
        int m_limit;
        Coordinate[] m_position;
        Direction m_direction;

        // Get/Set the current direction
        // ======================================
        public Direction Direction
        {
            get { return m_direction; }
            set { m_direction = value; }
        }
        // Get the current position
        // ======================================
        public Coordinate Position
        {
            get { return m_position[0]; }
        }
        // Get the current score
        // ======================================
        public int Score
        {
            get { return m_size - m_initial_size; }
        }

        // Constructor / Initialize object
        // ======================================
        public Player(int limit)
        {
            this.m_limit = limit;
            this.m_position = new Coordinate[limit];
            Spawn();
        }

        // Move the player on screen. 
        // ======================================
        // Once reaching the border of the playable area
        // the player will wrap around to the opposite side
        // of the screen
        public void Move()
        {

            // Shift left all positions from previous move
            // Example: If moving left:
            // |10|11|12|13|14| becomes |09|10|11|12|13|
            for (int i = (m_size - 1); i > 0; i--)
            {
                m_position[i] = m_position[i - 1];
            }
            switch (this.m_direction)
            {
                case Direction.UP:
                    // If the player is heading upwards and reaches the boundary
                    // of the screen, then wrap around to the bottom
                    m_position[0].Y = (m_position[0].Y == 0) ? (Screen.MAX_Y - 1) :
                        (m_position[0].Y - 1);
                    break;
                case Direction.DOWN:
                    // If the player is heading downwards and reaches the boundary
                    // of the screen, then wrap around to the top
                    m_position[0].Y = (m_position[0].Y == (Screen.MAX_Y - 1)) ? 0 :
                        (m_position[0].Y + 1);
                    break;
                case Direction.LEFT:
                    // If the player is heading left and reaches the boundary
                    // of the screen, then wrap around to the right end 
                    m_position[0].X = (m_position[0].X == 0) ? (Screen.MAX_X - 1) :
                        (m_position[0].X - 1);
                    break;
                case Direction.RIGHT:
                    // If the player is heading right and reaches the boundary
                    // of the screen, then wrap around to the left end
                    m_position[0].X = (m_position[0].X == (Screen.MAX_X - 1)) ? 0 :
                        (m_position[0].X + 1);
                    break;
            }
        }
        // Check if the player is alive
        // ======================================
        public bool isAlive()
        {
            for (int i = 1; i < m_size; i++)
            {
                // Crash on self
                if (m_position[0].Equals(m_position[i]))
                {
                    return false;
                }
            }
            return true;
        }
        // Grow the player
        // ======================================
        public void Grow()
        {
            if (m_size < m_limit)
            {
                ++m_size;
                for (int i = (m_size - 1); i > 0; i--)
                {
                    m_position[i] = m_position[i - 1];
                }
            }
        }
        // Spawns up the player on the screen
        // ======================================
        public void Spawn()
        {
            this.m_direction = Direction.LEFT;
            this.m_size = m_initial_size; // Initial size

            // Start at center of the screen
            for (int i = 0; i < this.m_size; i++)
            {
                this.m_position[i].X = Screen.MAX_X / 2 - 1 + i;
                this.m_position[i].Y = Screen.MAX_Y / 2 - 1;
            }
        }
        // Draw the player on screen
        // ======================================
        public void Draw(Screen sc)
        {
            // Draw player
            for (int i = 0; i < m_size; i++)
            {
                sc.setAt(this.m_position[i], '•');
            }
        }
    }
    // Represent the screen
    // ======================================
    // This class encapsulates the logic of 
    // rendering the game
    class Screen
    {
        public const int MAX_Y = 20;
        public const int MAX_X = 80;
        const int screen_size = MAX_X * MAX_Y;

        char[] m_positions;

        // Initializa object
        public Screen()
        {
            // Allocate screen buffer
            m_positions =
                new char[screen_size];
            this.Clear();
        }
        public void Clear()
        {
            this.Fill(' ');
        }
        public void Fill(char data)
        {
            Console.Clear();
            for (int i = 0; i < m_positions.Length; i++)
            {
                // Fill screen with spaces
                m_positions[i] = data;
            }
        }
        //
        public void setAt(Coordinate position, char data)
        {
            m_positions[position.Y * Screen.MAX_X + position.X] = data;
        }
        public void Render()
        {

            // Render screen
            for (int i = 0; i < m_positions.Length; i++)
            {
                Console.Write(m_positions[i]);
            }
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.SetWindowSize(80, 20);
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Screen sc = new Screen();
            Player player = new Player(100);
            Level level = new Level(100);
            player.Spawn();

            // Game loop
            while (true)
            {
                player.Move();
                if (!player.isAlive())
                {
                    player.Spawn();
                }
                if (level.EatApple(player.Position))
                {
                    player.Grow();
                }
                sc.Clear();
                level.Draw(sc);
                player.Draw(sc);
                sc.Render();

                Console.Write(String.Format("Score: {2} Position: X:{0} Y:{1}", player.Position.X,
                                            player.Position.Y, player.Score));

                // Check keyboard input
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            player.Direction = Direction.LEFT;
                            break;
                        case ConsoleKey.RightArrow:
                            player.Direction = Direction.RIGHT;
                            break;
                        case ConsoleKey.UpArrow:
                            player.Direction = Direction.UP;
                            break;
                        case ConsoleKey.DownArrow:
                            player.Direction = Direction.DOWN;
                            break;
                        case ConsoleKey.Escape:
                            return; // Exit                                          
                    }
                }
                // Slow down
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
