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
    public enum Direction 
    {
        NORTH,
        SOUTH,
        WEST,
        EAST,
        NONE,
        NORTH_WEST,
        NORTH_EAST,
        SOUTH_WEST,
        SOUTH_EAST
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
    // ======================================
    // Describes object state
    // ======================================
    // Used to determine the border of the screen
    public enum State 
    {
        WAITING,
        MOVING,
        DEAD
    }
    // ======================================
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
        // State
        // ======================================
        protected State m_state;

        public State State { 
            get { return m_state; }
            set { m_state = value; }
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
            m_direction = Direction.NONE;
            m_position = position;
            m_swap_direction = false;
        }
        // ======================================
        // Update state
        // ======================================
        public virtual bool Update() {
            if (m_state == State.MOVING) {
                Coordinate c = new Coordinate();
                switch (this.m_direction) {
                    case Direction.NORTH_WEST:
                        c.Y --; c.X --;
                        break;
                    case Direction.NORTH:
                        c.Y--;
                        break;
                    case Direction.NORTH_EAST:
                        c.Y--; c.X ++;
                        break;
                    case Direction.WEST:
                        c.X--;
                        break;
                    case Direction.EAST:
                        c.X++;
                        break;
                    case Direction.SOUTH_WEST:
                        c.Y++; c.X--;
                        break;
                    case Direction.SOUTH:
                        c.Y++;
                        break;
                    case Direction.SOUTH_EAST:
                        c.Y++; c.X++;
                        break;
                }
                // Calculate screen position
                // ======================================
                NormalizePosition(c);
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
        // Check for Collisions
        // ======================================
        public virtual bool Intersect(GameObject go) {
            // Override in subclass to handle collision
            // return true if collides, otherwise false
            if (m_position.Equals(go.Position)) {
                return true;
            }
            return false;
        }
        // ======================================
        // Get Collition 
        // ======================================
        public Border PredictObjectHit() {            
            switch (this.m_direction) {
                case Direction.NORTH_WEST:
                    return Border.RIGHT;
                case Direction.NORTH_EAST:
                    return Border.LEFT;
                case Direction.NORTH:
                    return Border.BOTTOM;
                case Direction.WEST:
                    return Border.RIGHT;
                case Direction.EAST:
                    return Border.LEFT;
                case Direction.SOUTH_WEST:
                    return Border.RIGHT;
                case Direction.SOUTH:
                    return Border.TOP;
                case Direction.SOUTH_EAST:
                    return Border.LEFT;
                default:
                    return Border.NONE;
            }
        }
        // ======================================
        // Get the opposite direction 
        // ======================================
        protected Direction GetReverseDirection() {
            switch (this.m_direction) {
                case Direction.NORTH_WEST:
                     return Direction.SOUTH_EAST;
                case Direction.NORTH:
                    return Direction.SOUTH;
                case Direction.NORTH_EAST:
                    return Direction.SOUTH_WEST;
                case Direction.WEST:
                    return Direction.EAST;
                case Direction.EAST:
                    return Direction.WEST;
                case Direction.SOUTH_WEST:
                    return Direction.NORTH_EAST;
                case Direction.SOUTH:
                    return Direction.NORTH;
                case Direction.SOUTH_EAST:
                    return Direction.NORTH_WEST;
                default:
                    return Direction.NONE;
            }
        }
        // ======================================
        // Move in the mirrored direction
        // ======================================
        protected Direction GetOppositeDirection(Border hit) {
            switch (this.m_direction)
            {
                case Direction.NORTH_WEST:
                    if(hit == Border.LEFT) {
                        return Direction.NORTH_EAST;
                    } else {
                        return Direction.SOUTH_WEST;
                    }
                case Direction.NORTH:
                    return Direction.SOUTH;
                case Direction.NORTH_EAST:
                    if(hit == Border.RIGHT) {
                        return Direction.NORTH_WEST;
                    } else { 
                        return Direction.SOUTH_EAST;
                    }
                case Direction.WEST:
                    return Direction.EAST;
                case Direction.EAST:
                    return Direction.WEST;
                case Direction.SOUTH_WEST:
                    if(hit == Border.LEFT) {
                        return Direction.SOUTH_EAST;
                    }
                    else { 
                        return Direction.NORTH_WEST;
                    }
                case Direction.SOUTH:
                    return Direction.NORTH;
                case Direction.SOUTH_EAST:
                    if(hit == Border.RIGHT) {
                        return Direction.SOUTH_WEST;
                    }
                    else { 
                        return Direction.NORTH_EAST;
                    }
                default:
                    return Direction.NONE;
            }
        }
        // ======================================
        // Calculate positions
        // ======================================
        protected void NormalizePosition(Coordinate diff) {
            Border border = Border.NONE;
            // ======================================
            // RIGHT Border reached
            // ======================================
            if ((m_position.X + diff.X) == (Screen.MAX_X - 1))
            {
                border = Border.RIGHT;
                if (!m_swap_direction) {
                    m_position.X = 0;
                }
            }
            else
            // ======================================
            // LEFT Border reached
            // ======================================
            if ((m_position.X + diff.X) == -1)
            {
                border = Border.LEFT;
                if (!m_swap_direction) {
                    m_position.X = (Screen.MAX_X - 1);
                }
            }
            else {
                m_position.X += diff.X;
            }
            // ======================================
            // BOTTOM Border reached
            // ======================================
            if ((m_position.Y + diff.Y) == (Screen.MAX_Y - 1))
            {
                border = Border.BOTTOM;
                if (!m_swap_direction) {
                    m_position.Y = 0;
                }
            }
            else
            // ======================================
            // TOP Border reached
            // ======================================
            if ((m_position.Y + diff.Y) == -1) {
                border = Border.TOP;
                if (!m_swap_direction) {
                    m_position.Y = (Screen.MAX_Y - 1);
                }
            }
            else {
                m_position.Y += diff.Y;
            }
            // ======================================
            // Change direction
            // ======================================
            if (m_swap_direction && border != Border.NONE) {
                this.m_direction = GetOppositeDirection(border);
            }
        }
    }
}
