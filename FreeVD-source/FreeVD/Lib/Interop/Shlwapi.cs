using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FreeVD.Lib.Interop
{
    public static class Shlwapi
    {
        [DllImport("shlwapi.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false, ThrowOnUnmappableChar = true)]
        public static extern int SHLoadIndirectString(string pszSource, StringBuilder pszOutBuf, int cchOutBuf, IntPtr ppvReserved);
    }
}