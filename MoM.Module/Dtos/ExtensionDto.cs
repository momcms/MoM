using MoM.Module.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoM.Module.Dtos
{
    public partial class ExtensionDto
    {
        public int extensionId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ExtensionType type { get; set; }
        public int versionMajor { get; set; }
        public int versionMinor { get; set; }
        public bool isInstalled { get; set; }

        public ExtensionDto() { }

        public ExtensionDto(int ExtensionId, string Name, string Description, ExtensionType Type, int VersionMajor, int VersionMinor, bool IsInstalled)
        {
            extensionId = ExtensionId;
            name = Name;
            description = Description;
            type = Type;
            versionMajor = VersionMajor;
            versionMinor = VersionMinor;
            isInstalled = IsInstalled;
        }
    }
}
