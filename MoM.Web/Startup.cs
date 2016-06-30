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
using MoM.Module.Dtos;
using MoM.Module.Enums;

namespace MoM.Web
{
    public class Startup
    {
        protected IConfiguration Configuration;
        private InstallationStatus InstallStatus { get; set; }

        private string ApplicationBasePath;

        private IHostingEnvironment HostingEnvironment;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
            ApplicationBasePath = hostingEnvironment.ContentRootPath;

            // Create the configuration for the connectionstring
            IConfigurationBuilder configurationConnectionStringBuilder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true);


            if (hostingEnvironment.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                // Good documentation here https://docs.asp.net/en/latest/security/app-secrets.html
                configurationConnectionStringBuilder.AddUserSecrets();
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                //configurationBuilder.AddApplicationInsightsSettings(developerMode: true);
            }


            configurationConnectionStringBuilder.AddEnvironmentVariables();
            IConfiguration configurationConnectionString = configurationConnectionStringBuilder.Build();

            // Create the configuration for the whole site including connectionsstring from appsettings.json
            IConfigurationBuilder configurationSiteSettingsBuilder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true)
                .AddEntityFrameworkConfig(options =>
                    options.UseSqlServer(configurationConnectionString.GetConnectionString("DefaultConnection"))
                    );
            Configuration = configurationSiteSettingsBuilder.Build();
            InstallStatus = (InstallationStatus)int.Parse(Configuration["InstallStatusMoM"]);
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

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            AddSiteSettings(services);

            // Inject each module service methods and database items
            foreach (IModule module in ExtensionManager.Extensions)
            {
                module.SetConfiguration(Configuration);
                module.ConfigureServices(services);
            }

            //Identity
            services.AddEntityFramework()
                //.AddSqlServer()
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

            //add watch to changes in appsettings.json
            //var appConfig = new FileInfo(ApplicationBasePath + "\\appsettings.json");
            
            //services.AddSingleton<IAppSettingsWatcher>(new AppsettingsWatcher(appConfig, Configuration));

            //FileSystemWatcher appSettingsWatcher = services.BuildServiceProvider().GetService<IAppSettingsWatcher>().WatchAppSettings();

            // configure view locations for the custom theme engine
            services.Configure<RazorViewEngineOptions>(options =>
            {
                var fileProviders = options.FileProviders.ToList();
                options.ViewLocationExpanders.Add(new ThemeViewLocationExpander(Configuration));
            });

            //setup iconfig
            //services.AddSingleton<IConfigurationRoot>(Configuration);   // IConfigurationRoot
            services.AddSingleton(Configuration);   // IConfiguration explicitly
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
            //applicationBuilder.UseCompression();

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
                    routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                }
                
                routeBuilder.MapRoute("error", "{controller=Error}/{action=Index}");
                routeBuilder.MapRoute("defaultApi", "api/{controller}/{id?}");

                //routeBuilder.MapRoute("spa-fallback", "{*anything}", new { controller = "Home", action = "Index" });
            });

            //todo add initial data and run migrations
        }

        private void DiscoverAssemblies()
        {
            string extensionsPath = HostingEnvironment.ContentRootPath + "\\" + Configuration["SiteModulePath"];
            IEnumerable<Assembly> assemblies = Managers.AssemblyManager.GetAssemblies(extensionsPath);

            ExtensionManager.SetAssemblies(assemblies, InstallStatus);
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

        private void AddSiteSettings(IServiceCollection services)
        {
            // Set site options to a strongly typed object for easy access
            // Source: https://docs.asp.net/en/latest/fundamentals/configuration.html
            // See also this issue for RC2 https://github.com/aspnet/Home/issues/1193
            // Instantiate in a class with:
            // IOptions<Site> SiteSettings;
            // public ClassName(IOptions<Site> siteSettings)
            // {
            //    SiteSettings = siteSettings;
            // }
            // Use in method like:
            // var theme = SiteSettings.Value.Theme;
            var siteSetting = Configuration.GetSection("SiteSetting");
            services.Configure<SiteSettingDto>(options =>
            {
                options.isInstalled = Convert.ToBoolean(Configuration["SiteIsInstalled"]);
                options.modulePath = Configuration["SiteModulePath"];
                options.title = Configuration["SiteTitle"];
                
                options.theme = new SiteSettingThemeDto
                {
                    module = Configuration["SiteThemeModule"],
                    name = Configuration["SiteThemeName"]
                };

                options.authentication = new SiteSettingAuthenticationDto
                {
                    facebook = new SiteSettingAuthenticationFacebookDto
                    {
                        appId = Configuration["SiteFacebookAppId"],
                        appSecret = Configuration["SiteFacebookAppSecret"],
                        enabled = Convert.ToBoolean(Configuration["SiteFacebookEnabled"])
                    },
                    google = new SiteSettingAuthenticationGoogleDto
                    {
                        clientId = Configuration["SiteGoogleClientAppId"],
                        clientSecret = Configuration["SiteGoogleClientSecret"],
                        enabled = Convert.ToBoolean(Configuration["SiteGoogleEnabled"])
                    },
                    microsoft = new SiteSettingAuthenticationMicrosoftDto
                    {
                        clientId = Configuration["SiteMicrosoftClientId"],
                        clientSecret = Configuration["SiteMicrosoftClientSecret"],
                        enabled = Convert.ToBoolean(Configuration["SiteMicrosoftEnabled"])
                    },
                    twitter = new SiteSettingAuthenticationTwitterDto
                    {
                        consumerKey = Configuration["SiteTwitterConsumerKey"],
                        consumerSecret = Configuration["SiteTwitterConsumerSecret"],
                        enabled = Convert.ToBoolean(Configuration["SiteTwitterEnabled"])
                    }
                };

                options.email = new SiteSettingEmailDto
                {
                    hostName = Configuration["SiteEmailHostName"],
                    password = Configuration["SiteEmailPassword"],
                    port = Convert.ToInt32(Configuration["SiteEmailPort"]),
                    requireCredentials = Convert.ToBoolean(Configuration["SiteEmailRequireCredentials"]),
                    senderEmailAdress = Configuration["SiteEmailSenderEmailAdress"],
                    userName = Configuration["SiteEmailUserName"],
                    useSSL = Convert.ToBoolean(Configuration["SiteEmailUseSSL"])
                };

                options.logo = new SiteSettingLogoDto
                {
                    height = Convert.ToInt32(Configuration["SiteLogoHeight"]),
                    imagePath = Configuration["SiteLogoImagePath"],
                    svgPath = Configuration["SiteLogoSvgPath"],
                    useImageLogo = Convert.ToBoolean(Configuration["SiteLogoUseImageLogo"]),
                    useSvgLogo = Convert.ToBoolean(Configuration["SiteLogoUseSvgLogo"]),
                    width = Convert.ToInt32(Configuration["SiteLogoWidth"])
                };
            });
        }

        private void AddSocialLogins(IApplicationBuilder applicationBuilder)
        {
            //If they are configured then add social login services
            //https://developers.facebook.com/apps
            if (Convert.ToBoolean(Configuration["SiteFacebookEnabled"]))
            {
                applicationBuilder.UseFacebookAuthentication(new FacebookOptions()
                {
                    AppId = Configuration["SiteFacebookAppId"],
                    AppSecret = Configuration["SiteFacebookAppSecret"]
                });
            }
            //https://console.developers.google.com/
            if (Convert.ToBoolean(Configuration["SiteGoogleEnabled"]))
            {
                applicationBuilder.UseGoogleAuthentication(new GoogleOptions()
                {
                    ClientId = Configuration["SiteGoogleClientAppId"],
                    ClientSecret = Configuration["SiteGoogleClientSecret"]
                });
            }
            //https://msdn.microsoft.com/en-us/library/bb676626.aspx
            if (Convert.ToBoolean(Configuration["SiteMicrosoftEnabled"]))
            {
                applicationBuilder.UseMicrosoftAccountAuthentication(new MicrosoftAccountOptions()
                {
                    ClientId = Configuration["SiteMicrosoftClientId"],
                    ClientSecret = Configuration["SiteMicrosoftClientSecret"]
                });
            }

            if (Convert.ToBoolean(Configuration["SiteTwitterEnabled"]))
            {
                applicationBuilder.UseTwitterAuthentication(new TwitterOptions()
                {
                    ConsumerKey = Configuration["SiteTwitterConsumerKey"],
                    ConsumerSecret = Configuration["SiteTwitterConsumerSecret"]
                });
            }
        }
    }
}
