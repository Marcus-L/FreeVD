using FreeVD.Lib.Interop;
using System;
using System.Threading;
using static FreeVD.Lib.Interop.User32;

namespace FreeVD.Lib.Hooks
{
    public class LocalWindowsHook : IDisposable
    {
        // Internal properties
        protected IntPtr m_hhook = IntPtr.Zero;
        protected HookType m_hookType;
        protected HookProc m_filterFunc;

        public Action<HookEventArgs> Hooked = (args) => { };

        // Class constructor(s)
        public LocalWindowsHook(HookType hook)
        {
            m_hookType = hook;
            m_filterFunc = new HookProc(CoreHookProc);
        }

        // Default filter function
        protected int CoreHookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
                return User32.CallNextHookEx(m_hhook, code, wParam, lParam);

            // Let clients determine what to do
            Hooked(new HookEventArgs
            {
                Code = code,
                wParam = wParam,
                lParam = lParam
            });

            // Yield to the next hook in the chain
            return User32.CallNextHookEx(m_hhook, code, wParam, lParam);
        }

        // Install the hook
        public void Install()
        {
            m_hhook = User32.SetWindowsHookEx(
                m_hookType,
                m_filterFunc,
                IntPtr.Zero,
                (int)AppDomain.GetCurrentThreadId());
            //Thread.CurrentThread.ManagedThreadId);
        }

        // Uninstall the hook
        public void Uninstall()
        {
            User32.UnhookWindowsHookEx(m_hhook);
            m_hhook = IntPtr.Zero;
        }

        public void Dispose()
        {
            if (IsInstalled) Uninstall();
        }

        public bool IsInstalled
        {
            get { return m_hhook != IntPtr.Zero; }
        }

    }

    //public class LocalWindowsHook
    //{
    //    // ************************************************************************
    //    // Internal properties
    //    protected IntPtr m_hhook = IntPtr.Zero;
    //    protected HookProc m_filterFunc = null;
    //    protected HookType m_hookType;
    //    // ************************************************************************

    //    // ************************************************************************
    //    // Event delegate
    //    public delegate void HookEventHandler(object sender, HookEventArgs e);
    //    // ************************************************************************

    //    // ************************************************************************
    //    // Event: HookInvoked 
    //    public event HookEventHandler HookInvoked;
    //    protected void OnHookInvoked(HookEventArgs e)
    //    {
    //        if (HookInvoked != null)
    //            HookInvoked(this, e);
    //    }
    //    // ************************************************************************

    //    // ************************************************************************
    //    // Class constructor(s)
    //    public LocalWindowsHook(HookType hook)
    //    {
    //        m_hookType = hook;
    //        m_filterFunc = new HookProc(this.CoreHookProc);
    //    }
    //    public LocalWindowsHook(HookType hook, HookProc func)
    //    {
    //        m_hookType = hook;
    //        m_filterFunc = func;
    //    }
    //    // ************************************************************************

    //    // ************************************************************************
    //    // Default filter function
    //    protected int CoreHookProc(int code, IntPtr wParam, IntPtr lParam)
    //    {
    //        if (code < 0)
    //            return CallNextHookEx(m_hhook, code, wParam, lParam);

    //        // Let clients determine what to do
    //        HookEventArgs e = new HookEventArgs();
    //        e.Code = code;
    //        e.wParam = wParam;
    //        e.lParam = lParam;
    //        OnHookInvoked(e);

    //        // Yield to the next hook in the chain
    //        return CallNextHookEx(m_hhook, code, wParam, lParam);
    //    }
    //    // ************************************************************************

    //    // ************************************************************************
    //    // Install the hook
    //    public void Install()
    //    {
    //        m_hhook = SetWindowsHookEx(
    //            m_hookType,
    //            m_filterFunc,
    //            IntPtr.Zero,
    //            (int)AppDomain.GetCurrentThreadId());
    //    }
    //    // ************************************************************************

    //    // ************************************************************************
    //    // Uninstall the hook
    //    public void Uninstall()
    //    {
    //        UnhookWindowsHookEx(m_hhook);
    //        m_hhook = IntPtr.Zero;
    //    }
    //    // ************************************************************************

    //    public bool IsInstalled
    //    {
    //        get { return m_hhook != IntPtr.Zero; }
    //    }
    //}
}