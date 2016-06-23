using MoM.Module.Enums;

namespace MoM.Module.Dtos
{
    public partial class ModuleInfoDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public string authors { get; set; }
        public string iconCss { get; set; }
        public ModuleType type { get; set; }
        public int versionMajor { get; set; }
        public int versionMinor { get; set; }
        public int loadPriority { get; set; }

        public ModuleInfoDto() { }

        public ModuleInfoDto(string Name, string Description, string Authors, string IconCss, ModuleType Type, int VersionMajor, int VersionMinor, int LoadPriority)
        {
            name = Name;
            description = Description;
            authors = Authors;
            iconCss = IconCss;
            type = Type;
            versionMajor = VersionMajor;
            versionMinor = VersionMinor;
            loadPriority = LoadPriority;
        }
    }
}
