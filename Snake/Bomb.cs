using System;
namespace Snake
{
    public class Bomb : GameObject
    {
        // Constructor / Initialize object
        public Bomb(Coordinate position) : base(position) {
            m_isLethal = true;
        }
        // Update state
        // ======================================
        public override void Update() {
            // Call baseclass
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
