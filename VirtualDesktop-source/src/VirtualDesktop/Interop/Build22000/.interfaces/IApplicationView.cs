using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsDesktop.Interop.Build22000.interfaces
{
    [ComImport]
    [Guid("00000000-0000-0000-0000-000000000000") /* replace at runtime */]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IApplicationView
    {
        int SetFocus();
        int SwitchTo();
        int TryInvokeBack(IntPtr /* IAsyncCallback* */ callback);
        int GetThumbnailWindow(out IntPtr hwnd);
        int GetMonitor(out IntPtr /* IImmersiveMonitor */ immersiveMonitor);
        int GetVisibility(out int visibility);
        int SetCloak(ApplicationViewCloakType cloakType, int unknown);

        int GetPosition(ref Guid guid /* GUID for IApplicationViewPosition */,
            out IntPtr /* IApplicationViewPosition** */ position);

        int SetPosition(ref IntPtr /* IApplicationViewPosition* */ position);
        int InsertAfterWindow(IntPtr hwnd);
        int GetExtendedFramePosition(out Rect rect);
        int GetAppUserModelId([MarshalAs(UnmanagedType.LPWStr)] out string id);
        int SetAppUserModelId(string id);
        int IsEqualByAppUserModelId(string id, out int result);
        int GetViewState(out uint state);
        int SetViewState(uint state);
        int GetNeediness(out int neediness);
        int GetLastActivationTimestamp(out ulong timestamp);
        int SetLastActivationTimestamp(ulong timestamp);
        int GetVirtualDesktopId(out Guid guid);
        int SetVirtualDesktopId(ref Guid guid);
        int GetShowInSwitchers(out int flag);
        int SetShowInSwitchers(int flag);
        int GetScaleFactor(out int factor);
        int CanReceiveInput(out bool canReceiveInput);
        int GetCompatibilityPolicyType(out ApplicationViewCompatibilityPolicy flags);
        int SetCompatibilityPolicyType(ApplicationViewCompatibilityPolicy flags);
        int GetSizeConstraints(IntPtr /* IImmersiveMonitor* */ monitor, out Size size1, out Size size2);
        int GetSizeConstraintsForDpi(uint uint1, out Size size1, out Size size2);
        int SetSizeConstraintsForDpi(ref uint uint1, ref Size size1, ref Size size2);
        int OnMinSizePreferencesUpdated(IntPtr hwnd);
        int ApplyOperation(IntPtr /* IApplicationViewOperation* */ operation);
        int IsTray(out bool isTray);
        int IsInHighZOrderBand(out bool isInHighZOrderBand);
        int IsSplashScreenPresented(out bool isSplashScreenPresented);
        int Flash();
        int GetRootSwitchableOwner(out IApplicationView rootSwitchableOwner);
        int EnumerateOwnershipTree(out IObjectArray ownershipTree);
        int GetEnterpriseId([MarshalAs(UnmanagedType.LPWStr)] out string enterpriseId);
        int IsMirrored(out bool isMirrored);
        int Unknown1(out int unknown);
        int Unknown2(out int unknown);
        int Unknown3(out int unknown);
        int Unknown4(out int unknown);
        int Unknown5(out int unknown);
        int Unknown6(int unknown);
        int Unknown7();
        int Unknown8(out int unknown);
        int Unknown9(int unknown);
        int Unknown10(int unknownX, int unknownY);
        int Unknown11(int unknown);
        int Unknown12(out Size size1);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Size
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public enum ApplicationViewCloakType
    {
        AVCT_NONE = 0,
        AVCT_DEFAULT = 1,
        AVCT_VIRTUAL_DESKTOP = 2
    }

    public enum ApplicationViewCompatibilityPolicy
    {
        AVCP_NONE = 0,
        AVCP_SMALL_SCREEN = 1,
        AVCP_TABLET_SMALL_SCREEN = 2,
        AVCP_VERY_SMALL_SCREEN = 3,
        AVCP_HIGH_SCALE_FACTOR = 4
    }
}
