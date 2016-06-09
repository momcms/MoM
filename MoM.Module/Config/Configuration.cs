using System.ComponentModel.DataAnnotations.Schema;

namespace MoM.Module.Config
{
    [Table("Configuration", Schema = "Core")]
    public class Configuration
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }
}
