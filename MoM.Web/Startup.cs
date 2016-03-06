using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using MoM.Module.Interfaces;
using MoM.Module.Managers;
using MoM.Web.Managers;
using MoM.Web.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoM.Web
{
    public class Startup
    {
        protected IConfigurationRoot ConfigurationRoot;

        private string ApplicationBasePath;

        private IHostingEnvironment HostingEnvironment;
        private IAssemblyLoaderContainer AssemblyLoaderContainer;
        private IAssemblyLoadContextAccessor AssemblyLoadContextAccessor;
        private ILibraryManager LibraryManager;

        public Startup(IHostingEnvironment hostingEnvironment, IApplicationEnvironment applicationEnvironment, IAssemblyLoaderContainer assemblyLoaderContainer, IAssemblyLoadContextAccessor assemblyLoadContextAccessor, ILibraryManager libraryManager)
        {
            HostingEnvironment = hostingEnvironment;
            ApplicationBasePath = applicationEnvironment.ApplicationBasePath;
            AssemblyLoaderContainer = assemblyLoaderContainer;
            AssemblyLoadContextAccessor = assemblyLoadContextAccessor;
            LibraryManager = libraryManager;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            IEnumerable<Assembly> assemblies = AssemblyManager.GetAssemblies(
              ApplicationBasePath.Substring(0, ApplicationBasePath.LastIndexOf("src")) + "artifacts\\bin\\Extensions",
              AssemblyLoaderContainer,
              AssemblyLoadContextAccessor,
              LibraryManager
            );

            ModuleManager.SetAssemblies(assemblies);

            IFileProvider fileProvider = GetFileProvider(ApplicationBasePath);

            HostingEnvironment.WebRootFileProvider = fileProvider;
            services.AddCaching();
            services.AddSession();
            services.AddMvc().AddPrecompiledRazorViews(ModuleManager.GetAssemblies.ToArray());
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProvider = fileProvider;
            }
            );

            foreach (IModule modules in ModuleManager.GetModules)
            {
                modules.SetConfigurationRoot(ConfigurationRoot);
                modules.ConfigureServices(services);
            }

            services.AddTransient<DefaultAssemblyProvider>();
            services.AddTransient<IAssemblyProvider, ModuleAssemblyProvider>();
        }

        public virtual void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseSession();
            applicationBuilder.UseStaticFiles();

            foreach (IModule modules in ModuleManager.GetModules)
                modules.Configure(applicationBuilder);

            applicationBuilder.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute(name: "Resource", template: "resource", defaults: new { controller = "Resource", action = "Index" });

                foreach (IModule modules in ModuleManager.GetModules)
                    modules.RegisterRoutes(routeBuilder);
            }
            );
        }

        public IFileProvider GetFileProvider(string path)
        {
            IEnumerable<IFileProvider> fileProviders = new IFileProvider[] { new PhysicalFileProvider(path) };

            return new CompositeFileProvider(
              fileProviders.Concat(
                ModuleManager.GetAssemblies.Select(a => new EmbeddedFileProvider(a, a.GetName().Name))
              )
            );
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
