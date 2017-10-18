using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsDesktop;

namespace FreeVD
{
    public static class VirtualDesktopFunctions
    {
        public static void PinWindow()
        {
            try
            {
                Window win = Window.ForegroundWindow();
                IEnumerable<Window> window = from Window w in Program.windows
                                             where w.Handle == win.Handle
                                             select w;
                if (window.Count() < 1)
                {
                    Program.windows.Add(win);
                }

                if (win.IsPinnedWindow)
                {
                    win.Unpin();
                }
                else
                {
                    win.Pin();
                }

                Program.MainForm.SetPinnedAppListBox();
           
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured pinning or unpinning the specified window. See additional details below." + Environment.NewLine + Environment.NewLine +
                    ex.Message + Environment.NewLine +
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }

        }

        public static void PinApp()
        {
            try
            {
                Window win = Window.ForegroundWindow();
                IEnumerable<Window> window = from Window w in Program.windows
                                             where w.Handle == win.Handle
                                             select w;
                if (window.Count() < 1)
                {
                    Program.windows.Add(win);
                }

                if (win.IsPinnedApplication)
                {
                    win.UnpinApplication();
                    Program.PinnedApps.Remove(win.AppID);
                }
                else
                {
                    win.PinApplication();
                    Program.PinnedApps.Add(win.AppID);
                }

                Program.MainForm.SetPinnedAppListBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured pinning or unpinning the specified application. See additional details below." + Environment.NewLine + Environment.NewLine +
                    ex.Message + Environment.NewLine +
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }

        }

        public static void DesktopMove(int desktopNumber, bool follow)
        {

            Window win = Window.ForegroundWindow();
            IEnumerable<Window> window = from Window w in Program.windows
                                         where w.Handle == win.Handle
                                         select w;
            if (window.Count() < 1)
            {
                Program.windows.Add(win);
            }

            win.MoveToDesktop(desktopNumber, follow);
        }

        public static void DesktopMoveNext(bool follow)
        {

            Window win = Window.ForegroundWindow();
            IEnumerable<Window> window = from Window w in Program.windows
                                         where w.Handle == win.Handle
                                         select w;
            if (window.Count() < 1)
            {
                Program.windows.Add(win);
            }

            win.MoveToNextDesktop(follow);
        }

        public static void DesktopMovePrevious(bool follow)
        {

            Window win = Window.ForegroundWindow();
            IEnumerable<Window> window = from Window w in Program.windows
                                         where w.Handle == win.Handle
                                         select w;
            if (window.Count() < 1)
            {
                Program.windows.Add(win);
            }

            win.MoveToPreviousDesktop(follow);
        }

        public static int GetDesktopNumber(Guid Guid)
        {
            try
            {
                Program.Desktops = VirtualDesktop.GetDesktops();
                for (int i = 0; i <= Program.Desktops.Count() - 1; i++)
                {
                    if (Program.Desktops[i].Id == Guid)
                    {
                        return i + 1;
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured identifying the desktop number. See additional details below." + Environment.NewLine + Environment.NewLine +
                    ex.Message + Environment.NewLine +
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "", "", "frmMain", ex);
                return 1;

            }

        }

        public static void GoToDesktop(int desktopNumber)
        {
            try
            {
                VirtualDesktop current = VirtualDesktop.Current;
                int i = GetDesktopNumber(current.Id);
                if (i == desktopNumber)
                {
                    return;
                }
                else
                {
                    int diff = Math.Abs(i - desktopNumber);
                    if (i < desktopNumber)
                    {
                        for (int z = 1; z <= diff; z++)
                        {
                            current = current.GetRight();
                        }
                    }
                    else
                    {
                        for (int z = 1; z <= diff; z++)
                        {
                            current = current.GetLeft();
                        }
                    }

                    //Right beofre switching the desktop, set the active window as the taskbar
                    //This prevents windows from flashing in the taskbar when switching desktops
                    Window w = Window.Taskbar();
                    w.SetAsForegroundWindow();
                    current.Switch();
                    Program.NavigateCount++;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured navigating to the specified desktop. See additional details below." + Environment.NewLine + Environment.NewLine +
                    ex.Message + Environment.NewLine +
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }
        }

        public static int GetCurrentDesktopNumber()
        {
            try
            {
                return GetDesktopNumber(VirtualDesktop.Current.Id);
            }
            catch
            {
                return 1;
            }
        }

    }
}
