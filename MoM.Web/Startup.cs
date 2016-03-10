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
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.StaticFiles;
using Microsoft.AspNet.Http;

namespace MoM.Web
{
    public class Startup
    {
        protected IConfigurationRoot ConfigurationRoot;

        private string ApplicationBasePath;
        private string ModulePath;

        private IHostingEnvironment HostingEnvironment;
        private IAssemblyLoaderContainer AssemblyLoaderContainer;
        private IAssemblyLoadContextAccessor AssemblyLoadContextAccessor;
        private ILibraryManager LibraryManager;

        public Startup(IHostingEnvironment hostingEnvironment, IApplicationEnvironment applicationEnvironment, IAssemblyLoaderContainer assemblyLoaderContainer, IAssemblyLoadContextAccessor assemblyLoadContextAccessor, ILibraryManager libraryManager)
        {
            HostingEnvironment = hostingEnvironment;
            ApplicationBasePath = applicationEnvironment.ApplicationBasePath;
            ModulePath = ApplicationBasePath.Substring(0, ApplicationBasePath.LastIndexOf("MoM")) + "artifacts\\bin\\Modules";
            AssemblyLoaderContainer = assemblyLoaderContainer;
            AssemblyLoadContextAccessor = assemblyLoadContextAccessor;
            LibraryManager = libraryManager;

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true);


            //builder.SetBasePath(hostingEnvironment.WebRootPath);
            //builder.AddEnvironmentVariables();
            ConfigurationRoot = configurationBuilder.Build();

        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(ConfigurationRoot);

            IEnumerable<Assembly> assemblies = AssemblyManager.GetAssemblies(
              ModulePath,
              AssemblyLoaderContainer,
              AssemblyLoadContextAccessor,
              LibraryManager
            );

            ModuleManager.SetAssemblies(assemblies);

            //IFileProvider fileProvider = HostingEnvironment.WebRootFileProvider;//GetFileProvider(ApplicationBasePath);

            //HostingEnvironment.WebRootFileProvider = fileProvider;
            //HostingEnvironment.WebRootPath = ApplicationBasePath + "\\wwwroot";
            services.AddCaching();

            services.AddMvc().AddPrecompiledRazorViews(ModuleManager.GetAssemblies.ToArray());
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProvider = GetFileProvider(ApplicationBasePath);
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

        public virtual void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(ConfigurationRoot.GetSection("Logging"));
            loggerFactory.AddDebug();

            applicationBuilder.UseApplicationInsightsRequestTelemetry();

            if (hostingEnvironment.IsDevelopment())
            {
                applicationBuilder.UseBrowserLink();
                applicationBuilder.UseDeveloperExceptionPage();
                applicationBuilder.UseDatabaseErrorPage();
            }
            else
            {
                applicationBuilder.UseExceptionHandler("/Error/Index");
            }

            applicationBuilder.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            applicationBuilder.UseApplicationInsightsExceptionTelemetry();
            applicationBuilder.UseDefaultFiles();
            applicationBuilder.UseStaticFiles();

            foreach (IModule modules in ModuleManager.GetModules)
                modules.Configure(applicationBuilder, hostingEnvironment);



            applicationBuilder.UseMvc(routeBuilder =>
            {
                //routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                //routeBuilder.MapRoute("spa-fallback", "{*anything}", new { controller = "Home", action = "Index" });
                //routeBuilder.MapRoute("defaultApi", "api/{controller}/{id?}");
                //routeBuilder.MapRoute(name: "Resource", template: "resource", defaults: new { controller = "Resource", action = "Index" });

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
