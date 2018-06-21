using System;
using System.Collections.Generic;

namespace Snake
{

    // Represent the level
    // ======================================
    // This class encapsulates the logic of 
    // rendering the level of the game
    class Level
    {
        LinkedList<GameObject> m_objects;

        // Constructor / Initialize object
        public Level()
        {
            m_objects =
                new LinkedList<GameObject>();
            this.Reset();
        }
        public Coordinate GetRandomPosition() {
            Coordinate pos;
            Random rnd = new Random();
            pos.X = (int)Math.Floor(rnd.NextDouble() * (Screen.MAX_X - 1));
            pos.Y = (int)Math.Floor(rnd.NextDouble() * (Screen.MAX_Y - 1));
            return pos;
        }
        public void AddBomb() {
            m_objects.AddFirst(
                new Bomb(GetRandomPosition()));
        }
        public void AddApple() {
            m_objects.AddFirst(
                new Apple(GetRandomPosition()));
        }
        // Randomize map
        // ======================================
        public void Reset() {
            m_objects.Clear();
            AddApple();
            AddBomb();
        }
        // Checks if an apple exists at the provided
        // position and if so, remove it
        // ======================================
        public void Test(Snake player)
        {
            bool createNew = false;
            foreach (GameObject go in m_objects) { 
                if (go.IsActive && player.Position.Equals(go.Position)) {
                    if (go.GetType() == typeof(Apple))
                    {
                        go.IsActive = false;
                        player.Grow();
                        player.Score += 1;
                        createNew = true;
                    }
                    if (go.GetType() == typeof(Bomb))
                    {
                        go.IsActive = false;
                        player.IsActive = false;
                    }
                }
            }
            if(!player.IsActive) {
                player.Spawn();
            }
            if(createNew) {
                AddApple();
            }
        }
        // Update all items on level
        // ======================================
        public void Update(Screen sc)
        {
            foreach (GameObject go in m_objects)
            {
                if (go.IsActive)
                {
                    go.Update();
                    go.Draw(sc);
                }
            }
        }
    }
    // Represent the screen
    // ======================================
    // This class encapsulates the logic of 
    // rendering the game
    public class Screen
    {
        public const int MAX_Y = 20;
        public const int MAX_X = 80;
        const int screen_size = MAX_X * MAX_Y;

        char[] m_buffer;

        // Constructor / Initialize object
        public Screen() {
            // Allocate screen buffer
            m_buffer =
                new char[screen_size];
            this.ClearBuffer();
        }
        // Clear the drawing buffer
        // ======================================
        public void ClearBuffer() {
            Console.Clear();
            for (int i = 0; i < m_buffer.Length; i++) {
                // Fill screen with spaces
                m_buffer[i] = ' ';
            }
        }
        // Clear the drawing buffer
        // ======================================
        public void DrawAt(Coordinate position, char data) {
            m_buffer[position.Y * Screen.MAX_X + position.X] = data;
        }
        // Render the screen
        // ======================================
        public void Render() {
            // Render screen
            for (int i = 0; i < m_buffer.Length; i++){
                Console.Write(m_buffer[i]);
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
            Level level = new Level();
            Snake snake = new Snake();

            // Game loop
            while (true)
            {
                sc.ClearBuffer();

                snake.Update();
                if (!snake.IsActive)
                {
                    snake.Spawn();
                }
                level.Update(sc);
                level.Test(snake);
                snake.Draw(sc);
                sc.Render();

                Console.Write(String.Format("Score: {2} Position: X:{0} Y:{1}", snake.Position.X,
                                            snake.Position.Y, snake.Score));

                // Check keyboard input
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            snake.Direction = Direction.LEFT;
                            break;
                        case ConsoleKey.RightArrow:
                            snake.Direction = Direction.RIGHT;
                            break;
                        case ConsoleKey.UpArrow:
                            snake.Direction = Direction.UP;
                            break;
                        case ConsoleKey.DownArrow:
                            snake.Direction = Direction.DOWN;
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
