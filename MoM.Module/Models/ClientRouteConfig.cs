using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoM.Module.Models
{
    [Table("ClientRouteConfig", Schema = "Core")]
    public partial class ClientRouteConfig
    {
        public int ClientRouteConfigId { get; set; }
        public ClientRouteConfig Parent { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string DisplayName { get; set; }
        [StringLength(500)]
        public string Path { get; set; }
        [StringLength(100)]
        public string Component { get; set; }
        [StringLength(500)]
        public string ImportPath { get; set; }
        public bool UseAsDefault { get; set; }
        public int SortOrder { get; set; }
        public ClientRouteConfigType Type { get; set; }
        [StringLength(100)]
        public string IconClass { get; set; }
        public bool ShowInMenu { get; set; }
    }

    public enum ClientRouteConfigType { Page, Widget }
}
