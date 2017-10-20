using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeVD.Lib.Interop;
using static FreeVD.Lib.Interop.User32;

namespace FreeVD.Lib.Hooks
{
    public class WindowEventHook : LocalWindowsHook
    {
        public enum CBT_HookCode : int
        {
            HCBT_MOVESIZE = 0,
            HCBT_MINMAX = 1,
            HCBT_QS = 2,
            HCBT_CREATEWND = 3,
            HCBT_DESTROYWND = 4,
            HCBT_ACTIVATE = 5,
            HCBT_CLICKSKIPPED = 6,
            HCBT_KEYSKIPPED = 7,
            HCBT_SYSCOMMAND = 8,
            HCBT_SETFOCUS = 9
        }

        public delegate void WindowEventHandler(object sender, Window window, IntPtr lParam);

        public event WindowEventHandler WindowCreated;
        public event WindowEventHandler WindowDestroyed;

        public WindowEventHook() : base(HookType.WH_CBT)
        {
            Hooked = (e) =>
            //HookInvoked += (obj,e) =>
            {
                var code = (CBT_HookCode)e.Code;
                switch (code)
                {
                    case CBT_HookCode.HCBT_CREATEWND:
                    case CBT_HookCode.HCBT_DESTROYWND:
                        break;
                    default:
                        // don't care
                        return;
                }

                IntPtr lParam = e.lParam;
                var window = new Window(e.wParam);

                switch (code)
                {
                    case CBT_HookCode.HCBT_CREATEWND:
                        WindowCreated?.Invoke(this, window, lParam);
                        break;
                    case CBT_HookCode.HCBT_DESTROYWND:
                        WindowDestroyed?.Invoke(this, window, lParam);
                        break;
                }

                return;
            };
        }
    }
}
