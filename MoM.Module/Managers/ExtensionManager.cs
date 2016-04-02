using MoM.Module.Interfaces;
using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using Microsoft.AspNet.Hosting;
using MoM.Module.Enums;
using MoM.Module.Dtos;

namespace MoM.Module.Managers
{
    public class ExtensionManager : IModule
    {
        private IConfiguration Configuration;

        public ExtensionInfoDto Info
        {
            get
            {
                return new ExtensionInfoDto
                {
                    name = "Data Manager",
                    description = "This is a Core class that allows injection from modules and themes to the different startup methods.",
                    authors = "Rolf Veinø Sørensen",
                    iconCss = "fa fa-database",
                    type = ModuleType.Core,
                    versionMajor = 1,
                    versionMinor = 0
                };
            }
        }

        public void SetConfiguration(IConfiguration configuration)
        {
            Configuration= configuration;
        }

        public IConfiguration GetConfiguration()
        {
            return Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Type type = GetIStorageImplementationType();

            if (type != null)
            {
                PropertyInfo connectionStringPropertyInfo = type.GetProperty("ConnectionString");

                if (connectionStringPropertyInfo != null)
                    connectionStringPropertyInfo.SetValue(null, Configuration["Site:ConnectionString"]);

                PropertyInfo assembliesPropertyInfo = type.GetProperty("Assemblies");

                if (assembliesPropertyInfo != null)
                    assembliesPropertyInfo.SetValue(null, AssemblyManager.GetAssemblies);

                services.AddScoped(typeof(IDataStorage), type);
            }
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
        }

        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
        }

        private Type GetIStorageImplementationType()
        {
            foreach (Assembly assembly in AssemblyManager.GetAssemblies.Where(a => !a.FullName.Contains("Reflection")))
                foreach (Type type in assembly.GetTypes())
                    if (typeof(IDataStorage).IsAssignableFrom(type) && type.GetTypeInfo().IsClass)
                        return type;

            return null;
        }
    }
}
