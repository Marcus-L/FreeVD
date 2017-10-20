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

        public static void LoadPins()
        {
            foreach (var window in Window.GetOpenWindows())
            {
                Debug.WriteLine($">> {window.IsPinnedApplication} {window.IsPinnedWindow}");
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
                    Debug.WriteLine($"whaaa {window.IsPinnedApplication}");
                    AppModel.PinnedWindows.Add(window);
                    WatchWindow(window);
                }
            }
        }
    }
}
