using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreeVD.Lib.Interop
{
    public static class WinTabKeyboardHook
    {
		[StructLayout(LayoutKind.Sequential)]
		public struct Kbdllhookstruct
		{
			public int VkCode;
			public int ScanCode;
			public int Flags;
			public int Time;
			public IntPtr Extra;
		}

		private const int HC_ACTION = 0;
		private const int WH_KEYBOARD_LL = 13;
		private const int WM_KEYDOWN = 0x0100;
		private const int VK_TAB = 0x09;

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

		public static IntPtr _hookID = IntPtr.Zero;
		private static LowLevelKeyboardProc proc;
		public static void Initialize()
		{
			proc = new LowLevelKeyboardProc(HookCallback);

			using (var curProcess = Process.GetCurrentProcess())
			{
				using (var curModule = curProcess.MainModule)
				{
					_hookID = SetWindowsHookEx(WH_KEYBOARD_LL, proc, Kernel32.GetModuleHandle(curModule.ModuleName), 0);
				}
			}
		}

		private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode == HC_ACTION)
			{
				var keyInfo = (Kbdllhookstruct)Marshal.PtrToStructure(lParam, typeof(Kbdllhookstruct));
				if ((int)wParam == WM_KEYDOWN
					&& keyInfo.VkCode == VK_TAB
					&& (User32.GetAsyncKeyState(Keys.LWin) < 0 || User32.GetAsyncKeyState(Keys.RWin) < 0))
				{
					Task.Run(() => {
						AppModel.GetAndSaveWindowInFocus();
						AppModel.SavePinnedWindowsPos();
					});
				}
			}

			return (IntPtr)User32.CallNextHookEx(_hookID, nCode, wParam, lParam);
		}
	}
}
