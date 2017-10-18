using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeVD
{
    public class Settings
    {
        public bool AutoStart { get; set; }

        public List<Hotkey> Hotkeys { get; set; }

        #region Load/Save
        private const string SETTINGS_FILENAME = "settings.json";

        public Settings Load()
        {
            if (Program.storage.FileExists(SETTINGS_FILENAME))
            {
                using (var stream = new IsolatedStorageFileStream(SETTINGS_FILENAME, FileMode.Open, Program.storage))
                using (var reader = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<Settings>(reader.ReadToEnd());
                }
            }
            return null;
        }

        public void Save()
        {
            using (var stream = new IsolatedStorageFileStream(SETTINGS_FILENAME, FileMode.OpenOrCreate, Program.storage))
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(JsonConvert.SerializeObject(this));
            }
        }
        #endregion
    }
}
