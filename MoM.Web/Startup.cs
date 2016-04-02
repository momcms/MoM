using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using MoM.Module.Interfaces;
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
using MoM.Module.Config;
using Microsoft.Extensions.OptionsModel;
using System.IO;

namespace MoM.Web
{
    public class Startup
    {
        protected IConfiguration Configuration;

        private string ApplicationBasePath;

        private IHostingEnvironment HostingEnvironment;
        private IAssemblyLoaderContainer AssemblyLoaderContainer;
        private IAssemblyLoadContextAccessor AssemblyLoadContextAccessor;
        private ILibraryManager LibraryManager;

        public Startup(
            IHostingEnvironment hostingEnvironment, 
            IApplicationEnvironment applicationEnvironment, 
            IAssemblyLoaderContainer assemblyLoaderContainer, 
            IAssemblyLoadContextAccessor assemblyLoadContextAccessor, 
            ILibraryManager libraryManager
            )
        {
            HostingEnvironment = hostingEnvironment;
            ApplicationBasePath = applicationEnvironment.ApplicationBasePath;
            AssemblyLoaderContainer = assemblyLoaderContainer;
            AssemblyLoadContextAccessor = assemblyLoadContextAccessor;
            LibraryManager = libraryManager;
            
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true);


            if (hostingEnvironment.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                // Good documentation here https://docs.asp.net/en/latest/security/app-secrets.html
                configurationBuilder.AddUserSecrets();

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

            string extensionsPath = Configuration["Site:ModulePath"];
            int lastIndex = ApplicationBasePath.LastIndexOf("MoM") == 0 ? ApplicationBasePath.LastIndexOf("src") : ApplicationBasePath.LastIndexOf("MoM");
            // Get assemblies to load as modules
            IEnumerable<Assembly> assemblies = Managers.AssemblyManager.GetAssemblies(
              Path.Combine(ApplicationBasePath.Substring(0, lastIndex < 0 ? 0 : lastIndex) + extensionsPath),
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

            // Set site options to a strongly typed object for easy access
            // Source: https://docs.asp.net/en/latest/fundamentals/configuration.html
            // Instantiate in a class with:
            // IOptions<Site> SiteSettings;
            // public ClassName(IOptions<Site> siteSettings)
            // {
            //    SiteSettings = siteSettings;
            // }
            // Use in method like:
            // var theme = SiteSettings.Value.Theme;
            services.Configure<SiteSettings>(Configuration.GetSection("Site"));

            // Inject each module service methods and database items
            foreach (IModule module in Module.Managers.AssemblyManager.GetModules)
            {
                module.SetConfiguration(Configuration);
                module.ConfigureServices(services);
            }

            //Identity
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Site:ConnectionString"]));
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

            //If they are configured then add social login services
            //https://developers.facebook.com/apps
            if (Configuration["Site:Authentication:Facebook:Enabled"] == "True")
            {
                applicationBuilder.UseFacebookAuthentication(options =>
                {
                    options.AppId = Configuration["Site:Authentication:Facebook:AppId"];
                    options.AppSecret = Configuration["Site:Authentication:Facebook:AppSecret"];
                });
            }
            //https://console.developers.google.com/
            if (Configuration["Site:Authentication:Google:Enabled"] == "True")
            {
                applicationBuilder.UseGoogleAuthentication(options =>
                {
                    options.ClientId = Configuration["Site:Authentication:Google:ClientId"];
                    options.ClientSecret = Configuration["Site:Authentication:Google:ClientSecret"];
                });
            }
            //https://msdn.microsoft.com/en-us/library/bb676626.aspx
            if (Configuration["Site:Authentication:Microsoft:Enabled"] == "True")
            {
                applicationBuilder.UseMicrosoftAccountAuthentication(options =>
                {
                    options.ClientId = Configuration["Site:Authentication:Microsoft:ClientId"];
                    options.ClientSecret = Configuration["Site:Authentication:Microsoft:ClientSecret"];
                });
            }

            if (Configuration["Site:Authentication:Twitter:Enabled"] == "True")
            {
                applicationBuilder.UseTwitterAuthentication(options =>
                {
                    options.ConsumerKey = Configuration["Site:Authentication:Twitter:ConsumerKey"];
                    options.ConsumerSecret = Configuration["Site:Authentication:Twitter:ConsumerSecret"];
                });
            }

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
