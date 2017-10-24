using FreeVD.Lib.Interop;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FreeVD.Lib.Hotkeys
{
    public class Hotkey : IDisposable
    {
        public static int MaxID = 0;
        private static HotkeyWindow Window = new HotkeyWindow();

        private int HotkeyID_ = 0;
        public int HotkeyID {
            get { return HotkeyID_; }
            set {
                HotkeyID_ = value;
                if (MaxID < value) MaxID = value;
            }
        }

        public Hotkey()
        {
            HotkeyID = Interlocked.Increment(ref MaxID);
        }

        public Hotkey(uint key, Keys modifiers) : this()
        {
            SetKeys(key, modifiers);
        }

        public void SetKeys(uint key, Keys modifiers)
        {
            Alt = (modifiers & Keys.Alt) == Keys.Alt;
            Ctrl = (modifiers & Keys.Control) == Keys.Control;
            Shift = (modifiers & Keys.Shift) == Keys.Shift;
            Win = (modifiers & Keys.LWin) == Keys.LWin || (modifiers & Keys.RWin) == Keys.RWin;
            Key = key;
        }

        [JsonIgnore]
        public Action Callback { get; set; } = () => { /* empty action by default */ };

        [JsonIgnore]
        public bool Registered { get; private set; }

        public bool Alt { get; set; }

        public bool Ctrl { get; set; }

        public bool Shift { get; set; }

        public bool Win { get; set; }

        public uint Key { get; set; }

        public string KeyCodeToString(Keys key)
        {
            if ((key >= Keys.D0 && key <= Keys.D9) ||
                (key >= Keys.A && key <= Keys.Z) ||
                (key >= Keys.F1 && key <= Keys.F12) ||
                (key >= Keys.NumPad0 && key <= Keys.NumPad9))
            {
                return key.ToString();
            }
            if (key == Keys.Next) return "PageDown";

            uint virtualKeyCode = (uint)key;
            uint scanCode = User32.MapVirtualKey(virtualKeyCode, 0);
            IntPtr inputLocaleIdentifier = User32.GetKeyboardLayout(0);

            var result = new StringBuilder();
            User32.ToUnicodeEx(virtualKeyCode, scanCode, new byte[255], result, (int)5, (uint)0, inputLocaleIdentifier);

            string retval = result.ToString();
            if (retval == "") return key.ToString();
            return retval;
        }


        public override string ToString()
        {
            string keys = (Ctrl ? "Ctrl+" : "") +
                          (Alt ? "Alt+" : "") +
                          (Shift ? "Shift+" : "") +
                          (Win ? "Win+" : "");

            return keys + (Key == 0 ? "" : KeyCodeToString((Keys)Key));
        }

        public bool Register()
        {
            uint Modifiers = (Alt ? Consts.MOD_ALT : 0)
                           + (Ctrl ? Consts.MOD_CONTROL : 0)
                           + (Shift ? Consts.MOD_SHIFT : 0)
                           + (Win ? Consts.MOD_WIN : 0);

            if (User32.RegisterHotKey(Window.Handle, HotkeyID, Modifiers, Key) == 0)
            {
                Debug.WriteLine($"error registering {HotkeyID}: {Modifiers} {Key}");
                return false;
            }
            else
            {
                Window.Hotkeys[HotkeyID] = this;
                Registered = true;
                return true;
            }
        }

        public void Unregister()
        {
            if (Registered)
            {
                Registered = (User32.UnregisterHotKey(Window.Handle, HotkeyID) == 0);
                if (!Registered)
                {
                    Window.Hotkeys.Remove(HotkeyID);
                }
                else
                {
                    Debug.WriteLine($"error unregistering {HotkeyID}");
                }
            }
        }

        public void Dispose()
        {
            Unregister();
        }
    }
}