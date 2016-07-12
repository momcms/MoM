using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using MoM.Module.Interfaces;
using MoM.Module.Services;
using MoM.Module.Config;
using MoM.Module.Models;
using MoM.Module.Middleware;
using Microsoft.AspNetCore.Builder;
using MoM.Module.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MoM.Module.Managers;
using MoM.Module.Enums;
using MoM.Web.Start;
using Microsoft.AspNetCore.Http;

namespace MoM.Web
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment;
        public IConfiguration Configuration;
        public InstallationStatus InstallStatus { get; set; }

        private string ApplicationBasePath;        

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
            ApplicationBasePath = hostingEnvironment.ContentRootPath;

            Configuration = Config.CreateConfiguration(HostingEnvironment, Configuration);
            InstallStatus = (InstallationStatus)int.Parse(Configuration["InstallStatusMoM"]);
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddApplicationInsightsTelemetry(Configuration);
            Assemblies.DiscoverAssemblies(HostingEnvironment, Configuration, InstallStatus);
            HostingEnvironment.WebRootFileProvider = FileProvider.CreateCompositeFileProvider(HostingEnvironment);

            //Variation of services.AddMvc();
            Mvc.AddMvcServices(services);
            //services.AddCaching();

            //services.AddGlimpse();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            SiteSettings.AddSiteSettings(services, Configuration);

            // Inject each module service methods and database items
            foreach (IModule module in ExtensionManager.Extensions)
            {   
                module.SetConfiguration(Configuration);
                module.ConfigureServices(services);
            }

            //Create policy based authorizations
            services.AddAuthorization(options =>
            {
                foreach (IModule module in ExtensionManager.Extensions)
                {
                    module.CreatePolicies(options);
                }
            });

            //Add the Administrators to all claims


            //Database running with Identity Context
            services.AddEntityFramework()
                .AddDbContext<ConfigurationContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
                options.Cookies.ApplicationCookie.AutomaticChallenge = false;
                //options.Cookies.ApplicationCookieAuthenticationScheme = "ApplicationCookie";
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddEntityFrameworkStores<ConfigurationContext>()
            .AddDefaultTokenProviders();

            // configure view locations for the custom theme engine
            services.Configure<RazorViewEngineOptions>(options =>
            {
                var fileProviders = options.FileProviders.ToList();
                options.ViewLocationExpanders.Add(new ThemeViewLocationExpander(Configuration));
            });

            // IConfiguration added explicitly
            services.AddSingleton(Configuration);   
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //applicationBuilder.UseApplicationInsightsRequestTelemetry();

            if (hostingEnvironment.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("~/Error/Index/{0}");
                app.UseExceptionHandler("/Error/Index");
            }

            //applicationBuilder.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            // Configure Session.
            //applicationBuilder.UseSession();

            //applicationBuilder.UseApplicationInsightsExceptionTelemetry();

            // Add static files to the request pipeline
            //applicationBuilder.UseDefaultFiles();
            app.UseStaticFiles();

            // Add gzip compression
            //applicationBuilder.UseCompression();

            // Add cookie-based authentication to the request pipeline
            app.UseIdentity();

            // Session must be used before MVC routes.
            //app.UseSession();

            Authentication.AddSocialLogins(app, Configuration);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookie",
                LoginPath = new PathString("/Account/Unauthorized/"),
                AccessDeniedPath = new PathString("/Home/AuthzError/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            // Ensure correct 401 and 403 HttpStatusCodes for authorization
            //applicationBuilder.UseAuthorizeCorrectly();

            // Inject each module config methods
            foreach (IModule module in ExtensionManager.Extensions)
                module.Configure(app);

            // Routes for MVC (note that Angular will also add routes)
            app.UseMvc(routeBuilder =>
            {
                // Inject each module routebuilder methods
                var modules = InstallStatus == InstallationStatus.Installed ?
                    ExtensionManager.Extensions
                    .Where(e => !e.Info.type.Equals(ModuleType.CoreInstaller))
                        .OrderBy(x => x.Info.loadPriority)
                    :
                    ExtensionManager.Extensions
                        .Where(e => e.Info.type.Equals(ModuleType.CoreInstaller))
                        .OrderBy(x => x.Info.loadPriority);
                foreach (IModule module in modules)
                {
                    module.RegisterRoutes(routeBuilder);
                }
                    

                //Base routes for mvc and the angular 2 app
                if(InstallStatus == InstallationStatus.Installed)
                {
                    routeBuilder.MapRoute("default", "{controller=App}/{action=Index}/{id?}");
                }
                
                routeBuilder.MapRoute("error", "{controller=Error}/{action=Index}");
                routeBuilder.MapRoute("defaultApi", "api/{controller}/{id?}");

                //routeBuilder.MapRoute("spa-fallback", "{*anything}", new { controller = "Home", action = "Index" });
            });

            //todo add initial data and run migrations
        }        
    }
}
