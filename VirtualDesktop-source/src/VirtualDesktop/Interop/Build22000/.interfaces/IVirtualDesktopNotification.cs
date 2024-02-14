using System;
using System.Runtime.InteropServices;
using WindowsDesktop.Interop.Build10240;

namespace WindowsDesktop.Interop.Build22000
{
    [ComImport]
    [Guid("00000000-0000-0000-0000-000000000000") /* replace at runtime */]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVirtualDesktopNotification
    {
        void VirtualDesktopCreated(IVirtualDesktop pDesktop);

        void VirtualDesktopDestroyBegin(IVirtualDesktop pDesktopDestroyed, IVirtualDesktop pDesktopFallback);

        void VirtualDesktopDestroyFailed(IVirtualDesktop pDesktopDestroyed, IVirtualDesktop pDesktopFallback);

        void VirtualDesktopDestroyed(IVirtualDesktop pDesktopDestroyed, IVirtualDesktop pDesktopFallback);

        void VirtualDesktopMoved(IVirtualDesktop pDesktop, int nIndexFrom, int nIndexTo);

        void VirtualDesktopNameChanged(IVirtualDesktop pDesktop, HString chName);

        void ViewVirtualDesktopChanged(IApplicationView pView);

        void CurrentVirtualDesktopChanged(IVirtualDesktop pDesktopOld, IVirtualDesktop pDesktopNew);

        void VirtualDesktopWallpaperChanged(IVirtualDesktop pDesktop, HString chPath);

        void VirtualDesktopSwitched(IVirtualDesktop pDesktop);

        void RemoteVirtualDesktopConnected(IVirtualDesktop pDesktop);
    }

    internal class VirtualDesktopNotification : VirtualDesktopNotificationService.EventListenerBase, IVirtualDesktopNotification
    {
        public void VirtualDesktopCreated(IVirtualDesktop pDesktop)
        {
            this.CreatedCore(pDesktop);
        }

        public void VirtualDesktopDestroyBegin(IVirtualDesktop pDesktopDestroyed, IVirtualDesktop pDesktopFallback)
        {
            this.DestroyBeginCore(pDesktopDestroyed, pDesktopFallback);
        }

        public void VirtualDesktopDestroyFailed(IVirtualDesktop pDesktopDestroyed, IVirtualDesktop pDesktopFallback)
        {
            this.DestroyFailedCore(pDesktopDestroyed, pDesktopFallback);
        }

        public void VirtualDesktopDestroyed(IVirtualDesktop pDesktopDestroyed, IVirtualDesktop pDesktopFallback)
        {
            this.DestroyedCore(pDesktopDestroyed, pDesktopFallback);
        }

        public void VirtualDesktopMoved(IVirtualDesktop pDesktop, int nIndexFrom, int nIndexTo)
        {
            this.MovedCore(pDesktop, nIndexFrom, nIndexTo);
        }

        public void VirtualDesktopNameChanged(IVirtualDesktop pDesktop, HString chName)
        {
            this.RenamedCore(pDesktop, chName);
        }

        public void ViewVirtualDesktopChanged(IApplicationView pView)
        {
            this.ViewChangedCore(pView);
        }

        public void CurrentVirtualDesktopChanged(IVirtualDesktop pDesktopOld, IVirtualDesktop pDesktopNew)
        {
            this.CurrentChangedCore(pDesktopOld, pDesktopNew);
        }

        public void VirtualDesktopWallpaperChanged(IVirtualDesktop pDesktop, HString chPath)
        {
            this.WallpaperChangedCore(pDesktop, chPath);
        }

        public void VirtualDesktopSwitched(IVirtualDesktop pDesktop)
        {

        }

        public void RemoteVirtualDesktopConnected(IVirtualDesktop pDesktop)
        {

        }
    }
}
