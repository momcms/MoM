using MoM.Module.Interfaces;
using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using Microsoft.AspNet.Hosting;

namespace MoM.Module.Managers
{
    public class MiddlewareManager : IModule
    {
        private IConfigurationRoot ConfigurationRoot;

        public string Name
        {
            get
            {
                return "Data Extension";
            }
        }

        public void SetConfigurationRoot(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Type type = GetIStorageImplementationType();

            if (type != null)
            {
                PropertyInfo connectionStringPropertyInfo = type.GetProperty("ConnectionString");

                if (connectionStringPropertyInfo != null)
                    connectionStringPropertyInfo.SetValue(null, ConfigurationRoot["Data:DefaultConnection:ConnectionString"]);

                PropertyInfo assembliesPropertyInfo = type.GetProperty("Assemblies");

                if (assembliesPropertyInfo != null)
                    assembliesPropertyInfo.SetValue(null, ModuleManager.GetAssemblies);

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
            foreach (Assembly assembly in ModuleManager.GetAssemblies.Where(a => !a.FullName.Contains("Reflection")))
                foreach (Type type in assembly.GetTypes())
                    if (typeof(IDataStorage).IsAssignableFrom(type) && type.GetTypeInfo().IsClass)
                        return type;

            return null;
        }
    }
}
