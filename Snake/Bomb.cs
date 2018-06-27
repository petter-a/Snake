using System;
namespace Snake {
    public class Bomb : GameObject
    {
        int m_frame;
        // Constructor / Initialize object
        // ======================================
        // Note that the Coordinate struct is passed
        // straight away to the base class constructor
        public Bomb(Coordinate position) : base(position) {
            m_direction = Direction.NORTH_WEST;
            m_state = State.MOVING;
            m_swap_direction = true;
            m_frame = 0;
        }
        // Update state
        // ===================================================
        public override bool Update() {
            m_frame = m_frame < 3 ? m_frame + 1 : 0;
            // TODO: Perform logic specific for this objectttype
            // ===================================================
            // Define the behaviour that diverges from the base
            // class implementation. 

            // Call baseclass to perform default logic
            // ===================================================
            // This call could be removed if no default handling
            // is required/needed.
            return base.Update();
        }
        // Draw the object
        // ======================================
       public override void Draw(Screen sc) {
            char graphic = ' ';
            switch(m_frame) {
                case 0:
                    graphic = '|';
                    break;
                case 1:
                    graphic = '/';
                    break;
                case 2:
                    graphic = '-';
                    break;
                case 3:
                    graphic = '\\';
                    break;
            }
            // Add Drawing logic
            // ======================================
            // Draw this object type on the screen
            if (m_state != State.DEAD) {
                sc.DrawAt(m_position, graphic);
            }
        }
        // Handle Collisions
        // ======================================
        // Define behaviour when an object is colliding
        // with another
        public override bool Intersect(GameObject go) {
            if (base.Intersect(go)) {
                // Collide with Anything
                m_direction = GetReverseDirection();
                return true;
            }
            return false;
        }

    }
}
