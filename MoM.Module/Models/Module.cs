using MoM.Module.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoM.Module.Models
{
    [Table("Module", Schema = "Core")]
    public partial class Module
    {        
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ModuleType Type { get; set; }
        public int VersionMajor { get; set; }
        public int VersionMinor { get; set; }
        public bool IsInstalled { get; set; }
    }
}
