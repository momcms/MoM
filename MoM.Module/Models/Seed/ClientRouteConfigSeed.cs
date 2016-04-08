using MoM.Module.Enums;
using System.Collections.Generic;

namespace MoM.Module.Models.Seed
{
    public class ClientRouteConfigSeed
    {
        public void Seed()
        {

        }

        public static readonly IEnumerable<ClientRouteConfig> ClientRoutes =
            new List<ClientRouteConfig>
            {
                new ClientRouteConfig { Component = "HomeComponent", DisplayName = "Home", IconClass="fa fa-home", ImportPath="app/pages/home", Name="Home", Path="/", SortOrder=1, Type= ClientRouteConfigType.Page, UseAsDefault= true },
                new ClientRouteConfig { Component = "ServicesComponent", DisplayName = "Services", IconClass="fa fa-ticket", ImportPath="app/pages/services", Name="Services", Path="/services", SortOrder=2, Type= ClientRouteConfigType.Page, UseAsDefault= false },
                new ClientRouteConfig { Component = "AdminComponent", DisplayName = "Administration", IconClass="fa fa-home", ImportPath="app/pages/admin", Name="Admin", Path="/admin/...", SortOrder=10, Type= ClientRouteConfigType.Page, UseAsDefault= false },

                //blog TODO: should be moved to blog module
                new ClientRouteConfig { Component = "BlogComponent", DisplayName = "Blog", IconClass="fa fa-bullhorn", ImportPath="app/modules/MoM.Blog/pages/blog", Name="Blog", Path="/blog", SortOrder=100, Type= ClientRouteConfigType.Page, UseAsDefault= false },
                new ClientRouteConfig { Component = "PostComponent", DisplayName = "Post", IconClass="fa fa-newspaper-o", ImportPath="app/modules/MoM.Blog/pages/post", Name="Post", Path="blog/:year/:month/:urlSlug", SortOrder=101, Type= ClientRouteConfigType.Page, UseAsDefault= false }

                //Admin section: should also be placed in the respective modules rather than here

            };
    }
}
