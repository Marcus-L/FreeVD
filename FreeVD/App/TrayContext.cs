using FreeVD.Lib.Hotkeys;
using FreeVD.Lib.Interop;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsDesktop;

namespace FreeVD
{
    public class TrayContext : ApplicationContext
    {
        private NotifyIcon TrayIcon { get; set; }
        private SettingsForm SettingsForm { get; set; }

        public TrayContext()
        {
            // delay startup to idle to avoid DPI scaling issues
            Application.Idle += Startup;
        }

        private void Startup(object app, EventArgs evt)
        {
            // avoid multiple startup
            Application.Idle -= Startup;

            // run pre-start checks
            if (!Utils.EnsureMinimumOSVersion() ||
                !Utils.EnsureSingleInstance())
            {
                Application.Exit();
                return;
            }

            TrayIcon = new NotifyIcon()
            {
                Visible = true,
                Text = $"FreeVD {AppModel.Version}",
                ContextMenuStrip = new ContextMenuStrip()
            };
            TrayIcon.ContextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem("Switch Desktop"),
                new ToolStripMenuItem("All Windows Here", null, (obj,args) => VirtualDesktop.Current.GatherWindows()),
                new ToolStripSeparator(),
                new ToolStripMenuItem("Settings", null, (obj,args) => OpenSettings()),
                new ToolStripMenuItem("Exit", null, (obj,args) => Application.Exit())
            });
            SetIcon(VirtualDesktop.Current);

            AppModel.Initialize();

            // events
            TrayIcon.MouseDoubleClick += (obj, args) =>
                { if (args.Button == MouseButtons.Left) OpenSettings(); };
            TrayIcon.ContextMenuStrip.Opening += (obj, args) => ConfigureDesktopsMenu();
            TrayIcon.MouseMove += (obj, args) => { AppModel.CurrentWindowInFocus = User32.GetForegroundWindow(); };
            TrayIcon.MouseDown += (obj, args) => AppModel.SaveWindowInFocus();

            // keep icon in sync with desktop
            VirtualDesktop.CurrentChanged += (obj, args) => {
                SetIcon(args.NewDesktop);
                AppModel.LoadCopiedWindows();
                AppModel.LoadPinnedWindowsPos();
                AppModel.LoadWindowInFocus();
            };
            VirtualDesktop.Destroyed += (obj, args) => {
                AppModel.RemoveDesktop(args.Destroyed.Id);
            };

            Application.ApplicationExit += (obj, args) => {
                TrayIcon.Visible = false;
                User32.UnhookWindowsHookEx(WinTabKeyboardHook._hookID);
            };

            // watch pinned apps/windows
            PinWatcher.Initialize();

            // open settings if app was started again
            Utils.ListenForAppStarts(OpenSettings);
        }

        private void ConfigureDesktopsMenu()
        {
            var currentDesktop = VirtualDesktop.Current;
            var switchDesktop = TrayIcon.ContextMenuStrip.Items.Cast<ToolStripItem>()
                .First(i => i.Text == "Switch Desktop") as ToolStripMenuItem;
            switchDesktop.DropDownItems.Clear();
            switchDesktop.DropDownItems.AddRange(
                VirtualDesktop.GetDesktops().Select((desktop, index) => 
                    new ToolStripMenuItem($"Desktop {index + 1}", null, (obj,args) => { AppModel.SavePinnedWindowsPos(); desktop.Switch(); })
                    {
                        CheckState = desktop == currentDesktop ? CheckState.Checked : CheckState.Unchecked,
                    })
                .ToArray());
        }

        private void SetIcon(VirtualDesktop desktop)
        {
            var rm = Properties.Resources.ResourceManager;
            TrayIcon.Icon = (Icon)rm.GetObject($"Windows_8_Numbers_{desktop.GetNumber()}");
        }

        private void OpenSettings()
        {
            if (SettingsForm?.Visible ?? false)
            {
                if (VirtualDesktop.Current.GetNumber() != 
                    VirtualDesktop.FromHwnd(SettingsForm.Handle).GetNumber())
                {
                    VirtualDesktop.MoveToDesktop(SettingsForm.Handle, 
                        VirtualDesktop.Current);
                }
                User32.SetForegroundWindow(SettingsForm.Handle);
            }
            else
            {
                SettingsForm = new SettingsForm();
                SettingsForm.ShowDialog();
                SettingsForm = null;
            }
        }
    }
}
