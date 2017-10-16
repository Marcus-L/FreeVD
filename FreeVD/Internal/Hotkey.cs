
using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using FreeVD.Interop;

namespace FreeVD
{
    public class Hotkey : IDisposable
    {
        private static void Log(string message)
        {
            File.AppendAllText("log.txt", message + "\n");
        }

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

        public string HotKeyString()
        {
            string keys__1 = "";

            if (Alt)
            {
                if (string.IsNullOrEmpty(keys__1))
                {
                    keys__1 = keys__1 + "ALT";
                }
                else
                {
                    keys__1 = keys__1 + "+ALT";
                }
            }

            if (Ctrl)
            {
                if (string.IsNullOrEmpty(keys__1))
                {
                    keys__1 = keys__1 + "CTRL";
                }
                else
                {
                    keys__1 = keys__1 + "+CTRL";
                }
            }

            if (Shift)
            {
                if (string.IsNullOrEmpty(keys__1))
                {
                    keys__1 = keys__1 + "SHIFT";
                }
                else
                {
                    keys__1 = keys__1 + "+SHIFT";
                }
            }

            if (Win)
            {
                if (string.IsNullOrEmpty(keys__1))
                {
                    keys__1 = keys__1 + "WIN";
                }
                else
                {
                    keys__1 = keys__1 + "+WIN";
                }
            }

            keys__1 = keys__1 + "+" + (Keys)Key;

            return keys__1;
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

            uint Modifiers = 0;
            if (alt)
            {
                Modifiers += Consts.MOD_ALT;
            }
            if (ctrl)
            {
                Modifiers += Consts.MOD_CONTROL;
            }
            if (shift)
            {
                Modifiers += Consts.MOD_SHIFT;
            }
            if (win)
            {
                Modifiers += Consts.MOD_WIN;
            }
            uint keyValue = Convert.ToUInt32(key);


            Log($"registering {HotkeyID}: {Modifiers} {keyValue}");
            if (User32.RegisterHotKey(Window.Handle, HotkeyID, Modifiers, keyValue) == 0)
            {
                MessageBox.Show(HotKeyString() + Convert.ToString(" hotkey is already registered."));
                return false;
            }
            else
            {
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