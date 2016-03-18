using System.ComponentModel.DataAnnotations.Schema;

namespace MoM.Module.Models
{
    [Table("ClientRouteConfig", Schema = "MoM")]
    public partial class ClientRouteConfig
    {
        public int ClientRouteConfigId { get; set; }

        public string Path { get; set; }
        public string Name { get; set; }
        public string Component { get; set; }
        public bool UseAsDefault { get; set; }
        public int SortOrder { get; set; }
        public ClientRouteConfigType Type { get; set; }
    }

    public enum ClientRouteConfigType { Page, Widget }
}
