using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace MoM.Module.Middleware
{
    public class ThemeViewLocationExpander : IViewLocationExpander
    {
        private IConfiguration Configuration;

        public ThemeViewLocationExpander(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var themeModule = Configuration["SiteThemeModule"];
            var themeName = Configuration["SiteThemeName"];
            context.Values["theme"] = themeModule + "/" + themeName + "/";
        }

        public virtual IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            List<string> result = viewLocations.Select(f => f.Replace("/Views/", "/Views/Themes/" + context.Values["theme"])).ToList();
            foreach (var viewLocation in viewLocations) //keep the fallback paths
            {
                result.Add(viewLocation);
            }
            return result;
        }
    }
}
