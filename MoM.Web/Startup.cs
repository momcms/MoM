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
using Microsoft.Data.Entity;
using MoM.Module.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using MoM.Module.Services;
using MoM.Module.Extensions;
using MoM.Module.Middleware;
using MoM.Web.Config;

namespace MoM.Web
{
    public class Startup
    {
        protected IConfiguration Configuration;

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


            if (hostingEnvironment.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                //configurationBuilder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                configurationBuilder.AddApplicationInsightsSettings(developerMode: true);
            }

            configurationBuilder.AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();

        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            // Get assemblies to load as modules
            IEnumerable<Assembly> assemblies = Managers.AssemblyManager.GetAssemblies(
              ModulePath,
              AssemblyLoaderContainer,
              AssemblyLoadContextAccessor,
              LibraryManager
            );
            Module.Managers.AssemblyManager.SetAssemblies(assemblies);

            services.AddCaching();

            //services.AddGlimpse();

            // Load MVC and add precompiled views to mvc from the modules
            services.AddMvc().AddPrecompiledRazorViews(Module.Managers.AssemblyManager.GetAssemblies.ToArray());
            services.Configure<RazorViewEngineOptions>(options => {
                options.FileProvider = GetFileProvider(ApplicationBasePath);
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            // Set theme
            services.Configure<Site>(Configuration.GetSection("Site"));

            // Inject each module service methods and database items
            foreach (IModule modules in Module.Managers.AssemblyManager.GetModules)
            {
                modules.SetConfiguration(Configuration);
                modules.ConfigureServices(services);
            }

            //Identity
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
                options.Cookies.ApplicationCookie.AutomaticChallenge = false;
                //options.Cookies.ApplicationCookieAuthenticationScheme = "ApplicationCookie";
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<DefaultAssemblyProvider>();
            services.AddTransient<IAssemblyProvider, ModuleAssemblyProvider>();

            // configure view locations for the custom theme engine
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ThemeViewLocationExpander(Configuration));
            });
        }

        public virtual void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
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
                applicationBuilder.UseStatusCodePagesWithRedirects("~/Error/Index/{0}");
                applicationBuilder.UseExceptionHandler("/Error/Index");
            }

            applicationBuilder.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            // Configure Session.
            //applicationBuilder.UseSession();

            applicationBuilder.UseApplicationInsightsExceptionTelemetry();

            // Add static files to the request pipeline
            applicationBuilder.UseDefaultFiles();
            applicationBuilder.UseStaticFiles();

            // Add gzip compression
            applicationBuilder.UseCompression();

            // Add cookie-based authentication to the request pipeline
            applicationBuilder.UseIdentity();

            // Ensure correct 401 and 403 HttpStatusCodes for authorization
            applicationBuilder.UseAuthorizeCorrectly();

            // Inject each module config methods
            foreach (IModule modules in Module.Managers.AssemblyManager.GetModules)
                modules.Configure(applicationBuilder, hostingEnvironment);

            // Routes for MVC (note that Angular will also add routes)
            applicationBuilder.UseMvc(routeBuilder =>
            {
                //Base routes for mvc and the angular 2 app
                routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routeBuilder.MapRoute("error", "{controller=Error}/{action=Index}");
                routeBuilder.MapRoute("spa-fallback", "{*anything}", new { controller = "Home", action = "Index" });
                routeBuilder.MapRoute("defaultApi", "api/{controller}/{id?}");

                // Inject each module routebuilder methods
                foreach (IModule modules in Module.Managers.AssemblyManager.GetModules)
                    modules.RegisterRoutes(routeBuilder);
            });

            //todo add initial data and run migrations
        }

        public IFileProvider GetFileProvider(string path)
        {
            IEnumerable<IFileProvider> fileProviders = new IFileProvider[] { new PhysicalFileProvider(path) };

            return new CompositeFileProvider(
                fileProviders.Concat(
                Module.Managers.AssemblyManager.GetAssemblies.Select(a => new EmbeddedFileProvider(a, a.GetName().Name))
              )
            );
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
