using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreeVD.Lib.Interop
{
    public class SystemProcessHookForm : Form
    {
        private readonly int msgNotify;
        public delegate void EventHandler(object sender, IntPtr hWnd);
        public event EventHandler WindowCreatedEvent;
        public event EventHandler WindowDestroyedEvent;
        protected virtual void OnWindowDestroyed(IntPtr hWnd)
        {
            var handler = WindowDestroyedEvent;
            if (handler != null) handler(this, hWnd);
        }
        protected virtual void OnWindowCreated(IntPtr hWnd)
        {
            var handler = WindowCreatedEvent;
            if (handler != null) handler(this, hWnd);
        }

        public SystemProcessHookForm()
        {
            // Hook on to the shell
            msgNotify = User32.RegisterWindowMessage("SHELLHOOK");
            User32.RegisterShellHookWindow(Handle);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == msgNotify)
            {
                // Receive shell messages
                switch ((ShellEvents)m.WParam.ToInt32())
                {
                    case ShellEvents.HSHELL_WINDOWCREATED:
                        OnWindowCreated(m.LParam);
                        break;
                    case ShellEvents.HSHELL_WINDOWDESTROYED:
                        OnWindowDestroyed(m.LParam);
                        break;
                }
            }
            base.WndProc(ref m);
        }

        protected override void Dispose(bool disposing)
        {
            try { User32.DeregisterShellHookWindow(Handle); }
            catch { }
            base.Dispose(disposing);
        }
    }
}
