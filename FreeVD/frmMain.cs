using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsDesktop;
using WindowsInput;
using WindowsInput.Native;
using FreeVD.Internal;
using System.Runtime.Serialization.Formatters.Binary;
using FreeVD;
using System.IO;

namespace FreeVD
{
    public partial class frmMain : Form
    { 
        private bool ExitClicked = false;
        public Timer timerCheckVersion = new Timer();

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

        #region "Event Handlers"

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
                LoadSettings();
                SetSystemTrayIcon();
                timerCheckVersion.Tick += timerCheckVersion_Tick;
                timerCheckVersion.Interval = 600000; //10 minutes
                timerCheckVersion.Start();

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
            GC.Collect();
        }

        private void frmMain_Closing(object sender, CancelEventArgs e)
        {
            if (!ExitClicked)
            {
                if (PInvoke.GetSystemMetrics(PInvoke.SystemMetric.SM_SHUTTINGDOWN) == 0)
                {
                    e.Cancel = true;
                    HideSettings();
                }
                return;
            }
            SystemTray.Visible = false;
            Log.LogEvent("Program Exited", "Icon Theme: " + Program.IconTheme +
                            "\r\nPin Count: " + Program.PinCount +
                            "\r\nMove Count: " + Program.MoveCount +
                            "\r\nNavigateCount: " + Program.NavigateCount, "", "frmMain", null);
            Environment.Exit(0);
        }

        private void timerCheckVersion_Tick(object sender, EventArgs e)
        {
            Program.CheckVersion();
        }



        #endregion

        #region "System Tray"

