using System;
using System.Collections.Generic;

namespace Snake {
    // Represent the level
    // ======================================
    // This class encapsulates the logic of 
    // rendering the level of the game
    class Level {
        public Snake Snake {
            get { return (Snake)m_objects[0]; }
        }
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
                pos.X = (int)Math.Floor(rnd.NextDouble() * (Screen.MAX_SIZE_X - 1));
                pos.Y = (int)Math.Floor(rnd.NextDouble() * (Screen.MAX_SIZE_Y - 1));
            } while (!IsEmptyPosition(pos));
            return pos;
        }
        // Randomize map
        // ======================================
        public void Reset() {
            m_objects.Clear();

            m_objects.Add(new Snake());
            // Add objects
            m_objects.Add(
                new Bomb(GetRandomPosition()));
            m_objects.Add(
                new Apple(GetRandomPosition()));
            m_objects.Add(
                new Apple(GetRandomPosition()));
            m_objects.Add(
                new Bouncer(GetRandomPosition()));
            m_objects.Add(
                new Bouncer(GetRandomPosition()));
            m_objects.Add(
                new Bouncer(GetRandomPosition()));
        }
        // Update all items on level
        // ======================================
        public void Update(Screen sc)
        {
            // Remove inactive objects
            // ======================================
            m_objects.RemoveAll(x => x.State == State.DEAD);

            // Add apples
            // ======================================
            while (m_objects.Count < 7) {
                m_objects.Add(
                    new Apple(GetRandomPosition()));
            }
            // Update alive objects
            // ======================================
            for (int i = 0; i < m_objects.Count; i ++ ){
                m_objects[i].Update();
            }
            for (int i = 0; i < m_objects.Count; i++) {
                for (int a = 0; a < m_objects.Count; a++) {
                    if (a != i) {
                        m_objects[i].Intersect(m_objects[a]);
                    }
                }
                m_objects[i].Draw(sc);
            }
            Console.Write(String.Format("Score: {2} Position: X:{0} Y:{1}", m_objects[0].Position.X,
                            m_objects[0].Position.Y, ((Snake)m_objects[0]).Score));

        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
           
            Screen sc = new Screen();
            Level level = new Level();

            // Game loop
            while (true) {
                long a = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                // Empty screen
                // ======================================
                sc.ClearBuffer();

                level.Update(sc);
                // Render screen
                // ======================================
                sc.Render();

                // Check keyboard input
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case ConsoleKey.UpArrow:
                            level.Snake.Direction = Direction.NORTH;
                            break;
                        case ConsoleKey.LeftArrow:
                            level.Snake.Direction = Direction.WEST;
                            break;
                        case ConsoleKey.RightArrow:
                            level.Snake.Direction = Direction.EAST;
                            break;
                        case ConsoleKey.DownArrow:
                            level.Snake.Direction = Direction.SOUTH;
                            break;
                        case ConsoleKey.Q:
                            level.Snake.Direction = Direction.NORTH_WEST;
                            break;
                        case ConsoleKey.W:
                            level.Snake.Direction = Direction.NORTH_EAST;
                            break;
                        case ConsoleKey.Escape:
                            return; // Exit                                          
                    }
                }
                long sleep = 75L - (DateTimeOffset.Now.ToUnixTimeMilliseconds() -a);
                // Slow down
                System.Threading.Thread.Sleep(sleep < 0 ? 0 : (int)sleep);
            }
        }
    }
}
