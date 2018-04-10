using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CarPhysicsTester
{
    /// <summary>
    /// Клавиатура
    /// </summary>
    public static class Keyboard
    {
        public static HashSet<Keys> pressed = new HashSet<Keys>();

        [Flags]
        private enum KeyStates
        {
            None = 0,
            Down = 1,
            Toggled = 2
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        private static KeyStates GetKeyState(Keys key)
        {
            KeyStates state = KeyStates.None;

            short retVal = GetKeyState((int)key);

            if ((retVal & 0x8000) == 0x8000)
                state |= KeyStates.Down;

            if ((retVal & 1) == 1)
                state |= KeyStates.Toggled;

            return state;
        }

        public static bool IsKeyDown(Keys key)
        {
            return KeyStates.Down == (GetKeyState(key) & KeyStates.Down);
        }

        public static bool IsKeyToggled(Keys key)
        {
            if (pressed.Contains(key))
            {
                if (!IsKeyDown(key))
                    pressed.Remove(key);
            }
            else
            {
                if (IsKeyDown(key))
                {
                    pressed.Add(key);
                    return true;
                }
            }

            return false;
        }
    }
}
