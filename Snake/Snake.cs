using System;
namespace Snake {
    public class Snake : GameObject {
        const int m_initial_size = 4;
        const int m_limit_size = 100;

        bool m_swap_frame;
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
        public override bool Update() {
            // Shift frame
            m_swap_frame = !m_swap_frame;
            // Shift left all positions from previous move
            m_tail[0] = m_position;
            for (int i = (m_size - 1); i > 0; i --) {
                m_tail[i] = m_tail[i -1];           
            }
            // Call baseclass to perform default logic
            // ===================================================
            // This call could be removed if no default handling is required/needed.
            return base.Update();
        }
        // ======================================
        // Draw the object
        // ======================================
        public override void Draw(Screen sc) {
            char graphic = m_swap_frame ? '<' : '>';
            // Add Drawing logic
            // ======================================
            // Draw this object type on the screen
            sc.DrawAt(m_position, graphic);
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
                    Grow();
                    Score ++;
                    return true;
                }
                // Collide with Bomb
                if (go.GetType() == typeof(Bomb)) {
                    ReSpawn();
                    return true;
                }
                // Collide with Bouncer
                if (go.GetType() == typeof(Bouncer))
                {
                    m_direction = GetReverseDirection();
                    return true;
                }
            }
            // Check if tail is hit by a bomb
            // ======================================
            if (go.GetType() == typeof(Bomb)) {
                for (int i = 0; i < m_size; i++) {
                    if (m_tail[i].Equals(go.Position)) {
                        ReSpawn();
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
            this.m_state = State.MOVING;
            this.m_size = m_initial_size;
            this.Score = 0;
            this.m_swap_frame = true;

            // Start at center of the screen
            this.m_position.X = Screen.MAX_SIZE_X/2 - 1;
            this.m_position.Y = Screen.MAX_SIZE_Y/2 - 1;

            for (int i = 0; i < this.m_size; i++)
            {
                this.m_tail[i].X = Screen.MAX_SIZE_X/2 - 1 + i;
                this.m_tail[i].Y = Screen.MAX_SIZE_Y/2 - 1;
            }
        }
    }
}
