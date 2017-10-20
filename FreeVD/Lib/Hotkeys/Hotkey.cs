using FreeVD.Lib.Interop;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace FreeVD.Lib.Hotkeys
{
    public class Hotkey : IDisposable
    {
        private static int GlobalID = 0;
        private static HotkeyWindow Window = new HotkeyWindow();

        public int HotkeyID { get; set; }

        public Hotkey()
        {
            HotkeyID = Interlocked.Increment(ref GlobalID);
        }

        public Hotkey(uint key, Keys modifiers) : this()
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

        public override string ToString()
        {
            string keys = (Ctrl ? "Ctrl+" : "") +
                          (Alt ? "Alt+" : "") +
                          (Shift ? "Shift+" : "") +
                          (Win ? "Win+" : "");

            return keys + (Keys)Key;
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