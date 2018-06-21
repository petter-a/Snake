using System;
namespace Snake
{
    public class Apple : GameObject
    {
        // Constructor / Initialize object
        public Apple(Coordinate position) : base(position) {
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
            if(m_isActive) {
                sc.DrawAt(m_position, '');
            }
        }

    }
}
