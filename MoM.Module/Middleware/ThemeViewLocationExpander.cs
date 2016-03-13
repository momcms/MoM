using Microsoft.AspNet.Mvc.Razor;
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
            var themeModule = Configuration["Site:Theme:Module"];
            var themeSelected = Configuration["Site:Theme:Selected"];
            context.Values["theme"] = themeModule + "/" + themeSelected + "/";
        }

        public virtual IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            foreach(var module in Managers.AssemblyManager.GetModules)
            {

            }
            List<string> result = viewLocations.Select(f => f.Replace("/Views/", "/Views/Themes/" + context.Values["theme"])).ToList();
            foreach (var viewLocation in viewLocations) //keep the fallback paths
            {
                result.Add(viewLocation);
            }
            return result;
        }
    }
}
