using Microsoft.Extensions.Configuration;
using MoM.Module.Dtos;
using MoM.Module.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;

namespace MoM.Module.Services
{
    public class AppsettingsWatcher : IAppSettingsWatcher
    {
        public FileInfo FileInfo;
        public IConfiguration Config;
        public static FileSystemWatcher AppSettingsWatcher;

        public AppsettingsWatcher(FileInfo fileInfo, IConfiguration config)
        {
            FileInfo = fileInfo;
            Config = config;
        }

        public FileSystemWatcher WatchAppSettings()
        {
            var lastUpdated = File.GetLastWriteTime(FileInfo.FullName);
            AppSettingsWatcher = new FileSystemWatcher(FileInfo.DirectoryName, FileInfo.Name);
            AppSettingsWatcher.NotifyFilter = NotifyFilters.LastWrite;
            AppSettingsWatcher.Changed += delegate
            {
                DateTime lastWriteTime = File.GetLastWriteTime(FileInfo.FullName);
                if(lastWriteTime.Subtract(lastUpdated).TotalMilliseconds > 100)
                {
                    //appsettings.json have changed do stuff
                    AppSettingsChanged();
                }

                lastUpdated = lastWriteTime;
            };
            AppSettingsWatcher.EnableRaisingEvents = true;

            return AppSettingsWatcher;
        }

        public void AppSettingsChanged()
        {
            var read = File.ReadAllText(FileInfo.FullName);
            dynamic json = JsonConvert.DeserializeObject(read);
            SiteSettingDto appSettingsJson = json.Site.ToObject<SiteSettingDto>();
            var site = Config.GetSection("Site");
            var test = site.Value;
            //site.Value = appSettingsJson;
            foreach(var child in site.GetChildren())
            {
                var t = child.Value;
            }
        }
    }
}
