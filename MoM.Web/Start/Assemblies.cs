using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MoM.Module.Enums;
using MoM.Module.Managers;
using System.Collections.Generic;
using System.Reflection;

namespace MoM.Web.Start
{
    public class Assemblies
    {
        public static void DiscoverAssemblies(IHostingEnvironment hostingEnvironment, IConfiguration configuration, InstallationStatus installStatus)
        {
            string extensionsPath = hostingEnvironment.ContentRootPath + "\\" + configuration["SiteModulePath"];
            IEnumerable<Assembly> assemblies = Managers.AssemblyManager.GetAssemblies(extensionsPath);

            ExtensionManager.SetAssemblies(assemblies, installStatus);
        }
    }
}
