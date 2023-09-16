using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FreeVD
{
    public static class Log
    {
        public static void LogEvent(string name, string details, Exception ex)
        {
            Debug.WriteLine($"{name}\n{details}\n{ex.Message}\n{ex.StackTrace}");
        }
    }
}
