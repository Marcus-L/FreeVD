using FreeVD.Lib.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsDesktop;
using WindowsDesktop.Internal;

namespace FreeVD
{
    public class Window
    {
        public static IDictionary<IntPtr, string> GetOpenWindows()
        {
            IntPtr shellWindow = User32.GetShellWindow();
            Dictionary<IntPtr, string> windows = new Dictionary<IntPtr, string>();

            User32.EnumWindows(delegate (IntPtr hWnd, IntPtr lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!User32.IsWindowVisible(hWnd)) return true;

                int length = User32.GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                User32.GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;

            }, IntPtr.Zero);

            return windows;
        }

        //Create a new instance from the current foreground window
        public static Window ForegroundWindow()
        {
            try
            {
                Window win = new Window(User32.GetForegroundWindow());
                return win;
            }catch
            {
                return null;
            }
            
        }

        //Create a new instance from the taskbar window
        public static Window Taskbar()
        {
            try
            {
                IntPtr hWnd = User32.FindWindow("Shell_TrayWnd", null);
                Window win = new Window(hWnd);
                return win;
            }
            catch
            {
                return null;
            }
        }

        public Window(IntPtr hWnd)
        {
            this.Handle = hWnd;
        }

        public IntPtr Handle { get; set; }

        public string Caption => GetWindowText();

        public string ApplicationName => GetWindowName();

        public int DesktopNumber
        {
            get
            {
                try
                {
                    if (Program.IsExcludedWindow(this.Caption))
                    {
                        return 1;
                    }
                    return GetDesktopNumber(VirtualDesktop.FromHwnd(Handle).Id);
                }catch(Exception ex)
                {
                    Log.LogEvent("Exception", 
                                 "Handle: " + Handle.ToString() + 
                                 "\r\nCaption: " + this.Caption, 
                                 "", 
                                 "Window", ex);
                    return 1;
                }
                
            }
        }

        public bool IsDesktop
        {
            get
            {
                try
                {
                    const int maxChars = 256;
                    StringBuilder className = new StringBuilder(maxChars);
                    if (User32.GetClassName(this.Handle, className, maxChars) > 0)
                    {
                        string cName = className.ToString();
                        if (cName == "Progman" || cName == "WorkerW")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }


                }
                catch (Exception ex)
                {
                    Log.LogEvent("Exception", "", "", "Window", ex);
                    return false;
                }

            }
        }

        public bool SetAsForegroundWindow()
        {
            try
            {
                return User32.SetForegroundWindow(this.Handle);
            }
            catch (Exception ex)
            {
                Log.LogEvent("Exception", "", "", "Window", ex);
                return false;
            }
        }

        public string AppID
        {
            get
            {
                try
                {
                    return ApplicationHelper.GetAppId(Handle);
                }
                catch (Exception ex)
                {
                    Log.LogEvent("Exception", "", "", "Window", ex);
                    return "";
                }
            }
        }

        public bool IsPinnedWindow
        {
            get
            {
                try
                {
                    if(Handle != IntPtr.Zero)
                    {
                        return VirtualDesktop.IsPinnedWindow(Handle);
                    }else
                    {
                        return false;
                    }
                    
                }
                catch (Exception ex)
                {
                    Log.LogEvent("Exception", "", "", "Window", ex);
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
                    if (Handle != IntPtr.Zero)
                    {
                        return VirtualDesktop.IsPinnedApplication(AppID);
                    }
                    else
                    {
                        return false;
                    }                    
                }
                catch (Exception ex)
                {
                    Log.LogEvent("Exception", "", "", "Window", ex);
                    return false;
                }

            }
        }

        public System.Diagnostics.Process Process
        {
            get
            {
                try
                {
                    return System.Diagnostics.Process.GetProcessById((int)GetProcessID());
                }catch(Exception ex)
                {
                    Log.LogEvent("Exception", "", "", "Window", ex);
                    return null;
                }
                
            }
        }

