using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace FreeVD
{
    public static class PinWatcher
    {
        public static void WatchWindow(Window window)
        {
            Automation.AddAutomationEventHandler(WindowPattern.WindowClosedEvent, AutomationElement.FromHandle(window.Handle), TreeScope.Element,
                (sender, e) => {
                    AppModel.PinnedWindows.Remove(window);
                });
        }

        public static void Initialize()
        {
            // Watch for windows opening, check if they are pinned applications
            Automation.AddAutomationEventHandler(WindowPattern.WindowOpenedEvent,
              AutomationElement.RootElement, TreeScope.Children,
                async (sender, e) =>
                {
                    var el = sender as AutomationElement;
                    var window = new Window((IntPtr)el.Current.NativeWindowHandle);
                    await Task.Delay(750); // give window some time to fully open
                    ProcessWindow(window);
                });

            foreach (var window in Window.GetOpenWindows())
            {
                ProcessWindow(window);
            }
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
            }
            else if (window.IsPinnedWindow)
            {
                AppModel.PinnedWindows.Add(window);
                WatchWindow(window);
            }
        }
    }
}
