using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreeVD
{
    public class Settings
    {
        private static IsolatedStorageFile Storage = IsolatedStorageFile.GetUserStoreForAssembly();

        public static Settings Default = LoadDefaultSettings();

        [JsonIgnore]
        public bool AutoStart { get; set; }

        public List<VDHotkey> Hotkeys { get; set; } = new List<VDHotkey>();

        [JsonIgnore]
        public List<Window> PinnedWindows = new List<Window>();

        private const string SETTINGS_FILENAME = "settings.json";

        private static Settings LoadDefaultSettings()
        {
            if (Storage.FileExists(SETTINGS_FILENAME))
            {
                try
                {
                    using (var stream = new IsolatedStorageFileStream(SETTINGS_FILENAME, FileMode.Open, Storage))
                    using (var reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();
                        Default = JsonConvert.DeserializeObject<Settings>(json);
                        Default.Hotkeys.ForEach(hotkey => hotkey.Register());
                        Default.AutoStart = GetAutoStart();
                        return Default;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading settings: {ex.Message}");
                }
            }
            return null;
        }

        public static void Reload()
        {
            LoadDefaultSettings();
        }

        public static void Save()
        {
            using (var stream = new IsolatedStorageFileStream(SETTINGS_FILENAME, FileMode.Create, Storage))
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(JsonConvert.SerializeObject(Default));
            }
            SetAutoStart(Default.AutoStart);
        }

        private static bool GetAutoStart()
        {
            using (var rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                string appname = typeof(Program).Assembly.GetName().Name;
                return rk?.GetValue(appname).ToString() == Application.ExecutablePath.ToString();
            }
        }

        private static void SetAutoStart(bool autoStart)
        {
            using (var rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                string appname = typeof(Program).Assembly.GetName().Name;
                if (autoStart)
                {
                    rk.SetValue(appname, Application.ExecutablePath.ToString());
                }
                else
                {
                    rk.DeleteValue(appname, false);
                }
            }
        }

        public static void EnsureDefaultSettings()
        {
            if (Default == null)
            {
                Default = new Settings();
                Default.Hotkeys.AddRange(VDHotkey.CreateDefaultHotkeys_Numpad());
                Default.Hotkeys.AddRange(VDHotkey.CreateDefaultHotkeys());
                Default.Hotkeys.ForEach(hotkey => hotkey.Register());
                Save();
            }
        }
    }
}
