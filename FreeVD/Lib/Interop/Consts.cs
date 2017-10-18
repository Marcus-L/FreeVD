using System;

namespace FreeVD.Lib.Interop
{
    public static class Consts
    {
        public const uint MOD_ALT = 0x1;
        public const uint MOD_CONTROL = 0x2;
        public const uint MOD_SHIFT = 0x4;
        public const uint MOD_WIN = 0x8;
        public const int WM_HOTKEY = 0x312;

        public const bool EnumWindows_ContinueEnumerating = true;
        public const bool EnumWindows_StopEnumerating = false;
    }
}