        public void Unpin()
        {
            try
            {
                VirtualDesktop.UnpinWindow(Handle);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured unpinning the specified window. See additional details below." + Environment.NewLine + Environment.NewLine +
                    ex.Message + Environment.NewLine +
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "",
                             "Window Handle: " + this.Handle.ToString() + Environment.NewLine +
                             "Window Caption: " + this.Caption + Environment.NewLine +
                             "Application: " + this.ApplicationName,
                             "Window",
                             ex);
            }
        } 

        public void UnpinApplication()
        {
            try
            {
                VirtualDesktop.UnpinApplication(AppID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured unpinning the specified application. See additional details below." + Environment.NewLine + Environment.NewLine +
                    ex.Message + Environment.NewLine +
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "",
                             "Window Handle: " + this.Handle.ToString() + Environment.NewLine +
                             "Window Caption: " + this.Caption + Environment.NewLine +
                             "Application: " + this.ApplicationName,
                             "Window",
                             ex);

            }
        }

        public void Pin()
        {
            try
            {
                VirtualDesktop.PinWindow(Handle);
                Program.PinCount++;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured pinning the specified window. See additional details below." + Environment.NewLine + Environment.NewLine +
                    ex.Message + Environment.NewLine +
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "",
                             "Window Handle: " + this.Handle.ToString() + Environment.NewLine +
                             "Window Caption: " + this.Caption + Environment.NewLine +
                             "Application: " + this.ApplicationName,
                             "Window",
                             ex);
            }
        }

        public void PinApplication()
        {
            try
            {
                VirtualDesktop.PinApplication(AppID);
                Program.PinCount++;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured pinning the specified application. See additional details below." + Environment.NewLine + Environment.NewLine +
                    ex.Message + Environment.NewLine +
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "",
                             "Window Handle: " + this.Handle.ToString() + Environment.NewLine +
                             "Window Caption: " + this.Caption + Environment.NewLine +
                             "Application: " + this.ApplicationName,
                             "Window",
                             ex);
            }
        }

        public void MoveToPreviousDesktop()
        {
            if (this.DesktopNumber == 1)
            {
                MoveToDesktop(VirtualDesktop.GetDesktops().Count());
            }
            else
            {
                MoveToDesktop(this.DesktopNumber - 1);
            }
        }

        public void MoveToPreviousDesktop(bool follow)
        {
            MoveToPreviousDesktop();
            if (follow) GoToDesktop();
        }

        public void MoveToNextDesktop()
        {
            if (this.DesktopNumber == VirtualDesktop.GetDesktops().Count())
            {
                MoveToDesktop(1);
            }
            else
            {
                MoveToDesktop(this.DesktopNumber + 1);
            }
            
        }

        public void MoveToNextDesktop(bool follow)
        {
            MoveToNextDesktop();
            if (follow) GoToDesktop();
        }

        public void MoveToDesktop(int desktopNumber)
        {
            try
            {
                if(Program.IsExcludedWindow(this.Caption))
                {
                    return;
                }
                //Create addtional desktops if necessary
                VirtualDesktop[] Desktops = VirtualDesktop.GetDesktops();
                if (Desktops.Count() < desktopNumber)
                {
                    int diff = Math.Abs(Desktops.Count() - desktopNumber);
                    for (int x = 1; x <= diff; x++)
                    {
                        VirtualDesktop.Create();
                    }
                }             

                VirtualDesktop current = VirtualDesktop.Current;

                int i = GetDesktopNumber(current.Id);
                if (i == desktopNumber)
                {
                    VirtualDesktopHelper.MoveToDesktop(Handle, current);
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
                    VirtualDesktopHelper.MoveToDesktop(Handle, current);
                    Program.MoveCount++;
                }

            }
            catch (Exception ex)
            {
                if (this.Caption != "")
                {
                    
                }
                
                Log.LogEvent("Exception", "", 
                             "Window Handle: " + this.Handle.ToString() + Environment.NewLine + 
                             "Window Caption: " + this.Caption + Environment.NewLine + 
                             "Application: " + this.ApplicationName, 
                             "Window", 
                             ex);
            }


        }

        public void MoveToDesktop(int desktopNumber, bool follow)
        {
            MoveToDesktop(desktopNumber);
            if(follow)
            {                
                GoToDesktop(desktopNumber);
            }
        }

        public void GoToDesktop()
        {
            GoToDesktop(DesktopNumber);
        }

        private void GoToDesktop(int desktopNumber)
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

                    //Right before switching the desktop, set the active window as the taskbar
                    //This prevents windows from flashing in the taskbar when switching desktops
                    Window w = Window.Taskbar();
                    w.SetAsForegroundWindow();
                    current.Switch();
                    //give focus to the window we followed
                    this.SetAsForegroundWindow();
                    Program.NavigateCount++;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured navigating to the specified desktop. See additional details below." + Environment.NewLine + Environment.NewLine +
                    ex.Message + Environment.NewLine +
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "", "", "Window", ex);
            }
        }

        private string GetWindowText()
        {
            try
            {
                int length = User32.GetWindowTextLength(Handle);
                StringBuilder text = new StringBuilder(length + 1);
                User32.GetWindowText(Handle, text, text.Capacity);
                return text.ToString();
            }catch (Exception ex)
            {
                Log.LogEvent("Exception", "", "", "Window", ex);
                return "";
            }
        }

        private string GetWindowName()
        {
            try
            {
                uint lpdwProcessId;
                User32.GetWindowThreadProcessId(Handle, out lpdwProcessId);

                IntPtr hProcess = Kernel32.OpenProcess(0x0410, false, lpdwProcessId);

                StringBuilder text = new StringBuilder(1000);
                //GetModuleBaseName(hProcess, IntPtr.Zero, text, text.Capacity);
                Psapi.GetModuleFileNameEx(hProcess, IntPtr.Zero, text, text.Capacity);

                Kernel32.CloseHandle(hProcess);

                return text.ToString();
            }
            catch (Exception ex)
            {
                Log.LogEvent("Exception", "", "", "Window", ex);
                return "";
            }
            
        }

        private uint GetProcessID()
        {
            try
            {
                uint lpdwProcessId;
                User32.GetWindowThreadProcessId(Handle, out lpdwProcessId);
                return lpdwProcessId;
            }catch (Exception ex)
            {
                Log.LogEvent("Exception", "", "", "Window", ex);
                return 0;
            }
            
        }

        private int GetDesktopNumber(Guid Guid)
        {
            try
            {
                VirtualDesktop[] Desktops = VirtualDesktop.GetDesktops();
                for (int i = 0; i <= Desktops.Count() - 1; i++)
                {
                    if (Desktops[i].Id == Guid)
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
                Log.LogEvent("Exception", "", "", "Window", ex);
                return 1;
            }

        }
    }
}
