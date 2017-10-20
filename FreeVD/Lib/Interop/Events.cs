using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeVD.Lib.Interop
{
    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

    public class HookEventArgs : EventArgs
    {
        public int Code;
        public IntPtr wParam;
        public IntPtr lParam;
    }
}
