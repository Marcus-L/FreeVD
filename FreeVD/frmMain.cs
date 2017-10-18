using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WindowsDesktop;
using Newtonsoft.Json;
using System.Resources;
using System.Drawing;
using FreeVD.Lib.Interop;

namespace FreeVD
{
    public partial class frmMain : Form
    { 
        private bool ExitClicked = false;

        public frmMain()
        {
            InitializeComponent();
            Log.LogEvent("Program Started", "", "", "", null);
            //Wire up some events
            this.Closing += frmMain_Closing;
            this.Load += frmMain_Load;
            mnuExit.Click += mnuExit_Click;
            mnuSettings.Click += mnuSettings_Click;
            VirtualDesktop.CurrentChanged += VirtualDesktop_CurrentChanged;
            VirtualDesktop.ApplicationViewChanged += VirtualDesktop_ApplicationViewChanged;
            lblVersion.Text = Program.version;
            SystemTray.Text = "FreeVD " + Program.version;

            //Create a new thread to retrieve the ProgID and Executables on this machine.
            //This is used so that the app is able to pin an application
            //System.Threading.Thread tGetProgs = new System.Threading.Thread(new System.Threading.ThreadStart(GetProgs));
            //tGetProgs.Start();
        } 

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                ShowInTaskbar = false;
                Visible = false;
                EnsureDefaultSettings();
                SetSystemTrayIcon();
            }
            catch (Exception ex)
            {
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }
        }

        private void VirtualDesktop_ApplicationViewChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void VirtualDesktop_CurrentChanged(object sender, VirtualDesktopChangedEventArgs e)
        {
            SetSystemTrayIcon();
        }

        private void frmMain_Closing(object sender, CancelEventArgs e)
        {
            if (!ExitClicked)
            {
                if (User32.GetSystemMetrics(SystemMetric.SM_SHUTTINGDOWN) == 0)
                {
                    e.Cancel = true;
                    HideSettings();
                }
                return;
            }
            SystemTray.Visible = false;
            Log.LogEvent("Program Exited", "Pin Count: " + Program.PinCount +
                            "\r\nMove Count: " + Program.MoveCount +
                            "\r\nNavigateCount: " + Program.NavigateCount, "", "frmMain", null);
            Environment.Exit(0);
        }

        private void SystemTray_MouseClick(object sender, MouseEventArgs e)
        {
            // do nothing
        }

        private void SystemTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) ShowSettings();
        }

        private void mnuSettings_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void mnuGatherWindows_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (KeyValuePair<IntPtr, string> window in Window.GetOpenWindows())
                {
                    IntPtr hWnd = window.Key;
                    string title = window.Value;
                    Window win = new Window(hWnd);
                    if(win.DesktopNumber != VirtualDesktopFunctions.GetCurrentDesktopNumber() && win.IsDesktop == false)
                    {
                        win.MoveToDesktop(VirtualDesktopFunctions.GetCurrentDesktopNumber());
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        private void DesktopMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mnu = (ToolStripMenuItem)sender;
            VirtualDesktopFunctions.GoToDesktop((int)mnu.Tag);
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            try
            {
                ExitClicked = true;
                this.Close();

                MessageBox.Show("exiting");
            }
            catch (Exception ex)
            {
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }

        }

        private void SystemTrayMenu_Opening(object sender, CancelEventArgs e)
        {
            CreateDesktopMenu();
        }

        private void CreateDesktopMenu()
        {
            Program.Desktops = VirtualDesktop.GetDesktops();
            mnuSwitchDesktop.DropDownItems.Clear();


            for (int i = 0; i < Program.Desktops.Count(); i++)
            {
                ToolStripMenuItem mnu = new ToolStripMenuItem();
                mnu.Text = "Desktop " + (i + 1).ToString();
                mnu.Tag = i + 1;
                mnu.Click += DesktopMenu_Click;
                if (VirtualDesktopFunctions.GetDesktopNumber(VirtualDesktop.Current.Id) == i + 1)
                {
                    mnu.CheckState = CheckState.Checked;
                }
                else
                {
                    mnu.CheckState = CheckState.Unchecked;
                }

                mnuSwitchDesktop.DropDownItems.Add(mnu);
            }
        }

        public void SetSystemTrayIcon()
        {
            try
            {
                VirtualDesktop current = VirtualDesktop.Current;
                int dn = VirtualDesktopFunctions.GetDesktopNumber(current.Id);
                var rm = Properties.Resources.ResourceManager;
                SystemTray.Icon = (Icon)rm.GetObject($"Windows_8_Numbers_{dn}");
                SystemTray.Visible = true;
            }
            catch (Exception ex)
            {
                SystemTray.Icon = Properties.Resources.Windows_8_Numbers_1;
                MessageBox.Show("An error occured setting the system tray icon. See additional details below." + Environment.NewLine + Environment.NewLine +
                    ex.Message + Environment.NewLine +
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }
        }    

        public void ShowSettings()
        {
            try
            {
                if (ShowInTaskbar)
                {
                    BringToFront();
                }
                else
                {
                    ShowInTaskbar = true;
                    Visible = true;
                }

                // reload items
                lstHotkeys.Items.Clear();
                lstHotkeys.Items.AddRange(Settings.Default.Hotkeys.Select(
                    hk => new ListViewItem(new string[] {
                    hk.ActionString,
                    hk.ToString(),
                    JsonConvert.SerializeObject(hk)
                })).ToArray());
                lstHotkeys.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured showing the settings window. See additional details below." + Environment.NewLine + Environment.NewLine + 
                    ex.Message + Environment.NewLine + 
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }
        }
        
        public void HideSettings()
        {
            try
            {
                Visible = false;
                ShowInTaskbar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured hiding the settings window. See additional details below." + Environment.NewLine + Environment.NewLine + 
                    ex.Message + Environment.NewLine + 
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }
        }

        public void EnsureDefaultSettings()
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

        public void SaveSettings()
        {
            Debug.WriteLine("saving settings, unregistering " + Settings.Default.Hotkeys.Count + " hotkeys");
            // deactivate all the active hotkeys
            Settings.Default.Hotkeys.ForEach(hotkey => hotkey.Unregister());

            // update saved settings
            Settings.Default.Hotkeys.Clear();
            Settings.Default.Hotkeys.AddRange(lstHotkeys.Items.Cast<ListViewItem>()
                .Select(lvi =>
                    JsonConvert.DeserializeObject<VDHotkey>(
                        lvi.SubItems[lvi.SubItems.Count - 1].Text
                    )
                ));
            Settings.Save();

            // reload settings to activate hotkeys
            Settings.Reload();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            HideSettings();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveSettings();
            HideSettings();
        }

        private void btnAddHotkey_Click(object sender, EventArgs e)
        {
            frmHotKey f = new frmHotKey();
            f.ShowDialog(this);
        }

        private void btnDeleteHotkey_Click(object sender, EventArgs e)
        {
            try
            {
                int i = lstHotkeys.SelectedIndices[0];
                if (i > -1)
                {
                    Program.hotkeys.RemoveAt(i);
                    lstHotkeys.Items.RemoveAt(i);
                }
            }
            catch (Exception ex)
            {
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }
            
        }

        public void SetPinnedAppListBox()
        {
            lstPinnedApps.Items.Clear();
            foreach (string appID in Program.PinnedApps)
            {
                lstPinnedApps.Items.Add(appID);
            }
        }

        private void mnuPinnedApps_Opening(object sender, CancelEventArgs e)
        {
            if (lstPinnedApps.SelectedIndex == -1)
            {
                mnuUnpin.Enabled = false;
            }
            else
            {
                mnuUnpin.Enabled = true;
            }
        }

        private void mnuUnpin_Click(object sender, EventArgs e)
        {
            try
            {
                VirtualDesktop.UnpinApplication(lstPinnedApps.Text);
                Program.PinnedApps.Remove(lstPinnedApps.Text);
                SetPinnedAppListBox();
            }
            catch (Exception ex)
            {
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }

        }
    }
}
