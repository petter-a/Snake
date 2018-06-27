using System;
namespace Snake {
    // ======================================
    // Represent the screen
    // ======================================
    // This class encapsulates the logic of 
    // rendering the game
    public class Screen {
        // Constants
        public const int MAX_SIZE_Y = 20;
        public const int MAX_SIZE_X = 80;
        const int scrn_size = MAX_SIZE_X * MAX_SIZE_Y;
        const int view_size = MAX_SIZE_X * MAX_SIZE_Y;
        // 
        char[] m_buffer;

        // Constructor / Initialize object
        public Screen() {
            // ======================================
            // Init Console
            // ======================================
            Console.SetWindowSize(MAX_SIZE_X, MAX_SIZE_Y +1);
            try { // Only works on Windows
                Console.SetBufferSize(MAX_SIZE_X, MAX_SIZE_Y +1);
            } catch(Exception) {};

            // ======================================
            // Allocate screen buffer
            // ======================================
            m_buffer =
                new char[MAX_SIZE_X * MAX_SIZE_Y];
            // ======================================
            // Clear Buffer
            // ======================================
            ClearBuffer();
        }
        // ======================================
        // Clear the drawing buffer
        // ======================================
        public void ClearBuffer() {
            for (int i = 0; i < m_buffer.Length; i++) {
                // ======================================
                // Fill Buffer with spaces
                // ======================================
                m_buffer[i] = '·';
            }
        }
        // ======================================
        // Draw on buffer
        // ======================================
        public void DrawAt(Coordinate position, char data) {
            // ======================================
            // Draw at position
            // ======================================
            m_buffer[position.Y * Screen.MAX_SIZE_X 
                     + position.X] = data;
        }
        // ======================================
        // Swap buffer to screen
        // ======================================
        public void Render() {
            // ======================================
            // Restore cursor position
            // ======================================
            Console.SetCursorPosition(
                0, 0);

            // ======================================
            // Render buffer on screen
            // ======================================
            for (int i = 0; i < m_buffer.Length; i++) {
                Console.Write(m_buffer[i]);
            }
        }

    }

}