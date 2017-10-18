using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsDesktop;

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

            //Add Excluded windows
            ExcludedWindowCaptions.Add("ASUS_Check");
            ExcludedWindowCaptions.Add("NVIDIA GeForce Overlay");
            ExcludedWindowCaptions.Add("FreeVD Settings");

            //Run the main form
            Application.Run(MainForm = new frmMain());
        }

        public static frmMain MainForm;
        public static string version
        {
            get { return typeof(Program).Assembly.GetName().Version.ToString(); }
        }

        public static List<string> WallpaperStyles = new List<string>();
        public static List<string> PinnedApps = new List<string>();
        public static List<Window> windows = new List<Window>();
        public static List<VDHotkey> hotkeys = new List<VDHotkey>();
        public static VirtualDesktop[] Desktops = VirtualDesktop.GetDesktops();
        public static List<string> ExcludedWindowCaptions = new List<string>();

        //stats to log
        public static uint PinCount = 0;
        public static uint MoveCount = 0;
        public static uint NavigateCount = 0;

        public static void AddWindowToList(Window win)
        {
            if (!Program.windows.Any(w => w.Handle == win.Handle))
            {
                windows.Add(win);
            }
        }

        public static bool IsExcludedWindow(string caption)
        {
            foreach(string s in ExcludedWindowCaptions)
            {
                if(caption == s)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
