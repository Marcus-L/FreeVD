using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Threading;

namespace FreeVD
{
    static class Program
    {
        public static TrayContext Context;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // create default settings
            EnsureDefaultSettings();

            // watch pinned apps/windows
            PinWatcher.LoadPins();

            Context = new TrayContext();
            Application.Run(Context);
        }

        //private static void LoadPins()
        //{
        //    foreach (var window in Window.GetOpenWindows())
        //    {
        //        if (window.IsPinnedApplication)
        //        {
        //            if (!AppModel.PinnedApps.Contains(window.GetAppId()))
        //            {
        //                AppModel.PinnedApps.Add(window.GetAppId());
        //            }
        //        }
        //        else if (window.IsPinnedWindow)
        //        {
        //            AppModel.PinnedWindows.Add(window);
        //            Automation.AddAutom ationEventHandler(WindowPattern.WindowClosedEvent,
        //                AutomationElement.FromHandle(window.Handle), TreeScope.Subtree,
        //                (sender, e) => {
        //                    AppModel.PinnedWindows.Remove(window);
        //                });
        //        }
        //    }
        //}

        private static void WindowWatcher_WindowDestroyed(object obj, Window window, IntPtr lp)
        {
            AppModel.PinnedWindows.Remove(window);
        }

        private static void EnsureDefaultSettings()
        {
            if (Settings.Default == null)
            {
                Settings.Default = new Settings();
                Settings.Default.Hotkeys.AddRange(VDHotkey.CreateDefaultHotkeys_Numpad());
                Settings.Default.Hotkeys.AddRange(VDHotkey.CreateDefaultHotkeys());
                Settings.Default.Hotkeys.ForEach(hotkey => hotkey.Register());
                Settings.Save();
            }
        }
    }
}
