using MoM.Module.Enums;

namespace MoM.Module.Dtos
{
    public partial class ClientRouteConfigDto
    {
        public int clientRouteConfigId { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string path { get; set; }
        public string component { get; set; }
        public string importPath { get; set; }
        public bool useAsDefault { get; set; }
        public int sortOrder { get; set; }
        public ClientRouteConfigType type { get; set; }
        public string iconClass { get; set; }

        public ClientRouteConfigDto(
            int ClientRouteConfigId,
            string Name,
            string DisplayName,
            string Path,
            string Component,
            string ImportPath,
            bool UseAsDefault,
            int SortOrder,
            ClientRouteConfigType Type,
            string IconClass
            )
        {
            clientRouteConfigId = ClientRouteConfigId;
            name = Name;
            displayName = DisplayName;
            path = Path;
            component = Component;
            importPath = ImportPath;
            useAsDefault = UseAsDefault;
            sortOrder = SortOrder;
            type = Type;
            iconClass = IconClass;
        }
    }
}
