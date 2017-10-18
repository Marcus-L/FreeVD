using System;
using System.Runtime.InteropServices;
using System.Text;

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

    public static class Psapi
    {
        //  DWORD WINAPI GetModuleBaseName(
        //      __in      HANDLE hProcess,
        //      __in_opt  HMODULE hModule,
        //      __out     LPTSTR lpBaseName,
        //      __in      DWORD nSize
        //  );
        [DllImport("psapi.dll")]
        public static extern uint GetModuleBaseName(IntPtr hWnd, IntPtr hModule, StringBuilder lpFileName, int nSize);

        //  DWORD WINAPI GetModuleFileNameEx(
        //      __in      HANDLE hProcess,
        //      __in_opt  HMODULE hModule,
        //      __out     LPTSTR lpFilename,
        //      __in      DWORD nSize
        //  );
        [DllImport("psapi.dll")]
        public static extern uint GetModuleFileNameEx(IntPtr hWnd, IntPtr hModule, StringBuilder lpFileName, int nSize);
    }
}