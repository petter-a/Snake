using System;
using System.Collections.Generic;

namespace Snake
{

    // Represent the level
    // ======================================
    // This class encapsulates the logic of 
    // rendering the level of the game
    class Level {
        List<GameObject> m_objects;
        // ======================================
        // Constructor / Initialize object
        // ======================================
        public Level() {
            m_objects =
                new List<GameObject>();
            this.Reset();
        }
        // ======================================
        // Clear the drawing buffer
        // ======================================
        public bool IsEmptyPosition(Coordinate position) {
            foreach(GameObject go in m_objects) {
                if(go.Position.Equals(position)) {
                    return false;
                }
            }
            return true;
        }
        // ======================================
        // Get a random position that is not occupied
        // ======================================
        public Coordinate GetRandomPosition() {
            Coordinate pos;
            do {
                Random rnd = new Random();
                pos.X = (int)Math.Floor(rnd.NextDouble() * (Screen.MAX_X - 1));
                pos.Y = (int)Math.Floor(rnd.NextDouble() * (Screen.MAX_Y - 1));
            } while (!IsEmptyPosition(pos));
            return pos;
        }
        // Randomize map
        // ======================================
        public void Reset() {
            m_objects.Clear();

            // Add objects
            m_objects.Add(
                new Bomb(GetRandomPosition()));
            m_objects.Add(
                new Apple(GetRandomPosition()));
        }
        // Checks if an apple exists at the provided
        // position and if so, remove it
        // ======================================
        public void Test(Snake player)
        {
            foreach (GameObject go in m_objects) {
            }

        }
        // Update all items on level
        // ======================================
        public void Update(Screen sc, Snake player)
        {
            // Remove inactive objects
            // ======================================
            m_objects.RemoveAll(x => x.IsActive == false);

            // Add apples
            // ======================================
            if (m_objects.Count < 2) {
                m_objects.Add(
                    new Apple(GetRandomPosition()));
            }
            // Update snake
            // ======================================
            player.Update(sc);
            // Update alive objects
            // ======================================
            foreach (GameObject go in m_objects) {
                player.Intersect(go);
                go.Update(sc);
                go.Draw(sc);
            }
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
           
            Screen sc = new Screen();
            Level level = new Level();
            Snake snake = new Snake();

            // Game loop
            while (true) {
                // Empty screen
                // ======================================
                sc.ClearBuffer();

                level.Update(sc, snake);
                if (!snake.IsActive)
                {
                    snake.ReSpawn();
                }
                snake.Draw(sc);
                // Render screen
                // ======================================
                sc.Render();

                Console.Write(String.Format("Score: {2} Position: X:{0} Y:{1}", snake.Position.X,
                                            snake.Position.Y, snake.Score));

                // Check keyboard input
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case ConsoleKey.UpArrow:
                            snake.Direction = Direction.NORTH;
                            break;
                        case ConsoleKey.LeftArrow:
                            snake.Direction = Direction.WEST;
                            break;
                        case ConsoleKey.RightArrow:
                            snake.Direction = Direction.EAST;
                            break;
                        case ConsoleKey.DownArrow:
                            snake.Direction = Direction.SOUTH;
                            break;
                        case ConsoleKey.Escape:
                            return; // Exit                                          
                    }
                }
                // Slow down
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}
