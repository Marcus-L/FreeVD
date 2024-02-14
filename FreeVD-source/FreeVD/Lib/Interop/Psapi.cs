using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FreeVD.Lib.Interop
{
    public static class Psapi
    {
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