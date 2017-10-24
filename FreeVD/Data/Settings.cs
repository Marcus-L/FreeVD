using FreeVD.Lib.Hotkeys;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreeVD
{
    public class Settings
    {
        public static Settings Default = LoadDefaultSettings();

        [JsonIgnore]
        public bool AutoStart { get; set; }

        public List<VDHotkey> Hotkeys { get; set; } = new List<VDHotkey>();

        [JsonIgnore]
        public List<Window> PinnedWindows = new List<Window>();

        private const string SETTINGS_FILENAME = @"m4rc.us\FreeVD\settings.json";

        private static string SettingsFilename()
        {
            string loc = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(loc, SETTINGS_FILENAME);
        }

        private static Settings LoadDefaultSettings()
        {
            string settings_file = SettingsFilename();
            if (File.Exists(settings_file))
            {
                try
                {
                    using (var stream = new FileStream(settings_file, FileMode.Open))
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
            // renumber hotkeys
            for (int i = 0; i < Default.Hotkeys.Count; i++)
            {
                Default.Hotkeys[i].HotkeyID = i + 1;
            }
            Hotkey.MaxID = Default.Hotkeys.Count -1;

            string settings_file = SettingsFilename();
            Directory.CreateDirectory(Path.GetDirectoryName(settings_file));
            using (var stream = new FileStream(settings_file, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(JsonConvert.SerializeObject(Default, Formatting.Indented,
                    new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));
            }
            SetAutoStart(Default.AutoStart);
        }

        private static bool GetAutoStart()
        {
            using (var rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                string appname = typeof(Program).Assembly.GetName().Name;
                return rk?.GetValue(appname)?.ToString() == Application.ExecutablePath.ToString();
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
