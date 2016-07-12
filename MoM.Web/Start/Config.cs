using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoM.Module.Config;

namespace MoM.Web.Start
{
    public class Config
    {
        public static IConfiguration CreateConfiguration(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
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
            configuration = configurationSiteSettingsBuilder.Build();            
            return configuration;
        }
    }
}
