using FreeVD.Lib.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsDesktop;

namespace FreeVD
{
    public static class AppModel
    {
        public static string Version
        {
            get { return typeof(Program).Assembly.GetName().Version.ToString(); }
        }

        public static IntPtr CurrentWindowInFocus = IntPtr.Zero;
        public static Dictionary<Guid, IntPtr> FocusedWindows = new Dictionary<Guid, IntPtr>();
        public static ObservableCollection<AppInfo> PinnedApps = new ObservableCollection<AppInfo>();
        public static ObservableCollection<Window> PinnedWindows = new ObservableCollection<Window>();
        public static ObservableCollection<Window> CopiedWindows = new ObservableCollection<Window>();

        public static void Initialize()
        {
            FocusedWindows.Clear();
            GetAndSaveWindowInFocus();
        }

        public static void LoadCopiedWindows()
        {
            foreach (Window w in CopiedWindows)
                w.LoadCopyStateForCurrentDesktop();
        }

        public static void LoadPinnedWindowsPos()
        {
            foreach (Window w in PinnedWindows.Concat(CopiedWindows))
                w.LoadWindowStateForCurrentDesktop();
        }

        public static void SavePinnedWindowsPos()
        {
            foreach (Window w in PinnedWindows.Concat(CopiedWindows))
                w.SaveWindowStateForCurrentDesktop();
        }

        public static void RemoveDesktop(Guid guid)
        {
            foreach (Window w in PinnedWindows)
                if (w.WindowPlacements.ContainsKey(guid))
                    w.WindowPlacements.Remove(guid);
            FocusedWindows.Remove(guid);
            foreach (Window w in CopiedWindows)
                if (w.WindowVisibleDesktops.Contains(guid))
                    w.WindowVisibleDesktops.Remove(guid);
        }

        public static void RemoveWindow(Window w)
        {
            Guid[] keys = FocusedWindows.Keys.ToArray();
            for (int i = 0; i < FocusedWindows.Count; ++i)
            {
                if (FocusedWindows[keys[i]] == w.Handle)
                    FocusedWindows.Remove(keys[i]);
            }
            CopiedWindows.Remove(w);
        }

        public static void GetAndSaveWindowInFocus()
        {
            CurrentWindowInFocus = User32.GetForegroundWindow();
            SaveWindowInFocus();
        }

        public static void SaveWindowInFocus()
        {
            if (CurrentWindowInFocus != IntPtr.Zero)
                SetWindowsInFocusOnDesktop(CurrentWindowInFocus, VirtualDesktop.Current.Id);
        }

        public static void SetWindowsInFocusOnDesktop(IntPtr window, Guid desktopId)
        {
            if (FocusedWindows.ContainsKey(desktopId))
                FocusedWindows[desktopId] = window;
            else
                FocusedWindows.Add(desktopId, window);
        }

        public static void LoadWindowInFocus()
        {
            IntPtr wnd;
            if (FocusedWindows.TryGetValue(VirtualDesktop.Current.Id, out wnd))
            {
                Window w = new Window(wnd);
                if (VirtualDesktop.Current.Contains(w))
                    User32.SetForegroundWindow(wnd);
                else FocusedWindows.Remove(VirtualDesktop.Current.Id);
            }
                
        }
    }
}
