using FreeVD.Lib.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeVD
{
    public static class PinWatcher
    {
        private static SystemProcessHookForm systemProcessHookForm;

        public static void Initialize()
        {
            // Watch for windows opening, check if they are pinned applications
            systemProcessHookForm = new SystemProcessHookForm();
            systemProcessHookForm.WindowCreatedEvent += (sender, hWnd) =>
            {
                if(hWnd != IntPtr.Zero)
                    ProcessWindow(new Window(hWnd));
            };

            systemProcessHookForm.WindowDestroyedEvent += (sender, hWnd) =>
            {
                if (hWnd != IntPtr.Zero)
                {
                    var window = new Window(hWnd);
                    AppModel.PinnedWindows.Remove(window);
                    AppModel.RemoveWindow(window);
                }
            };


            foreach (var window in Window.GetOpenWindows())
                ProcessWindow(window);
        }

        private static void ProcessWindow(Window window)
        {
            if (window.IsPinnedApplication)
            {
                var id = window.GetAppId();
                if (!AppModel.PinnedApps.Any(a => a.Id == id))
                {
                    AppModel.PinnedApps.Add(AppInfo.FromWindow(window));
                }
                if(!AppModel.PinnedWindows.Contains(window))
                    AppModel.PinnedWindows.Add(window);
            }
            else if (window.IsPinnedWindow)
            {
                if (!AppModel.PinnedWindows.Contains(window))
                    AppModel.PinnedWindows.Add(window);
            }
        }
    }
}
