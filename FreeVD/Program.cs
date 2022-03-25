using FreeVD.Lib.Hotkeys;
using FreeVD.Lib.Interop;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace FreeVD
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // create default settings
            Settings.EnsureDefaultSettings();

            WinTabKeyboardHook.Initialize();

            // run tray icon app
            Application.Run(new TrayContext());
        }
    }
}
