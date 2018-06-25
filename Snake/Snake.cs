using System;
namespace Snake
{
    public class Snake : GameObject {
        const int m_initial_size = 4;
        const int m_limit_size = 100;

        int m_size;
        Coordinate[] m_tail;
        public int Score { get; set; }
        // ======================================
        // Constructor / Initialize object
        // ======================================
        // Note that a new Coordinate struct is passed
        // straight away to the base class constructor
        public Snake() : base(new Coordinate()) {
            this.m_tail = new Coordinate[
                m_limit_size];
            this.ReSpawn();
        }
        // ======================================
        // Update state
        // ======================================
        public override bool Update(Screen sc) {
            // Shift left all positions from previous move
            m_tail[0] = m_position;
            for (int i = (m_size - 1); i > 0; i --) {
                m_tail[i] = m_tail[i -1];           
            }
            // Call baseclass to perform default logic
            // ===================================================
            // This call could be removed if no default handling is required/needed.
            return base.Update(sc);
        }
        // ======================================
        // Draw the object
        // ======================================
        public override void Draw(Screen sc) {
            // Add Drawing logic
            // ======================================
            // Draw this object type on the screen
            sc.DrawAt(m_position, (char)248);
            // Draw tail
            for (int i = 0; i < m_size; i++) {
                sc.DrawAt(this.m_tail[i], (char)248);
            }
        }
        // Handle Collisions
        // ======================================
        // Define behaviour when an object is colliding
        // with another
        public override bool Intersect(GameObject go) {
            if(base.Intersect(go)) { 
                // Collide with Apple
                if (go.GetType() == typeof(Apple)) {
                    go.State = State.DEAD;
                    this.Grow();
                    this.Score ++;
                    return true;
                }
                // Collide with Bomb
                if (go.GetType() == typeof(Bomb)) {
                    this.State = State.DEAD;
                    return true;
                }
                if (go.GetType() == typeof(Wall))
                {
                    this.Direction = GetOppositeDirection(Border.LEFT);
                    return true;
                }
            }
            // Check if tail is crossed by a bomb
            // ======================================
            if (go.GetType() == typeof(Bomb)) {
                for (int i = 0; i < m_size; i++) {
                    if (m_tail[i].Equals(go.Position)) {
                        m_State = State.DEAD;
                        return true;
                    }
                }
            }


            return false;

        }
        // ======================================
        // Increase the size of the snake
        // ======================================
        public void Grow() {
            if (m_size < m_limit_size) {
                ++m_size;
                for (int i = (m_size - 1); i > 0; i--) {
                    m_tail[i] = m_tail[i - 1];
                }
                m_tail[0] = m_position;
            }
        }
        // ======================================
        // Spawns up the player on the center of the screen
        // ======================================
        public void ReSpawn() {
            this.m_direction = Direction.WEST;
            this.m_State = State.MOVING;
            this.m_size = m_initial_size;
            this.Score = 0;

            // Start at center of the screen
            this.m_position.X = Screen.MAX_X/2 - 1;
            this.m_position.Y = Screen.MAX_Y/2 - 1;

            for (int i = 0; i < this.m_size; i++)
            {
                this.m_tail[i].X = Screen.MAX_X/2 - 1 + i;
                this.m_tail[i].Y = Screen.MAX_Y/2 - 1;
            }
        }
    }
}
