using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeVD
{
    public static class AppModel
    {
        public static string Version
        {
            get { return typeof(Program).Assembly.GetName().Version.ToString(); }
        }

        public static ObservableCollection<AppInfo> PinnedApps = new ObservableCollection<AppInfo>();
        public static ObservableCollection<Window> PinnedWindows = new ObservableCollection<Window>();
    }
}
