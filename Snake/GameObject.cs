using System;
namespace Snake {
    // ======================================
    // Describes a coordinate on the screen
    // ======================================
    // The console is made up by rows and columns
    public struct Coordinate {
        public int X;
        public int Y;

        public bool Equals(Coordinate b) {
            return (X == b.X && Y == b.Y);
        }
    }
    // ======================================
    // Describes direction
    // ======================================
    // Used to determine the direction of the player
    public enum Direction {
        NORTH_WEST,
        NORTH_EAST,
        NORTH,
        WEST,
        CENTER,
        EAST,
        SOUTH_WEST,
        SOUTH_EAST,
        SOUTH
    }
    // ======================================
    // Describes direction
    // ======================================
    // Used to determine the border of the screen
    public enum Border
    {
        NONE,
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    }
    // The game object
    // ======================================
    // The base class of all objects in the game
    public class GameObject {
        // ======================================
        // Position
        // ======================================
        protected Coordinate m_position;

        public Coordinate Position {
            get { return m_position; }
        }
        // ======================================
        // Direction
        // ======================================
        protected Direction m_direction;

        public Direction Direction {
            get { return m_direction; }
            set { m_direction = value; }
        }
        // ======================================
        // Is active
        // ======================================
        protected bool m_isActive;

        public bool IsActive {
            get { return m_isActive; }
            set { m_isActive = value; }
        }
        // ======================================
        // Swap Direction
        // ======================================
        protected bool m_swap_direction;

        public bool SwapDirection {
            get { return m_swap_direction; }
            set { m_swap_direction = value; }
        }
        // ======================================
        // Constructor/Initialize Object
        // ======================================
        public GameObject(Coordinate position) {
            m_direction = Direction.CENTER;
            m_position = position;
            m_isActive = true;
            m_swap_direction = false;
        }
        // ======================================
        // Update state
        // ======================================
        public virtual bool Update(Screen sc) {
            if (m_isActive) {
                switch (this.m_direction) {
                    case Direction.NORTH_WEST:
                        m_position.Y--;
                        m_position.X--;
                        break;
                    case Direction.NORTH:
                        m_position.Y --;
                        break;
                    case Direction.NORTH_EAST:
                        m_position.Y--;
                        m_position.X++;
                        break;
                    case Direction.CENTER:
                        // Be still
                        break;
                    case Direction.WEST:
                        m_position.X--;
                        break;
                    case Direction.EAST:
                        m_position.X++;
                        break;
                    case Direction.SOUTH_WEST:
                        m_position.Y++;
                        m_position.X--;
                        break;
                    case Direction.SOUTH:
                        m_position.Y++;
                        break;
                    case Direction.SOUTH_EAST:
                        m_position.Y++;
                        m_position.X++;
                        break;
                }
                // Calculate screen position
                // ======================================
                NormalizePosition();
            }
            return true;
        }
        // ======================================
        // Draw the object
        // ======================================
        public virtual void Draw(Screen sc) {
            sc.DrawAt(m_position, ' ');
        }
        // ======================================
        // Handle Collisions
        // ======================================
        public virtual bool Intersect(GameObject go) {
            // Override in subclass to handle collision
            // return true if handled, otherwise false
            return false;
        }
        // ======================================
        // Move in the opposite direction
        // ======================================
        protected void SetOppositeDirection(Border hit) {
            switch (this.m_direction)
            {
                case Direction.NORTH_WEST:
                    if(hit == Border.LEFT) {
                        this.m_direction = Direction.NORTH_EAST;
                    } else {
                        this.m_direction = Direction.SOUTH_WEST;
                    }
                    break;
                case Direction.NORTH:
                    this.m_direction = Direction.SOUTH;
                    break;
                case Direction.NORTH_EAST:
                    if(hit == Border.RIGHT) {
                        this.m_direction = Direction.NORTH_WEST;
                    } else { 
                        this.m_direction = Direction.SOUTH_EAST;
                    }
                    break;
                case Direction.CENTER:
                    break;
                case Direction.WEST:
                    this.m_direction = Direction.WEST;
                    break;
                case Direction.EAST:
                    this.m_direction = Direction.EAST;
                    break;
                case Direction.SOUTH_WEST:
                    if(hit == Border.LEFT) {
                        this.m_direction = Direction.SOUTH_EAST;
                    }
                    else { 
                        this.m_direction = Direction.NORTH_WEST;
                    }
                    break;
                case Direction.SOUTH:
                    this.m_direction = Direction.NORTH;
                    break;
                case Direction.SOUTH_EAST:
                    if(hit == Border.RIGHT) {
                        this.m_direction = Direction.SOUTH_WEST;
                    }
                    else { 
                        this.m_direction = Direction.NORTH_EAST;
                    }
                    break;
            }
        }
        // ======================================
        // Calculate positions
        // ======================================
        public void NormalizePosition() {
            // ======================================
            // Set Border
            // ======================================
            Border border = Border.NONE;
            // Calculate MAX X
            // ======================================
            if (m_position.X == (Screen.MAX_X - 1)) {
                border = Border.RIGHT;
                m_position.X -= (m_swap_direction ? 1 : Screen.MAX_X - 1);
            }
            else
            // Calculate MIX X 
            // ======================================
            if (m_position.X == -1) {
                border = Border.LEFT;
                m_position.X += (m_swap_direction ? 1 : Screen.MAX_X);
            }
            // Calculate MAX y
            // ======================================
            if (m_position.Y == (Screen.MAX_Y - 1)) {
                border = Border.BOTTOM;
                m_position.Y -= (m_swap_direction ? 1 : Screen.MAX_Y - 1);
            }
            else
            // Calculate MIN Y 
            // ======================================
            if (m_position.Y == -1) {
                border = Border.TOP;
                m_position.Y += (m_swap_direction ? 1 : Screen.MAX_Y);
            }
            if(m_swap_direction && border != Border.NONE) {
                SetOppositeDirection(border);
            }
        }

    }
}
