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

namespace MoM.Module.Managers
{
    public class ExtensionManager : IModule
    {
        private IConfiguration Configuration;

        public string Name
        {
            get
            {
                return "Data Extension";
            }
        }

        public ExtensionType Type
        {
            get
            {
                return ExtensionType.Module;
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
                    connectionStringPropertyInfo.SetValue(null, Configuration["Data:DefaultConnection:ConnectionString"]);

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
