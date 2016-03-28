using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoM.Module.Enums;

namespace MoM.Module.Interfaces
{
    public interface IModule
    {
        string Name { get; }
        ModuleType Type { get; }

        void SetConfiguration(IConfiguration configuration);
        IConfiguration GetConfiguration();

        void ConfigureServices(IServiceCollection services);
        void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment);
        void RegisterRoutes(IRouteBuilder routeBuilder);
    }
}
