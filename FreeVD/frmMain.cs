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
            System.Threading.Thread.Sleep(3000);
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
                    if(win.DesktopNumber != VirtualDestopFunctions.GetCurrentDesktopNumber() && win.IsDesktop == false)
                    {
                        win.MoveToDesktop(VirtualDestopFunctions.GetCurrentDesktopNumber());
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
            VirtualDestopFunctions.GoToDesktop((int)mnu.Tag);
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
                if (VirtualDestopFunctions.GetDesktopNumber(VirtualDesktop.Current.Id) == i + 1)
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
                int i = VirtualDestopFunctions.GetDesktopNumber(current.Id);
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

            keyMoveNext.Callback = VirtualDestopFunctions.DesktopMoveNext;
            keyMoveNext.Register(Keys.Right, true, false, false, true);

            keyMoveNextFollow.Callback = VirtualDestopFunctions.DesktopMoveNextFollow;
            keyMoveNextFollow.Register(Keys.Right, true, true, false, true);

            keyMovePrevious.Callback = VirtualDestopFunctions.DesktopMovePrevious;
            keyMovePrevious.Register(Keys.Left, true, false, false, true);

            keyMovePreviousFollow.Callback = VirtualDestopFunctions.DesktopMovePreviousFollow;
            keyMovePreviousFollow.Register(Keys.Left, true, true, false, true);


            keyPinWindow.Callback = VirtualDestopFunctions.PinWindow;
            keyPinWindow.Register(Keys.Z, true, false, false, true);

            keyPinApp.Callback = VirtualDestopFunctions.PinApp;
            keyPinApp.Register(Keys.A, true, false, false, true);
        }

        private void CreateDefaultHotkeys_Numpad()
        {
            Hotkey keyGoTo01 = new Hotkey("1");
            Hotkey keyGoTo02 = new Hotkey("2");
            Hotkey keyGoTo03 = new Hotkey("3");
            Hotkey keyGoTo04 = new Hotkey("4");
            Hotkey keyGoTo05 = new Hotkey("5");
            Hotkey keyGoTo06 = new Hotkey("6");
            Hotkey keyGoTo07 = new Hotkey("7");
            Hotkey keyGoTo08 = new Hotkey("8");
            Hotkey keyGoTo09 = new Hotkey("9");

            Hotkey keyMoveTo01 = new Hotkey("1");
            Hotkey keyMoveTo02 = new Hotkey("2");
            Hotkey keyMoveTo03 = new Hotkey("3");
            Hotkey keyMoveTo04 = new Hotkey("4");
            Hotkey keyMoveTo05 = new Hotkey("5");
            Hotkey keyMoveTo06 = new Hotkey("6");
            Hotkey keyMoveTo07 = new Hotkey("7");
            Hotkey keyMoveTo08 = new Hotkey("8");
            Hotkey keyMoveTo09 = new Hotkey("9");

            Hotkey keyMoveFollowTo01 = new Hotkey("1");
            Hotkey keyMoveFollowTo02 = new Hotkey("2");
            Hotkey keyMoveFollowTo03 = new Hotkey("3");
            Hotkey keyMoveFollowTo04 = new Hotkey("4");
            Hotkey keyMoveFollowTo05 = new Hotkey("5");
            Hotkey keyMoveFollowTo06 = new Hotkey("6");
            Hotkey keyMoveFollowTo07 = new Hotkey("7");
            Hotkey keyMoveFollowTo08 = new Hotkey("8");
            Hotkey keyMoveFollowTo09 = new Hotkey("9");

            HotkeyItem hkiGoTo01 = new HotkeyItem("Navigate to Desktop", keyGoTo01);
            HotkeyItem hkiGoTo02 = new HotkeyItem("Navigate to Desktop", keyGoTo02);
            HotkeyItem hkiGoTo03 = new HotkeyItem("Navigate to Desktop", keyGoTo03);
            HotkeyItem hkiGoTo04 = new HotkeyItem("Navigate to Desktop", keyGoTo04);
            HotkeyItem hkiGoTo05 = new HotkeyItem("Navigate to Desktop", keyGoTo05);
            HotkeyItem hkiGoTo06 = new HotkeyItem("Navigate to Desktop", keyGoTo06);
            HotkeyItem hkiGoTo07 = new HotkeyItem("Navigate to Desktop", keyGoTo07);
            HotkeyItem hkiGoTo08 = new HotkeyItem("Navigate to Desktop", keyGoTo08);
            HotkeyItem hkiGoTo09 = new HotkeyItem("Navigate to Desktop", keyGoTo09);

            HotkeyItem hkiMoveTo01 = new HotkeyItem("Move Window to Desktop", keyMoveTo01);
            HotkeyItem hkiMoveTo02 = new HotkeyItem("Move Window to Desktop", keyMoveTo02);
            HotkeyItem hkiMoveTo03 = new HotkeyItem("Move Window to Desktop", keyMoveTo03);
            HotkeyItem hkiMoveTo04 = new HotkeyItem("Move Window to Desktop", keyMoveTo04);
            HotkeyItem hkiMoveTo05 = new HotkeyItem("Move Window to Desktop", keyMoveTo05);
            HotkeyItem hkiMoveTo06 = new HotkeyItem("Move Window to Desktop", keyMoveTo06);
            HotkeyItem hkiMoveTo07 = new HotkeyItem("Move Window to Desktop", keyMoveTo07);
            HotkeyItem hkiMoveTo08 = new HotkeyItem("Move Window to Desktop", keyMoveTo08);
            HotkeyItem hkiMoveTo09 = new HotkeyItem("Move Window to Desktop", keyMoveTo09);

            HotkeyItem hkiMoveFollowTo01 = new HotkeyItem("Move Window to Desktop & Follow", keyMoveFollowTo01);
            HotkeyItem hkiMoveFollowTo02 = new HotkeyItem("Move Window to Desktop & Follow", keyMoveFollowTo02);
            HotkeyItem hkiMoveFollowTo03 = new HotkeyItem("Move Window to Desktop & Follow", keyMoveFollowTo03);
            HotkeyItem hkiMoveFollowTo04 = new HotkeyItem("Move Window to Desktop & Follow", keyMoveFollowTo04);
            HotkeyItem hkiMoveFollowTo05 = new HotkeyItem("Move Window to Desktop & Follow", keyMoveFollowTo05);
            HotkeyItem hkiMoveFollowTo06 = new HotkeyItem("Move Window to Desktop & Follow", keyMoveFollowTo06);
            HotkeyItem hkiMoveFollowTo07 = new HotkeyItem("Move Window to Desktop & Follow", keyMoveFollowTo07);
            HotkeyItem hkiMoveFollowTo08 = new HotkeyItem("Move Window to Desktop & Follow", keyMoveFollowTo08);
            HotkeyItem hkiMoveFollowTo09 = new HotkeyItem("Move Window to Desktop & Follow", keyMoveFollowTo09);

            Program.hotkeys.AddRange(new HotkeyItem[] {
                             hkiGoTo01, hkiGoTo02, hkiGoTo03, hkiGoTo04, hkiGoTo05, hkiGoTo06, hkiGoTo07, hkiGoTo08, hkiGoTo09,
                             hkiMoveTo01, hkiMoveTo02, hkiMoveTo03, hkiMoveTo04, hkiMoveTo05, hkiMoveTo06, hkiMoveTo07, hkiMoveTo08, hkiMoveTo09,
                             hkiMoveFollowTo01, hkiMoveFollowTo02, hkiMoveFollowTo03, hkiMoveFollowTo04, hkiMoveFollowTo05, hkiMoveFollowTo06, hkiMoveFollowTo07, hkiMoveFollowTo08, hkiMoveFollowTo09 });

            keyGoTo01.Callback = VirtualDestopFunctions.DesktopGo;
            keyGoTo01.Register(Keys.NumPad1, false, true, false, true);

            keyGoTo02.Callback = VirtualDestopFunctions.DesktopGo;
            keyGoTo02.Register(Keys.NumPad2, false, true, false, true);

            keyGoTo03.Callback = VirtualDestopFunctions.DesktopGo;
            keyGoTo03.Register(Keys.NumPad3, false, true, false, true);
                      
            keyGoTo04.Callback = VirtualDestopFunctions.DesktopGo;
            keyGoTo04.Register(Keys.NumPad4, false, true, false, true);
                      
            keyGoTo05.Callback = VirtualDestopFunctions.DesktopGo;
            keyGoTo05.Register(Keys.NumPad5, false, true, false, true);

            keyGoTo06.Callback = VirtualDestopFunctions.DesktopGo;
            keyGoTo06.Register(Keys.NumPad6, false, true, false, true);

            keyGoTo07.Callback = VirtualDestopFunctions.DesktopGo;
            keyGoTo07.Register(Keys.NumPad7, false, true, false, true);

            keyGoTo08.Callback = VirtualDestopFunctions.DesktopGo;
            keyGoTo08.Register(Keys.NumPad8, false, true, false, true);

            keyGoTo09.Callback = VirtualDestopFunctions.DesktopGo;
            keyGoTo09.Register(Keys.NumPad9, false, true, false, true);


            keyMoveTo01.Callback = VirtualDestopFunctions.DesktopMove;
            keyMoveTo01.Register(Keys.NumPad1, true, false, false, true);

            keyMoveTo02.Callback = VirtualDestopFunctions.DesktopMove;
            keyMoveTo02.Register(Keys.NumPad2, true, false, false, true);

            keyMoveTo03.Callback = VirtualDestopFunctions.DesktopMove;
            keyMoveTo03.Register(Keys.NumPad3, true, false, false, true);

            keyMoveTo04.Callback = VirtualDestopFunctions.DesktopMove;
            keyMoveTo04.Register(Keys.NumPad4, true, false, false, true);

            keyMoveTo05.Callback = VirtualDestopFunctions.DesktopMove;
            keyMoveTo05.Register(Keys.NumPad5, true, false, false, true);

            keyMoveTo06.Callback = VirtualDestopFunctions.DesktopMove;
            keyMoveTo06.Register(Keys.NumPad6, true, false, false, true);

            keyMoveTo07.Callback = VirtualDestopFunctions.DesktopMove;
            keyMoveTo07.Register(Keys.NumPad7, true, false, false, true);

            keyMoveTo08.Callback = VirtualDestopFunctions.DesktopMove;
            keyMoveTo08.Register(Keys.NumPad8, true, false, false, true);

            keyMoveTo09.Callback = VirtualDestopFunctions.DesktopMove;
            keyMoveTo09.Register(Keys.NumPad9, true, false, false, true);


            keyMoveFollowTo01.Callback = VirtualDestopFunctions.DesktopMoveFollow;
            keyMoveFollowTo01.Register(Keys.NumPad1, true, true, false, true);

            keyMoveFollowTo02.Callback = VirtualDestopFunctions.DesktopMoveFollow;
            keyMoveFollowTo02.Register(Keys.NumPad2, true, true, false, true);

            keyMoveFollowTo03.Callback = VirtualDestopFunctions.DesktopMoveFollow;
            keyMoveFollowTo03.Register(Keys.NumPad3, true, true, false, true);

            keyMoveFollowTo04.Callback = VirtualDestopFunctions.DesktopMoveFollow;
            keyMoveFollowTo04.Register(Keys.NumPad4, true, true, false, true);

            keyMoveFollowTo05.Callback = VirtualDestopFunctions.DesktopMoveFollow;
            keyMoveFollowTo05.Register(Keys.NumPad5, true, true, false, true);

            keyMoveFollowTo06.Callback = VirtualDestopFunctions.DesktopMoveFollow;
            keyMoveFollowTo06.Register(Keys.NumPad6, true, true, false, true);

            keyMoveFollowTo07.Callback = VirtualDestopFunctions.DesktopMoveFollow;
            keyMoveFollowTo07.Register(Keys.NumPad7, true, true, false, true);

            keyMoveFollowTo08.Callback = VirtualDestopFunctions.DesktopMoveFollow;
            keyMoveFollowTo08.Register(Keys.NumPad8, true, true, false, true);

            keyMoveFollowTo09.Callback = VirtualDestopFunctions.DesktopMoveFollow;
            keyMoveFollowTo09.Register(Keys.NumPad9, true, true, false, true);
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
                string hotkey = hki.hk.HotKeyString();


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

        public void LoadSettings()
        {
            try
            {
                if (Program.storage.FileExists("FreeVD.bin") == false)
                {
                    CreateDefaultHotkeys_Numpad();
                    //CreateDefaultHotkeys_D();
                    CreateDefaultHotkeys();
                    SaveSettings();
                }

                string[] individualSettings;

                using (var stream = new IsolatedStorageFileStream("FreeVD.bin", System.IO.FileMode.Open, Program.storage))
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
                            hk.Callback = VirtualDestopFunctions.DesktopGo;
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
                                    hk.Callback = VirtualDestopFunctions.DesktopMove;
                                    break;
                                case "Next":
                                    hk.Callback = VirtualDestopFunctions.DesktopMoveNext;
                                    break;
                                case "Previous":
                                    hk.Callback = VirtualDestopFunctions.DesktopMovePrevious;
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
                                    hk.Callback = VirtualDestopFunctions.DesktopMoveFollow;
                                    break;
                                case "Next":
                                    hk.Callback = VirtualDestopFunctions.DesktopMoveNextFollow;
                                    break;
                                case "Previous":
                                    hk.Callback = VirtualDestopFunctions.DesktopMovePreviousFollow;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Pin/Unpin Window":
                            hk.Callback = VirtualDestopFunctions.PinWindow;
                            break;
                        case "Pin/Unpin Application":
                            hk.Callback = VirtualDestopFunctions.PinApp;
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

                using (var stream = new IsolatedStorageFileStream("FreeVD.bin", System.IO.FileMode.OpenOrCreate, Program.storage))
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

        private void lblGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/marcus-l/freevd");
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
