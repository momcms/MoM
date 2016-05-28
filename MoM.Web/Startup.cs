using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.Web.CodeGeneration.DotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using MoM.Module.Interfaces;
using MoM.Module.Services;
using MoM.Module.Config;
using MoM.Module.Models;
using MoM.Module.Middleware;
using Microsoft.AspNetCore.Builder;
using MoM.Module.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using System;
using MoM.Module.Managers;

namespace MoM.Web
{
    public class Startup
    {
        protected IConfiguration Configuration;

        private string ApplicationBasePath;

        private IHostingEnvironment HostingEnvironment;
        private ILibraryManager LibraryManager;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
            
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true);


            if (hostingEnvironment.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                // Good documentation here https://docs.asp.net/en/latest/security/app-secrets.html
                configurationBuilder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                //configurationBuilder.AddApplicationInsightsSettings(developerMode: true);
            }
            

            configurationBuilder.AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();

        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddApplicationInsightsTelemetry(Configuration);
            DiscoverAssemblies();
            HostingEnvironment.WebRootFileProvider = CreateCompositeFileProvider();
            AddMvcServices(services);
            //services.AddCaching();

            //services.AddGlimpse();

            // Load MVC and add precompiled views to mvc from the modules
            //services.AddMvc();//.AddPrecompiledRazorViews(Module.Managers.AssemblyManager.GetAssemblies.ToArray());
            //services.Configure<RazorViewEngineOptions>(options =>
            //{
            //    options.FileProvider = GetFileProvider(ApplicationBasePath);
            //});

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

            //services.Configure<SiteSettings>(Configuration.GetSection("Site"));

            services.Configure<SiteSettings>(options =>
            {
                options.Theme = new Theme { Module = Configuration["Site:Theme:Module"], Selected = Configuration["Site:Theme:Selected"] };
                options.Title = Configuration["Site:Title"];
            });

            // Inject each module service methods and database items
            foreach (IModule module in ExtensionManager.Extensions)
            {
                module.SetConfiguration(Configuration);
                module.ConfigureServices(services);
            }

            //Identity
            services.AddEntityFramework()
                //.AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Site:ConnectionString"]));
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
                options.Cookies.ApplicationCookie.AutomaticChallenge = false;
                //options.Cookies.ApplicationCookieAuthenticationScheme = "ApplicationCookie";
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            //services.AddTransient<DefaultAssemblyProvider>();
            //services.AddTransient<IAssemblyProvider, ModuleAssemblyProvider>();

            //add watch to changes in appsettings.json
            var appConfig = new FileInfo(ApplicationBasePath + "\\appsettings.json");
            
            services.AddSingleton<IAppSettingsWatcher>(new AppsettingsWatcher(appConfig, Configuration));

            FileSystemWatcher appSettingsWatcher = services.BuildServiceProvider().GetService<IAppSettingsWatcher>().WatchAppSettings();

