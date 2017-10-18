using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeVD
{
    public class Settings
    {
        private static IsolatedStorageFile Storage = IsolatedStorageFile.GetUserStoreForAssembly();

        public static Settings Default = LoadDefaultSettings();

        public bool AutoStart { get; set; }

        public List<VDHotkey> Hotkeys { get; set; } = new List<VDHotkey>();

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
                        Debug.WriteLine("loaded settings: " + json);
                        Default.Hotkeys.ForEach(hotkey => hotkey.Register());
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
        }
    }
}
