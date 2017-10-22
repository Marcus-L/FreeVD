using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FreeVD
{
    public class Utils
    {
        public static string AppId = typeof(Utils).GUID.ToString();
        public static Mutex AppMutex;

        public static bool EnsureMinimumOSVersion()
        {
            var ver = Environment.OSVersion;
            if (ver.Platform == PlatformID.Win32NT && 
                ver.Version.Major == 10 && 
                ver.Version.Minor == 0 &&
                ver.Version.Build >= 16299)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Windows 10 Version must be at least 10.0.16299\n(Fall Creators Update)\n\nApplication Exiting.", "Version Check Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static bool EnsureSingleInstance()
        {
            AppMutex = new Mutex(true, AppId, out var isNew);
            if (!isNew)
            {
                using (var pipe = new NamedPipeClientStream(".", AppId, PipeDirection.Out))
                {
                    // signal the running instance
                    try { pipe.Connect(100); } catch { }
                }
            }
            return isNew;
        }

        public static Task ListenForAppStarts(Action restarted)
        {
            return Task.Run(() =>
            {
                using (var stream = new NamedPipeServerStream(AppId, PipeDirection.In))
                {
                    while (true)
                    {
                        try
                        {
                            stream.WaitForConnection();
                            restarted();
                            stream.Disconnect();
                        }
                        catch {}
                    }
                }
            });
        }
    }
}
