
using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using FreeVD.Interop;

namespace FreeVD
{
    public class Hotkey : IDisposable
    {
        private static int GlobalID = 0;
        private static HotkeyWindow Window = new HotkeyWindow();

        public string ID { get; set; }
        public int HotkeyID { get; set; }

        public Hotkey(string id)
        {
            ID = id;
            HotkeyID = Interlocked.Increment(ref GlobalID);
        }

        public Hotkey(string id, uint key, Keys modifiers) : this(id)
        {
            Alt = (modifiers | Keys.Alt) == Keys.Alt;
            Ctrl = (modifiers | Keys.Control) == Keys.Alt;
            Shift = (modifiers | Keys.Shift) == Keys.Shift;
            Win = (modifiers | Keys.LWin) == Keys.LWin || (modifiers | Keys.RWin) == Keys.RWin;
            Key = key;
        }

        public Action<Hotkey> Callback { get; set; } = hk => { /* empty action by default */ };

        public bool IsRegistered { get; private set; }

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

        public bool Register(Keys key, bool alt, bool ctrl, bool shift, bool win)
        {
            if (this.IsRegistered)
            {
                this.Unregister();
            }

            Alt = alt;
            Ctrl = ctrl;
            Shift = shift;
            Win = win;
            Key = (uint)key;

            uint Modifiers = (alt ? Consts.MOD_ALT : 0)
                           + (ctrl ? Consts.MOD_CONTROL : 0)
                           + (shift ? Consts.MOD_SHIFT : 0)
                           + (win ? Consts.MOD_WIN : 0);

            uint keyValue = Convert.ToUInt32(key);

            System.Diagnostics.Debug.WriteLine($"registering {HotkeyID}: {Modifiers} {keyValue}");
            if (User32.RegisterHotKey(Window.Handle, HotkeyID, Modifiers, keyValue) == 0)
            {
                System.Diagnostics.Debug.WriteLine("already registered " + HotkeyID);
                MessageBox.Show(ToString() + Convert.ToString(" hotkey is already registered."));
                return false;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("registered " + HotkeyID);
                Window.Hotkeys[HotkeyID] = this;
                IsRegistered = true;
                return true;
            }
        }

        public void Unregister()
        {
            if (IsRegistered)
            {
                IsRegistered = (User32.UnregisterHotKey(Window.Handle, HotkeyID) == 0);
                if (!IsRegistered)
                {
                    System.Diagnostics.Debug.WriteLine("unregistered " + HotkeyID);
                    Window.Hotkeys.Remove(HotkeyID);
                }
            }
        }

        // To detect redundant calls

        private bool disposedValue;
        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if ((this.IsRegistered == true))
                    {
                        this.Unregister();
                    }
                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void IDisposable_Dispose()
        {
            Dispose(true);
        }

        void IDisposable.Dispose()
        {
            IDisposable_Dispose();
        }
    }
}