using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseSimulation.Simulators
{
    public class CursorSimulator
    {
        private double acceleration;
        private double divisor;

        public CursorSimulator()
        {
            this.ActivateEventLooperIncreasement();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [Flags]
        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        private void DoMouseClick()
        {
            mouse_event((uint)(MouseEventFlags.LEFTDOWN | MouseEventFlags.LEFTUP | MouseEventFlags.ABSOLUTE),
                (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, (uint)UIntPtr.Zero);
        }

        private void DoMouseRightClick()
        {
            mouse_event((uint)(MouseEventFlags.RIGHTDOWN | MouseEventFlags.RIGHTUP | MouseEventFlags.ABSOLUTE),
                (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, (uint)UIntPtr.Zero);
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
            int newDistance = this.ConvertDistance((int)distance);
            this.MoveCursorToX(newDistance);
        }

        public void MoveCursorToLeft(uint distance)
        {
            int newDistance = this.ConvertDistance((int)distance);
            this.MoveCursorToX(newDistance * -1);
        }

        public void MoveCursorToBottom(uint distance)
        {
            int newDistance = this.ConvertDistance((int)distance);
            this.MoveCursorToY(newDistance);
        }

        public void MoveCursorToTop(uint distance)
        {
            int newDistance = this.ConvertDistance((int)distance);
            this.MoveCursorToY(newDistance * -1);
        }

        private int ConvertDistance(int distance)
        {
            if (distance < 50)
                return (int)Math.Pow(distance / this.divisor, this.acceleration);

            if (distance < 300)
                return (int)Math.Pow(distance / this.divisor, this.acceleration + 0.1);

            return (int)Math.Pow(distance / this.divisor, this.acceleration + 0.2);
        }

        private void MoveCursorToX(int distance)
        {
            Cursor.Position = new Point(Cursor.Position.X + distance, Cursor.Position.Y);
        }

        private void MoveCursorToY(int distance)
        {
            Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + distance);
        }

        public void ActivateTaskLooperIncreasement()
        {
            //this.acceleration = 0.4;
            //this.acceleration = 1.1;
            //this.divisor = 7;
        }

        public void ActivateEventLooperIncreasement()
        {
            this.acceleration = 1.1;
            this.divisor = 6;
        }
    }
}