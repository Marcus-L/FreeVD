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
            if (!Utils.EnsureMinimumOSVersion() ||
                !Utils.EnsureSingleInstance())
                return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // create default settings
            Settings.EnsureDefaultSettings();

            // run tray icon app
            Application.Run(new TrayContext());
        }
    }
}
