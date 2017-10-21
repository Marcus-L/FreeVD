using FreeVD.Lib.Interop;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Automation;
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

            // events
            TrayIcon.MouseDoubleClick += (obj, args) =>
                { if (args.Button == MouseButtons.Left) OpenSettings(); };
            TrayIcon.ContextMenuStrip.Opening += (obj, args) => ConfigureDesktopsMenu();

            // keep icon in sync with desktop
            VirtualDesktop.CurrentChanged += (obj, args) => SetIcon(args.NewDesktop);

            // remove icon when exiting
            Application.ApplicationExit += (obj, args) => TrayIcon.Visible = false;

            // watch pinned apps/windows
            PinWatcher.Initialize();
        }

        private void ConfigureDesktopsMenu()
        {
            var currentDesktop = VirtualDesktop.Current;
            var switchDesktop = TrayIcon.ContextMenuStrip.Items.Cast<ToolStripItem>()
                .First(i => i.Text == "Switch Desktop") as ToolStripMenuItem;
            switchDesktop.DropDownItems.Clear();
            switchDesktop.DropDownItems.AddRange(
                VirtualDesktop.GetDesktops().Select((desktop, index) => 
                    new ToolStripMenuItem($"Desktop {index + 1}", null, (obj,args) => desktop.Switch())
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
                    VirtualDesktopHelper.MoveToDesktop(SettingsForm.Handle, 
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
