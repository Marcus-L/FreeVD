using System;

namespace FreeVD.Lib.Interop
{
    public enum HookType : int
    {
        WH_JOURNALRECORD = 0,
        WH_JOURNALPLAYBACK = 1,
        WH_KEYBOARD = 2,
        WH_GETMESSAGE = 3,
        WH_CALLWNDPROC = 4,
        WH_CBT = 5,
        WH_SYSMSGFILTER = 6,
        WH_MOUSE = 7,
        WH_HARDWARE = 8,
        WH_DEBUG = 9,
        WH_SHELL = 10,
        WH_FOREGROUNDIDLE = 11,
        WH_CALLWNDPROCRET = 12,
        WH_KEYBOARD_LL = 13,
        WH_MOUSE_LL = 14
    }

    public enum ShellEvents : int
    {
        HSHELL_WINDOWCREATED = 1,
        HSHELL_WINDOWDESTROYED = 2,
        HSHELL_ACTIVATESHELLWINDOW = 3,
        HSHELL_WINDOWACTIVATED = 4,
        HSHELL_GETMINRECT = 5,
        HSHELL_REDRAW = 6,
        HSHELL_TASKMAN = 7,
        HSHELL_LANGUAGE = 8,
        HSHELL_ACCESSIBILITYSTATE = 11,
        HSHELL_APPCOMMAND = 12
    }
}