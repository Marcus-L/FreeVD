using System;
using System.Runtime.InteropServices;

namespace FreeVD.Lib.Interop
{
    public static class Kernel32
    {
        [DllImport("kernel32", EntryPoint = "GlobalAddAtom", SetLastError = true, ExactSpelling = false)]
        public static extern int GlobalAddAtom([MarshalAs(UnmanagedType.LPTStr)] string lpString);

        //HANDLE WINAPI OpenProcess(
        //  __in  DWORD dwDesiredAccess,
        //  __in  BOOL bInheritHandle,
        //  __in  DWORD dwProcessId
        //);
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);
    }
}