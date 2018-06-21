using System;
namespace Snake
{
    // Describes a coordinate on the screen
    // ======================================
    // The console is made up by rows and columns
    public struct Coordinate
    {
        public int X;
        public int Y;

        public bool Equals(Coordinate b) {
            return (X == b.X && Y == b.Y);
        }
    } 
    // Describes direction
    // ======================================
    // Used to determine the direction of the player
    public enum Direction
    {
        CENTER,
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    // The game object
    // ======================================
    // The base class of all objects in the game
    public class GameObject
    {
        protected Coordinate m_position;
        public Coordinate Position {
            get { return m_position; }
        }
        protected Direction m_direction;
        public Direction Direction {
            get { return m_direction; }
            set { m_direction = value; }
        }
        protected bool m_isActive;
        public bool IsActive {
            get { return m_isActive; }
            set { m_isActive = value; }
        }
        public GameObject(Coordinate position) {
            m_direction = Direction.CENTER;
            m_position = position;
            m_isActive = true;
        }
        // Update state
        // ======================================
       public virtual void Update() {
            if(m_isActive) {
                switch (this.m_direction)
                {
                    case Direction.UP:
                        // If the object is heading upwards and reaches the boundary
                        // of the screen, then wrap around to the bottom
                        m_position.Y = (m_position.Y == 0) ? (Screen.MAX_Y - 1) :
                            (m_position.Y - 1);
                        break;
                    case Direction.DOWN:
                        // If the object is heading downwards and reaches the boundary
                        // of the screen, then wrap around to the top
                        m_position.Y = (m_position.Y == (Screen.MAX_Y - 1)) ? 0 :
                            (m_position.Y + 1);
                        break;
                    case Direction.LEFT:
                        // If the object is heading left and reaches the boundary
                        // of the screen, then wrap around to the right end 
                        m_position.X = (m_position.X == 0) ? (Screen.MAX_X - 1) :
                            (m_position.X - 1);
                        break;
                    case Direction.RIGHT:
                        // If the object is heading right and reaches the boundary
                        // of the screen, then wrap around to the left end
                        m_position.X = (m_position.X == (Screen.MAX_X - 1)) ? 0 :
                            (m_position.X + 1);
                        break;
                }
            }
        }
        // Draw the object
        // ======================================
        public virtual void Draw(Screen sc) {
            sc.DrawAt(m_position, ' ');
        }
        // Handle Collisions
        // ======================================
        public virtual void Intersect(GameObject go)
        {
            // Override in subclass to handle collision
        }
    }
}
