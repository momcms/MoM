using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MoM.Module.Interfaces
{
    public interface IModule
    {
        string Name { get; }

        void SetConfigurationRoot(IConfigurationRoot configurationRoot);
        void ConfigureServices(IServiceCollection services);
        void Configure(IApplicationBuilder applicationBuilder);
        void RegisterRoutes(IRouteBuilder routeBuilder);
    }
}
