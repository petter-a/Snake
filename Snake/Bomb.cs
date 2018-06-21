using System;
namespace Snake
{
    public class Bomb : GameObject
    {
        // Constructor / Initialize object
        public Bomb(Coordinate position) : base(position) {
        }
        // Update state
        // ===================================================
        public override void Update() {
            // TODO: Perform logic specific for this objectttype
            // ===================================================
            // Define the behaviour that diverges from the base
            // class implementation. 

            // Call baseclass to perform default logic
            // ===================================================
            // This call could be removed if no default handling
            // is required/needed.
            base.Update();
        }
        // Draw the object
        // ======================================
       public override void Draw(Screen sc) {
            if (m_isActive) {
                sc.DrawAt(m_position, 'Q');
            }
        }
    }
}
