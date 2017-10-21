using FreeVD.Lib.Interop;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Management.Deployment;
using WindowsDesktop;

namespace FreeVD
{
    public class AppInfo
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is AppInfo)
            {
                return ((AppInfo)obj).Id == Id;
            }
            return base.Equals(obj);
        }

        private static Regex PackageRegex = new Regex(@"^(.*_\w{13})(!.*){0,1}$");

        public static AppInfo FromWindow(Window window)
        {
            // need the next line to cause VirtualDesktop to Initialize ComObjects. do not delete
            bool placeHolder = VirtualDesktop.IsSupported;

            string appid = window.GetAppId();
            try
            {
                var match = PackageRegex.Match(appid);
                if (match.Success)
                {
                    var pm = new PackageManager();
                    var package = pm.FindPackagesForUser("", match.Groups[1].Value).FirstOrDefault();
                    string name = GetPackageDisplayName(package);
                    return new AppInfo() { Name = name ?? package?.Id.Name ?? appid, Id = appid };
                }
                else
                {
                    var query = $"SELECT ExecutablePath, CommandLine FROM Win32_Process WHERE ProcessId={window.GetProcess().Id}";
                    using (var searcher = new ManagementObjectSearcher(query))
                    using (var results = searcher.Get())
                    {
                        var mo = results.Cast<ManagementObject>().FirstOrDefault();
                        var file = FileVersionInfo.GetVersionInfo((string)mo["ExecutablePath"]);
                        string name = file.FileDescription;
                        if (name == "Google Chrome")
                        {
                            name += $" ({GetChromeAppName(appid)})";
                        }
                        return new AppInfo() { Name = name, Id = appid };
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogEvent("Window", $"Handle: {window.Handle}\nCaption: {window.GetWindowText()}", ex);
            }
            return new AppInfo() { Name = "Error", Id = appid };
        }

        private static string GetPackageDisplayName(Package package)
        {
            using (var capabilitiesKey = Registry.ClassesRoot.OpenSubKey(
                @"Local Settings\Software\Microsoft\Windows\CurrentVersion\AppModel" + 
                $@"\Repository\Packages\{package.Id.FullName}\App\Capabilities"))
            {
                var sb = new StringBuilder(256);
                Shlwapi.SHLoadIndirectString((string)capabilitiesKey.GetValue("ApplicationName"), 
                    sb, sb.Capacity, IntPtr.Zero);
                return sb.ToString();
            }
        }

        private static string GetChromeAppName(string appid)
        {
            string dataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                + @"\Google\Chrome\User Data";

            if (!appid.StartsWith("_crx_") ||
                !Directory.Exists(dataDir))
            {
                return appid;
            }
            // try to search for the extension name
            string extension = appid.Substring(5);
            var profiles = new List<string>(Directory.GetFiles(dataDir, "Profile *")) { "Default" };
            foreach (string profile in profiles)
            {
                string appDir = $"{dataDir}\\{profile}\\Web Applications\\{appid}";
                if (Directory.Exists(appDir))
                {
                    var icon = new FileInfo(Directory.GetFiles(appDir, "*.ico").FirstOrDefault());
                    if (icon != null)
                    {
                        return icon.Name.Substring(0, icon.Name.Length - 4);
                    }
                }
                string extDir = $"{dataDir}\\{profile}\\Extensions\\{extension}";
                if (Directory.Exists(extDir))
                {
                    string manifest = Directory.GetFiles(extDir, "manifest.json",
                        SearchOption.AllDirectories).FirstOrDefault();
                    try
                    {
                        string extname = JsonConvert.DeserializeAnonymousType(
                            File.ReadAllText(manifest), new { Name = "" }).Name;
                        if (extname == "__MSG_app_name__") extname = $"App:{appid}";
                        return extname;
                    }
                    catch { }
                }
            }
            return appid;
        }
    }
}
