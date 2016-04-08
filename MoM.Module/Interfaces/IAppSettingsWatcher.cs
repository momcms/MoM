using System.IO;

namespace MoM.Module.Interfaces
{
    public interface IAppSettingsWatcher
    {
        FileSystemWatcher WatchAppSettings();
    }
}
