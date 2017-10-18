using FreeVD.Lib.Hotkeys;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FreeVD
{
    public enum VDAction
    {
        GoToDesktop,
        MoveWindowToDesktop,
        MoveWindowToNextDesktop,
        MoveWindowToPreviousDesktop,
        TogglePinWindow,
        TogglePinApplication
    }

    public class VDHotkey : Hotkey
    {
        public VDAction Action { get; set; }

        public string ActionString
        {
            get
            {
                string str = Action.Humanize(LetterCasing.Title);
                if (DesktopNumber != 0) str += $" #{DesktopNumber}";
                if (Follow) str += " & Follow";
                return str;
            }
        }

        public int DesktopNumber { get; set; }

        public bool Follow { get; set; }

        public VDHotkey(Keys key, Keys modifiers) : base((uint)key, modifiers)
        {
            Callback = () =>
            {
                switch (Action)
                {
                    case VDAction.GoToDesktop:
                        VirtualDesktopFunctions.GoToDesktop(DesktopNumber);
                        break;
                    case VDAction.MoveWindowToDesktop:
                        VirtualDesktopFunctions.DesktopMove(DesktopNumber, Follow);
                        break;
                    case VDAction.MoveWindowToNextDesktop:
                        VirtualDesktopFunctions.DesktopMoveNext(Follow);
                        break;
                    case VDAction.MoveWindowToPreviousDesktop:
                        VirtualDesktopFunctions.DesktopMovePrevious(Follow);
                        break;
                    case VDAction.TogglePinApplication:
                        VirtualDesktopFunctions.PinApp();
                        break;
                    case VDAction.TogglePinWindow:
                        VirtualDesktopFunctions.PinWindow();
                        break;
                    default:
                        throw new NotImplementedException($"Unhandled action: {Action}");
                }
            };
        }

        public static IEnumerable<VDHotkey> CreateDefaultHotkeys()
        {
            var items = new List<VDHotkey>
            {
                new VDHotkey(Keys.Right, Keys.Alt | Keys.LWin)
                {
                    Action = VDAction.MoveWindowToNextDesktop
                },
                new VDHotkey(Keys.Left, Keys.Alt | Keys.LWin)
                {
                    Action = VDAction.MoveWindowToPreviousDesktop
                },
                new VDHotkey(Keys.Right, Keys.Control | Keys.Alt | Keys.LWin)
                {
                    Action = VDAction.MoveWindowToNextDesktop,
                    Follow = true
                },
                new VDHotkey(Keys.Left, Keys.Control | Keys.Alt | Keys.LWin)
                {
                    Action = VDAction.MoveWindowToPreviousDesktop,
                    Follow = true
                },
                new VDHotkey(Keys.Z, Keys.Control | Keys.LWin)
                {
                    Action = VDAction.TogglePinWindow
                },
                new VDHotkey(Keys.A, Keys.Control | Keys.LWin)
                {
                    Action = VDAction.TogglePinApplication
                }
            };
            return items;
        }

        public static IEnumerable<VDHotkey> CreateDefaultHotkeys_Numpad()
        {
            var items = new List<VDHotkey>();
            for (int i = 1; i < 10; i++)
            {
                Enum.TryParse<Keys>("NumPad" + i, out var key);

                items.Add(new VDHotkey(key, Keys.Control | Keys.LWin)
                {
                    Action = VDAction.GoToDesktop,
                    DesktopNumber = i
                });
                items.Add(new VDHotkey(key, Keys.Alt | Keys.LWin)
                {
                    Action = VDAction.MoveWindowToDesktop,
                    DesktopNumber = i
                });
                items.Add(new VDHotkey(key, Keys.Control | Keys.Alt | Keys.LWin)
                {
                    Action = VDAction.MoveWindowToDesktop,
                    Follow = true,
                    DesktopNumber = i
                });
            }
            return items.OrderBy(i => i.Action.ToString());
        }
    }
}