        private void SystemTray_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void SystemTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    ShowSettings();
                }
                catch { }
            }
        }

        private void mnuSettings_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void mnuGatherWindows_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (KeyValuePair<IntPtr, string> window in Windows.GetOpenWindows())
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

        #region "SystemTrayIcon"

        public void SetSystemTrayIcon()
        {
            SystemTrayWhiteBox();
        }

        private void SystemTrayWhiteBox()
        {
            try
            {
                VirtualDesktop current = VirtualDesktop.Current;
                int i = VirtualDesktopFunctions.GetDesktopNumber(current.Id);
                switch (i)
                {
                    case 1:
                        SystemTray.Icon = Properties.Resources.Windows_8_Numbers_1;
                        break;
                    case 2:
                        SystemTray.Icon = Properties.Resources.Windows_8_Numbers_2;
                        break;
                    case 3:
                        SystemTray.Icon = Properties.Resources.Windows_8_Numbers_3;
                        break;
                    case 4:
                        SystemTray.Icon = Properties.Resources.Windows_8_Numbers_4;
                        break;
                    case 5:
                        SystemTray.Icon = Properties.Resources.Windows_8_Numbers_5;
                        break;
                    case 6:
                        SystemTray.Icon = Properties.Resources.Windows_8_Numbers_6;
                        break;
                    case 7:
                        SystemTray.Icon = Properties.Resources.Windows_8_Numbers_7;
                        break;
                    case 8:
                        SystemTray.Icon = Properties.Resources.Windows_8_Numbers_8;
                        break;
                    case 9:
                        SystemTray.Icon = Properties.Resources.Windows_8_Numbers_9;
                        break;
                }

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

        #endregion

        #endregion        

        #region "Settings"


        private void CreateDefaultHotkeys()
        {
            Hotkey keyMoveNext = new Hotkey("Next");
            Hotkey keyMoveNextFollow = new Hotkey("Next");
            Hotkey keyMovePrevious = new Hotkey("Previous");
            Hotkey keyMovePreviousFollow = new Hotkey("Previous");

            Hotkey keyPinWindow = new Hotkey("99");
            Hotkey keyPinApp = new Hotkey("99");

            HotkeyItem hkiMoveNext = new HotkeyItem("Move Window to Desktop", keyMoveNext);
            HotkeyItem hkiMoveNextFollow = new HotkeyItem("Move Window to Desktop & Follow", keyMoveNextFollow);
            HotkeyItem hkiMovePrevious = new HotkeyItem("Move Window to Desktop", keyMovePrevious);
            HotkeyItem hkiMovePreviousFollow = new HotkeyItem("Move Window to Desktop & Follow", keyMovePreviousFollow);

            HotkeyItem hkiPinWindow = new HotkeyItem("Pin/Unpin Window", keyPinWindow);
            HotkeyItem hkiPinApp = new HotkeyItem("Pin/Unpin Application", keyPinApp);

            Program.hotkeys.AddRange(new HotkeyItem[] {
                             hkiMoveNext, hkiMoveNextFollow, hkiMovePrevious, hkiMovePreviousFollow,
                             hkiPinWindow, hkiPinApp });

            keyMoveNext.Callback = VirtualDesktopFunctions.DesktopMoveNext;
            keyMoveNext.Register(Keys.Right, true, false, false, true);

            keyMoveNextFollow.Callback = VirtualDesktopFunctions.DesktopMoveNextFollow;
            keyMoveNextFollow.Register(Keys.Right, true, true, false, true);

            keyMovePrevious.Callback = VirtualDesktopFunctions.DesktopMovePrevious;
            keyMovePrevious.Register(Keys.Left, true, false, false, true);

            keyMovePreviousFollow.Callback = VirtualDesktopFunctions.DesktopMovePreviousFollow;
            keyMovePreviousFollow.Register(Keys.Left, true, true, false, true);


            keyPinWindow.Callback = VirtualDesktopFunctions.PinWindow;
            keyPinWindow.Register(Keys.Z, true, false, false, true);

            keyPinApp.Callback = VirtualDesktopFunctions.PinApp;
            keyPinApp.Register(Keys.A, true, false, false, true);
        }

        private void CreateDefaultHotkeys_Numpad()
        {
            var items = new List<HotkeyItem>();
            for (int i = 1; i < 10; i++)
            {
                Enum.TryParse<Keys>("NumPad" + i, out var key);

                var ntd = new Hotkey(i.ToString());
                ntd.Register(key, false, true, false, true);
                items.Add(new HotkeyItem("Navigate to Desktop", ntd));

                var mtd = new Hotkey(i.ToString());
                mtd.Register(key, true, false, false, true);
                items.Add(new HotkeyItem("Move Window to Desktop", mtd));

                var mtdf = new Hotkey(i.ToString());
                mtdf.Register(key, true, true, false, true);
                items.Add(new HotkeyItem("Move Window to Desktop & Follow", mtdf));
            }
            Program.hotkeys.AddRange(items.OrderBy(i => i.Type));
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
                    LoadSettings();
                    this.ShowInTaskbar = true;
                    this.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured showing the settings window. See additional details below." + Environment.NewLine + Environment.NewLine + 
                    ex.Message + Environment.NewLine + 
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }
        }

        public void UpdateHotkeyTab()
        {
            lstHotkeys.Items.Clear();
            foreach (HotkeyItem hki in Program.hotkeys)
            {
                string text = "";
                string hotkey = hki.hk.ToString();


                switch (hki.Type)
                {
                    case "Navigate to Desktop":
                        text = hki.Type + " #" + hki.DesktopNumber();
                        break;
                    case "Move Window to Desktop":
                        switch (hki.DesktopNumber())
                        {
                            case "1":
                            case "2":
                            case "3":
                            case "4":
                            case "5":
                            case "6":
                            case "7":
                            case "8":
                            case "9":
                                text = hki.Type + " #" + hki.DesktopNumber();
                                break;
                            case "Next":
                                text = "Move to Next Desktop";
                                break;
                            case "Previous":
                                text = "Move to Previous Desktop";
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Move Window to Desktop & Follow":
                        switch (hki.DesktopNumber())
                        {
                            case "1":
                            case "2":
                            case "3":
                            case "4":
                            case "5":
                            case "6":
                            case "7":
                            case "8":
                            case "9":
                                text = "Move Window to Desktop #" + hki.DesktopNumber() + " & Follow";
                                break;
                            case "Next":
                                text = "Move Window to Next Desktop & Follow";
                                break;
                            case "Previous":
                                text = "Move Window to Previous Desktop & Follow";
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Pin/Unpin Window":
                        text = hki.Type;
                        break;
                    case "Pin/Unpin Application":
                        text = hki.Type;
                        break;
                    default:
                        break;
                }
                System.Windows.Forms.ListViewItem lvi = new System.Windows.Forms.ListViewItem(new string[] {
                                                                                                                text,
                                                                                                                hotkey }, -1);
                lstHotkeys.Items.Add(lvi);
                lstHotkeys.Refresh();
            }
        }

        public void HideSettings()
        {
            try
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured hiding the settings window. See additional details below." + Environment.NewLine + Environment.NewLine + 
                    ex.Message + Environment.NewLine + 
                    ex.Source + "::" + ex.TargetSite.Name);
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }
        }

        private bool foo;

        public void LoadSettings()
        {
            try
            {
                if (!foo) // (!Program.storage.FileExists("settings.json"))
                {
                    CreateDefaultHotkeys_Numpad();
                    CreateDefaultHotkeys();
                    SaveSettings();
                    foo = true;
                }

                string[] individualSettings;

                using (var stream = new IsolatedStorageFileStream("settings.json", FileMode.Open, Program.storage))
                {
                    var bf = new BinaryFormatter();
                    object oo = bf.Deserialize(stream);
                    string settings = (string)oo;
                    individualSettings = settings.Split('~');
                }
                
                Program.IconTheme = "White Box";
                
                //unregister all current hotkeys and remove from the list
                foreach (HotkeyItem hki in Program.hotkeys)
                {
                    hki.hk.Unregister();
                    hki.hk.Dispose();
                }
                Program.hotkeys.Clear();

                //hotkeys
                for (int i = 1; i < individualSettings.Length; i++)
                {
                    string type = individualSettings[i].Split(';')[0];
                    string desktopNumber = individualSettings[i].Split(';')[1];
                    bool ALT = bool.Parse(individualSettings[i].Split(';')[2]);
                    bool CTRL = bool.Parse(individualSettings[i].Split(';')[3]);
                    bool SHIFT = bool.Parse(individualSettings[i].Split(';')[4]);
                    bool WIN = bool.Parse(individualSettings[i].Split(';')[5]);
                    string KEY = individualSettings[i].Split(';')[6];

                    Hotkey hk = new Hotkey(desktopNumber);
                    KeysConverter kc = new KeysConverter();
                    hk.Register((Keys)kc.ConvertFromString(KEY), ALT, CTRL, SHIFT, WIN);


                    HotkeyItem hki = new HotkeyItem(type, hk);
                    Program.hotkeys.Add(hki);

                    switch (type)
                    {
                        case "Navigate to Desktop":
                            hk.Callback = VirtualDesktopFunctions.DesktopGo;
                            break;
                        case "Move Window to Desktop":
                            switch (desktopNumber)
                            {
                                case "1":
                                case "2":
                                case "3":
                                case "4":
                                case "5":
                                case "6":
                                case "7":
                                case "8":
                                case "9":
                                    hk.Callback = VirtualDesktopFunctions.DesktopMove;
                                    break;
                                case "Next":
                                    hk.Callback = VirtualDesktopFunctions.DesktopMoveNext;
                                    break;
                                case "Previous":
                                    hk.Callback = VirtualDesktopFunctions.DesktopMovePrevious;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Move Window to Desktop & Follow":
                            switch (desktopNumber)
                            {
                                case "1":
                                case "2":
                                case "3":
                                case "4":
                                case "5":
                                case "6":
                                case "7":
                                case "8":
                                case "9":
                                    hk.Callback = VirtualDesktopFunctions.DesktopMoveFollow;
                                    break;
                                case "Next":
                                    hk.Callback = VirtualDesktopFunctions.DesktopMoveNextFollow;
                                    break;
                                case "Previous":
                                    hk.Callback = VirtualDesktopFunctions.DesktopMovePreviousFollow;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Pin/Unpin Window":
                            hk.Callback = VirtualDesktopFunctions.PinWindow;
                            break;
                        case "Pin/Unpin Application":
                            hk.Callback = VirtualDesktopFunctions.PinApp;
                            break;
                        default:
                            break;
                    }

                }
                UpdateHotkeyTab();
            }
            catch (Exception ex)
            {
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }
        }

        public void SaveSettings()
        {
            try
            {
                StringBuilder settings = new StringBuilder();
                settings.Append("IconTheme;White Box");

                foreach(HotkeyItem hki in Program.hotkeys)
                {
                    settings.Append("~" + hki.Type + ";" + hki.DesktopNumber() + ";" + hki.ALT().ToString() + ";" + hki.CTRL().ToString() + ";" + hki.SHIFT().ToString() + ";" + hki.WIN().ToString() + ";" + hki.KEY());
                }

                using (var stream = new IsolatedStorageFileStream("settings.json", System.IO.FileMode.OpenOrCreate, Program.storage))
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(stream, settings.ToString());
                }
                SetSystemTrayIcon();
            }
            catch (Exception ex)
            {
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }

        }

        #endregion

        #region "Tabs"

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

        #region "Hotkey Tab"

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
                    Program.hotkeys[i].hk.Unregister();
                    Program.hotkeys[i].hk.Dispose();
                    Program.hotkeys.RemoveAt(i);
                    SaveSettings();
                    lstHotkeys.Items.RemoveAt(i);

                }
            }
            catch (Exception ex)
            {
                Log.LogEvent("Exception", "", "", "frmMain", ex);
            }
            
        }

        #endregion

        #region "Pinned App Tab"

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

        #endregion

        #endregion
    }
}

//public class VDHotkey : Hotkey
//{
//}

public class HotkeyItem
{
    public Hotkey hk = new Hotkey("");

    public HotkeyItem(string type, Hotkey hk)
    {
        this.Type = type;
        this.hk = hk;
    }

    public string Type
    {
        get; set;
    }
    
    public bool ALT()
    {
        return hk.Alt;
    }

    public bool CTRL()
    {
        return hk.Ctrl;
    }

    public bool SHIFT()
    {
        return hk.Shift;
    }

    public bool WIN()
    {
        return hk.Win;
    }

    public string KEY()
    {
        return hk.Key.ToString();
    }

    public string DesktopNumber()
    {
        return hk.ID;
    }

}
