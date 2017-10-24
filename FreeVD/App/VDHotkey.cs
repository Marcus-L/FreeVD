using FreeVD.Lib.Hotkeys;
using Humanizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsDesktop;

namespace FreeVD
{
    public enum VDAction
    {
        GoToDesktop = 0,
        MoveWindowToDesktop = 1,
        MoveWindowToNextDesktop = 2,
        MoveWindowToPreviousDesktop = 3,
        TogglePinWindow = 4,
        TogglePinApplication = 5
    }

    public class VDHotkey : Hotkey
    {
        public VDAction Action { get; set; }

        [JsonIgnore]
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

        public VDHotkey()
        {
            Callback = () =>
            {
                // non-window Actions
                switch (Action)
                {
                    case VDAction.GoToDesktop:
                        VDExtensions.GotoDesktop(DesktopNumber);
                        return;
                }
                // window Actions
                var window = Window.GetForegroundWindow();

                // skip non-movable windows
                if (window.IsDesktop || window.DesktopNumber == -1) return;

                // perform action
                switch (Action)
                {
                    case VDAction.MoveWindowToDesktop:
                        window.MoveToDesktop(DesktopNumber).Follow(Follow);
                        break;
                    case VDAction.MoveWindowToNextDesktop:
                        window.MoveToNextDesktop().Follow(Follow);
                        break;
                    case VDAction.MoveWindowToPreviousDesktop:
                        window.MoveToPreviousDesktop().Follow(Follow);
                        break;
                    case VDAction.TogglePinApplication:
                        window.TogglePinApp();
                        break;
                    case VDAction.TogglePinWindow:
                        window.TogglePinWindow();
                        break;
                    default:
                        throw new NotImplementedException($"Unhandled action: {Action}");
                }
            };
        }

        public VDHotkey(Keys key, Keys modifiers) : base((uint)key, modifiers)
        {
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
                new VDHotkey(Keys.W, Keys.Control | Keys.LWin)
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
            return items;
        }
    }
}