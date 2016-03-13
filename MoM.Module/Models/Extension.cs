using MoM.Module.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoM.Module.Models
{
    [Table("Extension", Schema = "Core")]
    public partial class Extension
    {        
        public int ExtensionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ExtensionType Type { get; set; }
        public int VersionMajor { get; set; }
        public int VersionMinor { get; set; }
        public bool IsInstalled { get; set; }
    }
}
