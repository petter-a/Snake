using System;
namespace Snake {
    public class Apple : GameObject
    {
        // Constructor / Initialize object
        // ======================================
        // Note that the Coordinate struct is passed
        // straight away to the base class constructor
        public Apple(Coordinate position) : base(position) {
        }
        // Update state
        // ======================================
        public override bool Update() {
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
            // Add Drawing logic
            // ======================================
            // Draw this object type on the screen
            if(m_state != State.DEAD) {
                sc.DrawAt(m_position, 'A');
            }
        }
        // Handle Collisions
        // ======================================
        // Define behaviour when an object is colliding
        // with another
        public override bool Intersect(GameObject go) {
            if (base.Intersect(go)) {
                // Remove
                if (go.GetType() == typeof(Snake)) {
                    m_state = State.DEAD;
                    return true;
                }
            }
            return false;
        }
    }
}
