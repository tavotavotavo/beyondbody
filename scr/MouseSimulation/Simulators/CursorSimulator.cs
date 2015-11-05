using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseSimulation.Simulators
{
    public class CursorSimulator
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;

        private void DoMouseClick()
        {
            //Call the imported function with the cursor's current position
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        private void DoMouseRightClick()
        {
            //Call the imported function with the cursor's current position
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_RIGHTDOWN, X, Y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
        }

        public void SimulateClick()
        {
            this.DoMouseClick();
        }

        public void SimulateDoubleClick()
        {
            this.DoMouseClick();
            this.DoMouseClick();
        }

        public void SimulateRightClick()
        {
            this.DoMouseRightClick();
        }

        public void MoveCursorToRight(uint distance)
        {
            this.MoveCursorToX((int)distance);
        }

        public void MoveCursorToLeft(uint distance)
        {
            this.MoveCursorToX((int)distance * -1);
        }

        public void MoveCursorToBottom(uint distance)
        {
            this.MoveCursorToY((int)distance);
        }

        public void MoveCursorToTop(uint distance)
        {
            this.MoveCursorToY((int)distance * -1);
        }

        private void MoveCursorToX(int distance)
        {
            Cursor.Position = new Point(Cursor.Position.X + distance, Cursor.Position.Y);
        }

        private void MoveCursorToY(int distance)
        {
            Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + distance);
        }
    }
}