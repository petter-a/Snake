using System;
namespace Snake
{
    public class Snake : GameObject
    {
        const int m_initial_size = 8;
        const int m_limit_size = 100;

        int m_size;
        Coordinate[] m_tail;

        int m_score;
        public int Score {
            get { return m_score; }
            set { m_score = value; }
        } 

        // Constructor / Initialize object
        public Snake() : base(new Coordinate()) {
            this.m_tail = new Coordinate[m_limit_size];
            this.Spawn();
        }
        // Update state
        // ======================================
        public override void Update() {
            // Shift left all positions from previous move
            // Example: If moving left:
            // |10|11|12|13|14| becomes |09|10|11|12|13|
            m_tail[0] = m_position;
            for (int i = (m_size - 1); i > 0; i--) {
                m_tail[i] = m_tail[i - 1];
            }
            // Call Baseclass
            base.Update();
        }
        // Draw the object
        // ======================================
        public override void Draw(Screen sc) {
            // Draw Snake
            sc.DrawAt(m_position, '•');

            for (int i = 0; i < m_size; i++) {
                sc.DrawAt(this.m_tail[i], '•');
            }
        }
        // Grow the player
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
        // Spawns up the player on the screen
        // ======================================
        public void Spawn() {
            this.m_direction = Direction.LEFT;
            this.m_score = 0;
            this.m_isActive = true;
            this.m_size = m_initial_size; // Initial size

            // Start at center of the screen
            this.m_position.X = Screen.MAX_X / 2 - 1;
            this.m_position.Y = Screen.MAX_Y / 2 - 1;

            for (int i = 0; i < this.m_size; i++)
            {
                this.m_tail[i].X = Screen.MAX_X / 2 - 1 + i;
                this.m_tail[i].Y = Screen.MAX_Y / 2 - 1;
            }
        }
    }
}
