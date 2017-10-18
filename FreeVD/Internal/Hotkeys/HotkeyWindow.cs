using FreeVD.Interop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FreeVD
{
    public class HotkeyWindow : NativeWindow
    {
        public Dictionary<int, Hotkey> Hotkeys = new Dictionary<int, Hotkey>();

        public HotkeyWindow()
        {
            CreateHandle(new CreateParams());
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case Consts.WM_HOTKEY:
                    var wParam = m.WParam.ToInt32();
                    if (Hotkeys.TryGetValue(wParam, out var hotkey))
                    {
                        hotkey.Callback(hotkey);
                        //VirtualDesktopFunctions.HandleHotkey(hotkey);
                    }
                    break;
            }
            base.WndProc(ref m);
        }
    }
}