            // configure view locations for the custom theme engine
            services.Configure<RazorViewEngineOptions>(options =>
            {
                var fileProviders = options.FileProviders.ToList();
                options.ViewLocationExpanders.Add(new ThemeViewLocationExpander(Configuration));
            });
        }

        public virtual void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //applicationBuilder.UseApplicationInsightsRequestTelemetry();

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

            //applicationBuilder.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            // Configure Session.
            //applicationBuilder.UseSession();

            //applicationBuilder.UseApplicationInsightsExceptionTelemetry();

            // Add static files to the request pipeline
            //applicationBuilder.UseDefaultFiles();
            applicationBuilder.UseStaticFiles();

            // Add gzip compression
            applicationBuilder.UseCompression();

            // Add cookie-based authentication to the request pipeline
            applicationBuilder.UseIdentity();

            AddSocialLogins(applicationBuilder);

            // Ensure correct 401 and 403 HttpStatusCodes for authorization
            applicationBuilder.UseAuthorizeCorrectly();

            // Inject each module config methods
            foreach (IModule module in ExtensionManager.Extensions)
                module.Configure(applicationBuilder);

            // Routes for MVC (note that Angular will also add routes)
            applicationBuilder.UseMvc(routeBuilder =>
            {
                //Base routes for mvc and the angular 2 app
                routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routeBuilder.MapRoute("error", "{controller=Error}/{action=Index}");
                routeBuilder.MapRoute("spa-fallback", "{*anything}", new { controller = "Home", action = "Index" });
                routeBuilder.MapRoute("defaultApi", "api/{controller}/{id?}");

                // Inject each module routebuilder methods
                foreach (IModule module in ExtensionManager.Extensions)
                    module.RegisterRoutes(routeBuilder);
            });

            //todo add initial data and run migrations
        }

        private void DiscoverAssemblies()
        {
            int lastIndex = HostingEnvironment.ContentRootPath.LastIndexOf("MoM") == 0 ? HostingEnvironment.ContentRootPath.LastIndexOf("src") : HostingEnvironment.ContentRootPath.LastIndexOf("MoM");
            string extensionsPath = HostingEnvironment.ContentRootPath.Substring(0, lastIndex < 0 ? 0 : lastIndex) + Configuration["Site:ModulePath"] + "\\";
            
            //string extensionsPath = this.hostingEnvironment.ContentRootPath + this.configurationRoot["Extensions:Path"];
            IEnumerable<Assembly> assemblies = Managers.AssemblyManager.GetAssemblies(extensionsPath);

            ExtensionManager.SetAssemblies(assemblies);
        }

        private void AddMvcServices(IServiceCollection services)
        {
            IMvcBuilder mvcBuilder = services.AddMvc();
            List<MetadataReference> metadataReferences = new List<MetadataReference>();

            foreach (Assembly assembly in ExtensionManager.Assemblies)
            {
                mvcBuilder.AddApplicationPart(assembly);
                metadataReferences.Add(MetadataReference.CreateFromFile(assembly.Location));
            }

            mvcBuilder.AddRazorOptions(
              o =>
              {
                  foreach (Assembly assembly in ExtensionManager.Assemblies)
                      o.FileProviders.Add(new EmbeddedFileProvider(assembly, assembly.GetName().Name));

                  Action<RoslynCompilationContext> previous = o.CompilationCallback;

                  o.CompilationCallback = c =>
                  {
                      previous?.Invoke(c);

                      c.Compilation = c.Compilation.AddReferences(metadataReferences);
                  };
              }
            );
        }

        private IFileProvider CreateCompositeFileProvider()
        {
            IEnumerable<IFileProvider> fileProviders = new IFileProvider[] {
                HostingEnvironment.WebRootFileProvider
            };

            return new Providers.CompositeFileProvider(
              fileProviders.Concat(
                ExtensionManager.Assemblies.Select(a => new EmbeddedFileProvider(a, a.GetName().Name))
              )
            );
        }

        private void AddSocialLogins(IApplicationBuilder applicationBuilder)
        {
            //If they are configured then add social login services
            //https://developers.facebook.com/apps
            if (Configuration["Site:Authentication:Facebook:Enabled"] == "True")
            {
                applicationBuilder.UseFacebookAuthentication(new FacebookOptions()
                {
                    AppId = Configuration["Site:Authentication:Facebook:AppId"],
                    AppSecret = Configuration["Site:Authentication:Facebook:AppSecret"]
                });
            }
            //https://console.developers.google.com/
            if (Configuration["Site:Authentication:Google:Enabled"] == "True")
            {
                applicationBuilder.UseGoogleAuthentication(new GoogleOptions()
                {
                    ClientId = Configuration["Site:Authentication:Google:ClientId"],
                    ClientSecret = Configuration["Site:Authentication:Google:ClientSecret"]
                });
            }
            //https://msdn.microsoft.com/en-us/library/bb676626.aspx
            if (Configuration["Site:Authentication:Microsoft:Enabled"] == "True")
            {
                applicationBuilder.UseMicrosoftAccountAuthentication(new MicrosoftAccountOptions()
                {
                    ClientId = Configuration["Site:Authentication:Microsoft:ClientId"],
                    ClientSecret = Configuration["Site:Authentication:Microsoft:ClientSecret"]
                });
            }

            if (Configuration["Site:Authentication:Twitter:Enabled"] == "True")
            {
                applicationBuilder.UseTwitterAuthentication(new TwitterOptions()
                {
                    ConsumerKey = Configuration["Site:Authentication:Twitter:ConsumerKey"],
                    ConsumerSecret = Configuration["Site:Authentication:Twitter:ConsumerSecret"]
                });
            }
        }
    }
}
