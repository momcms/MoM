using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace MoM.Module.Config
{
    public class ConfigurationProvider : Microsoft.Extensions.Configuration.ConfigurationProvider
    {
        public ConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction)
        {
            OptionsAction = optionsAction;
        }

        Action<DbContextOptionsBuilder> OptionsAction { get; }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<ConfigurationContext>();
            OptionsAction(builder);

            using (var dbContext = new ConfigurationContext(builder.Options))
            {
                dbContext.Database.EnsureCreated();
                Data = !dbContext.Configurations.Any()
                    ? CreateAndSaveDefaultValues(dbContext)
                    : dbContext.Configurations.ToDictionary(c => c.Id, c => c.Value);
            }
        }

        private static IDictionary<string, string> CreateAndSaveDefaultValues(
            ConfigurationContext dbContext)
        {
            var configValues = new Dictionary<string, string>
                {
                    { "SiteIsInstalled", "False" },
                    { "SiteModulePath", "Modules" },
                    { "SiteTitle", "MoM" },

                    { "SiteThemeModule", "MoM.Bootstrap" },
                    { "SiteThemeName",  "EasyModules" },  
                                      
                    { "SiteFacebookEnabled", "False" },
                    { "SiteFacebookAppId", "" },
                    { "SiteFacebookAppSecret", "" },
                    { "SiteGoogleEnabled", "False" },
                    { "SiteGoogleClientAppId", "" },
                    { "SiteGoogleClientSecret", "" },
                    { "SiteMicrosoftEnabled", "False" },
                    { "SiteMicrosoftClientId", "" },
                    { "SiteMicrosoftClientSecret", "" },
                    { "SiteTwitterEnabled", "False" },
                    { "SiteTwitterConsumerKey", "" },
                    { "SiteTwitterConsumerSecret", "" },

                    { "SiteEmailHostName", "" },
                    { "SiteEmailPassword", "" },
                    { "SiteEmailPort", "587" },
                    { "SiteEmailRequireCredentials", "True" },
                    { "SiteEmailSenderEmailAdress", "" },
                    { "SiteEmailUserName", "" },
                    { "SiteEmailUseSSL", "False" },

                    { "SiteLogoHeight", "48" },
                    { "SiteLogoImagePath", "" },
                    { "SiteLogoSvgPath", "" },
                    { "SiteLogoUseImageLogo", "True" },
                    { "SiteLogoUseSvgLogo", "True" },
                    { "SiteLogoWidth", "320" }
                };
            dbContext.Configurations.AddRange(configValues
                .Select(kvp => new Configuration { Id = kvp.Key, Value = kvp.Value })
                .ToArray());
            dbContext.SaveChanges();
            return configValues;
        }
    }
}
