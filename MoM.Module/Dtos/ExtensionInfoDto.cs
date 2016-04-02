using MoM.Module.Enums;

namespace MoM.Module.Dtos
{
    public partial class ExtensionInfoDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public string authors { get; set; }
        public string iconCss { get; set; }
        public ModuleType type { get; set; }
        public int versionMajor { get; set; }
        public int versionMinor { get; set; }

        public ExtensionInfoDto() { }

        public ExtensionInfoDto(string Name, string Description, string Authors, string IconCss, ModuleType Type, int VersionMajor, int VersionMinor)
        {
            name = Name;
            description = Description;
            authors = Authors;
            iconCss = IconCss;
            type = Type;
            versionMajor = VersionMajor;
            versionMinor = VersionMinor;
        }
    }
}
