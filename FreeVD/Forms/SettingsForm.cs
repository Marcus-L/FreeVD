using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WindowsDesktop;
using Newtonsoft.Json;
using FreeVD.Lib.Interop;

namespace FreeVD
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            lblVersion.Text = AppModel.Version;
            AppModel.PinnedApps.CollectionChanged += (obj, args) => RefreshPins();
            AppModel.PinnedWindows.CollectionChanged += (obj, args) => RefreshPins();
            PinnedAppsMenu.Opening += (obj, args) =>
            {
                MenuUnpin.Enabled = PinnedAppList.SelectedItems.Count > 0;
            };

            // TODO: Add polling to update Window titles when open

            Load += (obj, args) =>
            {
                // reload items
                HotkeyList.Items.Clear();
                HotkeyList.Items.AddRange(Settings.Default.Hotkeys.Select(
                    hk => new ListViewItem(new string[] {
                    hk.ActionString,
                    hk.ToString(),
                    JsonConvert.SerializeObject(hk)
                })).ToArray());
                HotkeyList.Refresh();
                RefreshPins();

                LoadSettings();
            };

            Shown += (obj, args) => User32.SetForegroundWindow(Handle);
        }

        // button event handlers
        private void ButtonApply_Click(object sender, EventArgs e) => SaveSettings(false);
        private void ButtonOK_Click(object sender, EventArgs e) => SaveSettings(true);
        private void ButtonCancel_Click(object sender, EventArgs e) => Close();
        
        private void LoadSettings()
        {
            cbAutoStart.Checked = Settings.Default.AutoStart;
        }

        private void SaveSettings(bool andClose)
        {
            // deactivate all the active hotkeys
            Settings.Default.Hotkeys.ForEach(hotkey => hotkey.Unregister());

            // update saved settings
            Settings.Default.Hotkeys.Clear();
            Settings.Default.Hotkeys.AddRange(HotkeyList.Items.Cast<ListViewItem>()
                .Select(lvi =>
                    JsonConvert.DeserializeObject<VDHotkey>(
                        lvi.SubItems[lvi.SubItems.Count - 1].Text
                    )
                ));
            Settings.Default.AutoStart = cbAutoStart.Checked;
            Settings.Save();
            Settings.Reload(); // reload settings to activate hotkeys

            if (andClose) Close();
        }

        public void RefreshPins()
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => RefreshPins()));
                return;
            }

            PinnedAppList.Items.Clear();
            Action<IEnumerable<string[]>> addItems = items => 
                PinnedAppList.Items.AddRange(items.Select(s => new ListViewItem(s)).ToArray());
            addItems(AppModel.PinnedApps.Select(a => new string[] { "Application", a.Name, JsonConvert.SerializeObject(a) }));
            addItems(AppModel.PinnedWindows.Select(w => new string[] { "Window", w.GetWindowText(), w.Handle.ToString() }));
        }

        private void MenuUnpin_Click(object sender, EventArgs e)
        {
            try
            {
                var subItems = PinnedAppList.SelectedItems[0].SubItems
                    .Cast<ListViewItem.ListViewSubItem>().Select(lvi => lvi.Text).ToArray();
                var appInfo = JsonConvert.DeserializeObject<AppInfo>(subItems[2]);
                switch (subItems[0])
                {
                    case "Window":
                        new Window(new IntPtr(int.Parse(appInfo.Id))).TogglePinWindow();
                        break;
                    case "Application":
                        VirtualDesktop.UnpinApplication(appInfo.Id);
                        AppModel.PinnedApps.Remove(appInfo);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                AppModel.PinnedApps.Remove(appInfo);
            }
            catch (Exception ex)
            {
                Log.LogEvent("SettingsForm", $"Unpinning", ex);
            }
        }

        //private void ButtonAddHotkey_Click(object sender, EventArgs e)
        //{
        //    HotKeyForm f = new HotKeyForm();
        //    f.ShowDialog(this);
        //}

        //private void ButtonDeleteHotkey_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int i = HotkeyList.SelectedIndices[0];
        //        if (i > -1)
        //        {
        //            HotkeyList.Items.RemoveAt(i);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogEvent("Exception", "", "", "frmMain", ex);
        //    }
        //}
    }
}
