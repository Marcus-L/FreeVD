using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FreeVD.Lib.Interop
{
    public static class User32
    {
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr FindWindow(string sClassName, string sAppName);

        [DllImport("user32.dll")]
        internal static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern int GetClassName(IntPtr hWnd,
                                                StringBuilder lpClassName,
                                                int nMaxCount);

        [DllImport("user32", SetLastError = true)]
        public static extern int RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32", SetLastError = true)]
        public static extern int UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("USER32.DLL")]
        public static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(SystemMetric smIndex);
    }
}