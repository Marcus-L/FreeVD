using FreeVD.Lib.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WindowsDesktop;
using WindowsDesktop.Interop;

namespace FreeVD
{
    public class Window : IEquatable<Window>
    {
        static Window()
        {
            Debug.WriteLine("windowww");
        }

        public static IEnumerable<Window> GetOpenWindows()
        {
            IntPtr shellWindow = User32.GetShellWindow();
            var windows = new List<Window>();
            User32.EnumWindows((handle,lParam) =>
            {
                if (handle != shellWindow &&
                    User32.IsWindowVisible(handle) &&
                    User32.GetWindowTextLength(handle) > 0)
                {
                    windows.Add(new Window(handle));
                }
                return true;
            },
            IntPtr.Zero);
            return windows;
        }

        public Window(IntPtr handle)
        {
            Handle = handle;
        }

        private static readonly List<string> ExcludedWindowText = new List<string>()
        {
            "ASUS_Check", "NVIDIA GeForce Overlay", "FreeVD Settings"
        };

        public IntPtr Handle { get; set; }

        public string GetAppId()
        {
            try
            {
                return ApplicationHelper.GetAppId(Handle);
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        public static Window GetForegroundWindow() => new Window(User32.GetForegroundWindow());

        public static Window GetTaskbar() => new Window(User32.FindWindow("Shell_TrayWnd", null));

        public int DesktopNumber
        {
            get
            {
                try
                {
                    if (!ExcludedWindowText.Contains(GetWindowText()) &&
                        ComObjects.GetVirtualDesktopManager().GetWindowDesktopId(Handle) != Guid.Empty)
                    {
                        return (IsPinnedWindow ? VirtualDesktop.Current : VirtualDesktop.FromHwnd(Handle)).GetNumber();
                    }
                }
                catch (Exception ex) when (ex.HResult == Consts.TYPE_E_ELEMENTNOTFOUND) {}
                catch (Exception ex)
                {
                    Log.LogEvent("Window", $"Handle: {Handle}\nCaption: {GetWindowText()}", ex);
                }
                return -1;
            }
        }

        public bool IsDesktop
        {
            get
            {
                switch (GetClassName())
                {
                    case "Progman":
                    case "WorkerW":
                    case "Shell_TrayWnd":
                    case "Shell_SecondaryTrayWnd":
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsPinnedWindow
        {
            get
            {
                try
                {
                    return Handle == IntPtr.Zero ? false : VirtualDesktop.IsPinnedWindow(Handle);
                }
                catch (Exception ex)
                {
                    Log.LogEvent("Window", $"Handle: {Handle}\nCaption: {GetWindowText()}", ex);
                    return false;
                }
            }
        }

        public bool IsPinnedApplication
        {
            get
            {
                try
                {
                    return Handle == IntPtr.Zero ? false : VirtualDesktop.IsPinnedApplication(GetAppId());
                }
                catch (Exception ex)
                {
                    Log.LogEvent("Window", $"Handle: {Handle}\nCaption: {GetWindowText()}", ex);
                    return false;
                }
            }
        }

        public string GetClassName()
        {
            int max = 512;
            var sb = new StringBuilder(max);
            return User32.GetClassName(Handle, sb, max) > 0 ? sb.ToString() : "ERR";
        }

        public Window MoveToPreviousDesktop()
        {
            var desktops = VirtualDesktop.GetDesktops();
            if (DesktopNumber == -1 || desktops.Count() == 1) return this;
            int prevDesktop = DesktopNumber == 1 ? desktops.Count() : DesktopNumber - 1;
            return MoveToDesktop(prevDesktop);
        }

        public Window MoveToNextDesktop()
        {
            var desktops = VirtualDesktop.GetDesktops();
            if (DesktopNumber == -1 || desktops.Count() == 1) return this;
            int nextDesktop = DesktopNumber == desktops.Count() ? 1 : DesktopNumber + 1;
            return MoveToDesktop(nextDesktop);
        }

        public Window MoveToDesktop(VirtualDesktop desktop)
        {
            if (DesktopNumber != -1)
            {
                AppModel.PinnedWindows.Remove(this);
                AppModel.PinnedApps.Remove(AppInfo.FromWindow(this));
                VirtualDesktopHelper.MoveToDesktop(Handle, desktop);
            }
            return this;
        }

        public Window MoveToDesktop(int desktopNumber)
        {
            // do not move popups (which exception out anyway) or explicitly excluded windows
            if (DesktopNumber != -1 && desktopNumber > 0)
            {
                try
                {
                    EnsureDesktops(desktopNumber); // Create addtional desktops if necessary
                    MoveToDesktop(VirtualDesktop.GetDesktops()[desktopNumber - 1]);
                }
                catch (Exception ex)
                {
                    Log.LogEvent("Window", $"Handle: {Handle}\nCaption: {GetWindowText()}", ex);
                }
            }
            return this;
        }

        public static void EnsureDesktops(int desktopCount)
        {
            int createNew = desktopCount - VirtualDesktop.GetDesktops().Count();
            while (createNew-- > 0) VirtualDesktop.Create();
        }

        public void Follow(bool follow = false)
        {
            if (follow && DesktopNumber != -1) // do not follow immovable windows
            {
                //User32.SetForegroundWindow(Window.GetTaskbar().Handle);
                VirtualDesktop.FromHwnd(Handle).Switch();
                User32.SetForegroundWindow(Handle);
            }
        }

        public void TogglePinWindow()
        {
            try
            {
                if (IsPinnedApplication) return; // don't pin a window of an already pinned app
                if (!IsPinnedWindow)
                {
                    VirtualDesktop.PinWindow(Handle);
                    PinWatcher.WatchWindow(this);
                    AppModel.PinnedWindows.Add(this);
                }
                else
                {
                    VirtualDesktop.UnpinWindow(Handle);
                    AppModel.PinnedWindows.Remove(this);
                }
            }
            catch (Exception ex)
            {
                Log.LogEvent("Window", $"Handle: {Handle}\nCaption: {GetWindowText()}", ex);
            }
        }

        public void TogglePinApp()
        {
            try
            {
                var info = AppInfo.FromWindow(this);
                if (IsPinnedApplication)
                {
                    VirtualDesktop.UnpinApplication(info.Id);
                    AppModel.PinnedApps.Remove(info);
                }
                else
                {
                    AppModel.PinnedWindows.Remove(this);
                    VirtualDesktop.PinApplication(info.Id);
                    AppModel.PinnedApps.Add(info);
                }
            }
            catch (Exception ex)
            {
                Log.LogEvent("Window", $"Handle: {Handle}\nCaption: {GetWindowText()}", ex);
            }
        }

        public string GetWindowText()
        {
            try
            {
                var text = new StringBuilder(User32.GetWindowTextLength(Handle) + 1);
                User32.GetWindowText(Handle, text, text.Capacity);
                return text.ToString();
            }
            catch (Exception ex)
            {
                Log.LogEvent("Window", $"Handle: {Handle}\nCaption: {GetWindowText()}", ex);
                return "";
            }
        }

        public string GetWindowName()
        {
            try
            {
                User32.GetWindowThreadProcessId(Handle, out var lpdwProcessId);
                var handle = Kernel32.OpenProcess(0x0410, false, lpdwProcessId);
                var text = new StringBuilder(1000);
                Psapi.GetModuleFileNameEx(handle, IntPtr.Zero, text, text.Capacity);
                Kernel32.CloseHandle(handle);
                return text.ToString();
            }
            catch (Exception ex)
            {
                Log.LogEvent("Window", $"Handle: {Handle}\nCaption: {GetWindowText()}", ex);
                return "";
            }
        }

        public Process GetProcess()
        {
            try
            {
                User32.GetWindowThreadProcessId(Handle, out var lpdwProcessId);
                return Process.GetProcessById((int)lpdwProcessId);
            }
            catch (Exception ex)
            {
                Log.LogEvent("Window", $"Handle: {Handle}\nCaption: {GetWindowText()}", ex);
                return null;
            }
        }

        // equality overrides, IEquatable<Window>
        public override bool Equals(object o) => o is Window ? Equals((Window)o) : base.Equals(o);
        public bool Equals(Window other) => Handle == other.Handle;
        public override int GetHashCode() => Handle.ToInt32().GetHashCode();
    }
}
