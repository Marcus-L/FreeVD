using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WindowsDesktop;
using Newtonsoft.Json;
using FreeVD.Lib.Interop;
using System.Reactive.Linq;
using System.Drawing;

namespace FreeVD
{
    public partial class SettingsForm : Form
    {
        private IDisposable Subscription;
        private int LastStateHash;

        public SettingsForm()
        {
            InitializeComponent();

            AppModel.PinnedApps.CollectionChanged += (obj, args) => RefreshPins();
            AppModel.PinnedWindows.CollectionChanged += (obj, args) => RefreshPins();
            PinnedAppsMenu.Opening += (obj, args) =>
            {
                MenuUnpin.Enabled = PinnedAppList.SelectedItems.Count > 0;
            };
            HotkeyMenu.Opening += (obj, args) =>
            {
                HotkeyMenu_Edit.Enabled = HotkeyList.SelectedItems.Count == 1;
                HotkeyMenu_Delete.Enabled = HotkeyList.SelectedItems.Count > 0;
            };
            AutoStartCheckbox.CheckedChanged += (obj, args) =>
            {
                if (AutoStartCheckbox.Checked != Settings.Default.AutoStart)
                    ButtonApply.Enabled = true;
            };
            Shown += (obj, args) => User32.SetForegroundWindow(Handle);

            // Add polling to update Window titles when open
            Subscription = Observable.Interval(TimeSpan.FromMilliseconds(1000))
                .Subscribe(token => RefreshPins());
            FormClosed += (obj, args) => Subscription.Dispose();

            Load += (obj, args) => RefreshData();
        }

        // button event handlers
        private void ButtonApply_Click(object sender, EventArgs e) => SaveSettings(false);
        private void ButtonOK_Click(object sender, EventArgs e) => SaveSettings(true);
        private void ButtonCancel_Click(object sender, EventArgs e) => Close();

        private void RefreshData()
        {
            ButtonApply.Enabled = false;

            // load misc settings
            VersionLabel.Text = VersionLabel.Text.Replace("{Version}", $"v{AppModel.Version}");
            LicenseLabel.Text = LicenseLabel.Text.Replace("{Version}", $"v{AppModel.Version}");

            AutoStartCheckbox.Checked = Settings.Default.AutoStart;

            // load hotkeys from settings
            HotkeyList.Items.Clear();
            HotkeyList.Items.AddRange(Settings.Default.Hotkeys.OrderBy(hk => hk.ActionString)
                .Select(hk => new ListViewItem().Update(hk)).ToArray());

            // load pins from app model
            RefreshPins();
        }

        private void SaveSettings(bool andClose)
        {
            if (ButtonApply.Enabled)
            {
                // update saved settings
                Settings.Default.Hotkeys.ForEach(hk => hk.Unregister());
                Settings.Default.Hotkeys.Clear();
                Settings.Default.Hotkeys.AddRange(HotkeyList.Items.Cast<ListViewItem>()
                    .Select(item => item.Get<VDHotkey>()));
                Settings.Default.AutoStart = AutoStartCheckbox.Checked;
                Settings.Save();
                Settings.Reload(); // reload settings to activate hotkeys
            }

            if (andClose) { Close(); }
            else { RefreshData(); }
        }

        public void RefreshPins()
        {
            // join main UI thread if necessary
            if (InvokeRequired) { Invoke((Action)RefreshPins); return; }

            // only refresh the window on changd hash to avoid flickering
            var hash = string.Join("|",
                AppModel.PinnedWindows.Select(w => w.GetWindowText())
                    .Union(AppModel.PinnedApps.Select(a => a.Name))).GetHashCode(); 
            if (hash != LastStateHash)
            {
                PinnedAppList.Items.Clear();
                PinnedAppList.Items.AddRange(AppModel.PinnedApps.Select(a => 
                    new ListViewItem().Update(a)).ToArray());
                PinnedAppList.Items.AddRange(AppModel.PinnedWindows.Select(w =>
                    new ListViewItem().Update(w)).ToArray());
                LastStateHash = hash;
            }
        }

        private void MenuUnpin_Click(object sender, EventArgs e)
        {
            try
            {
                var pin = PinnedAppList.SelectedItems[0].Get<PinInfo>();
                pin.Window?.TogglePinWindow();
                if (pin.AppInfo != null)
                {
                    VirtualDesktop.UnpinApplication(pin.AppInfo.Id);
                    AppModel.PinnedApps.Remove(pin.AppInfo);
                }
            }
            catch (Exception ex)
            {
                Log.LogEvent("SettingsForm", $"Unpinning", ex);
            }
        }

        private void EditSelectedItem()
        {
            var item = HotkeyList.SelectedItems[0];
            EditHotkey(item, item.Get<VDHotkey>());
        }

        private bool EditHotkey(ListViewItem item, VDHotkey hotkey)
        {
            
            var editForm = new HotkeyForm() { Hotkey = hotkey };
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                item.Update(hotkey);
                ButtonApply.Enabled = true;
                return true;
            }
            return false;
        }

        private void HotkeyMenu_Edit_Click(object sender, EventArgs e)
        {
            EditSelectedItem();
        }

        private void HotkeyList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (HotkeyList.SelectedItems.Count == 1) EditSelectedItem();
        }

        private void HotkeyMenu_Delete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in HotkeyList.SelectedItems)
            {
                item.Remove();
            }
            ButtonApply.Enabled = true;
        }

        private void HotkeyMenu_Add_Click(object sender, EventArgs e)
        {
            var item = new ListViewItem();
            if (EditHotkey(item, new VDHotkey()))
            {
                HotkeyList.Items.Add(item);
            }
        }
    }

    public static class ListViewItemExtensions
    {
        public static T Get<T>(this ListViewItem item)
        {
            string json = item.SubItems[2].Text;
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static ListViewItem Update(this ListViewItem item, VDHotkey hotkey)
        {
            item.SubItems.Clear();
            item.SubItems[0].Text = hotkey.ActionString;
            item.SubItems.Add(hotkey.ToString());
            item.SubItems.Add(JsonConvert.SerializeObject(hotkey));
            item.ForeColor = hotkey.Registered ? Color.Black : ColorTranslator.FromHtml("#777777");
            return item;
        }

        public static ListViewItem Update(this ListViewItem item, AppInfo appInfo)
        {
            item.SubItems.Clear();
            item.SubItems[0].Text = "Application";
            item.SubItems.Add(appInfo.Name);
            item.SubItems.Add(JsonConvert.SerializeObject(new PinInfo { AppInfo = appInfo }));
            return item;
        }

        public static ListViewItem Update(this ListViewItem item, Window window)
        {
            item.SubItems.Clear();
            item.SubItems[0].Text = "Window";
            item.SubItems.Add(window.GetWindowText());
            item.SubItems.Add(JsonConvert.SerializeObject(new PinInfo { Window = window }));
            return item;
        }
    }
}
