using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoM.Module.Dtos;
using System;

namespace MoM.Web.Start
{
    public class SiteSettings
    {
        public static void AddSiteSettings(IServiceCollection services, IConfiguration configuration)
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
            var siteSetting = configuration.GetSection("SiteSetting");
            services.Configure<SiteSettingDto>(options =>
            {
                options.isInstalled = Convert.ToBoolean(configuration["SiteIsInstalled"]);
                options.modulePath = configuration["SiteModulePath"];
                options.title = configuration["SiteTitle"];

                options.theme = new SiteSettingThemeDto
                {
                    module = configuration["SiteThemeModule"],
                    name = configuration["SiteThemeName"]
                };

                options.authentication = new SiteSettingAuthenticationDto
                {
                    facebook = new SiteSettingAuthenticationFacebookDto
                    {
                        appId = configuration["SiteFacebookAppId"],
                        appSecret = configuration["SiteFacebookAppSecret"],
                        enabled = Convert.ToBoolean(configuration["SiteFacebookEnabled"])
                    },
                    google = new SiteSettingAuthenticationGoogleDto
                    {
                        clientId = configuration["SiteGoogleClientAppId"],
                        clientSecret = configuration["SiteGoogleClientSecret"],
                        enabled = Convert.ToBoolean(configuration["SiteGoogleEnabled"])
                    },
                    microsoft = new SiteSettingAuthenticationMicrosoftDto
                    {
                        clientId = configuration["SiteMicrosoftClientId"],
                        clientSecret = configuration["SiteMicrosoftClientSecret"],
                        enabled = Convert.ToBoolean(configuration["SiteMicrosoftEnabled"])
                    },
                    twitter = new SiteSettingAuthenticationTwitterDto
                    {
                        consumerKey = configuration["SiteTwitterConsumerKey"],
                        consumerSecret = configuration["SiteTwitterConsumerSecret"],
                        enabled = Convert.ToBoolean(configuration["SiteTwitterEnabled"])
                    }
                };

                options.email = new SiteSettingEmailDto
                {
                    hostName = configuration["SiteEmailHostName"],
                    password = configuration["SiteEmailPassword"],
                    port = Convert.ToInt32(configuration["SiteEmailPort"]),
                    requireCredentials = Convert.ToBoolean(configuration["SiteEmailRequireCredentials"]),
                    senderEmailAdress = configuration["SiteEmailSenderEmailAdress"],
                    userName = configuration["SiteEmailUserName"],
                    useSSL = Convert.ToBoolean(configuration["SiteEmailUseSSL"])
                };

                options.logo = new SiteSettingLogoDto
                {
                    height = Convert.ToInt32(configuration["SiteLogoHeight"]),
                    imagePath = configuration["SiteLogoImagePath"],
                    svgPath = configuration["SiteLogoSvgPath"],
                    useImageLogo = Convert.ToBoolean(configuration["SiteLogoUseImageLogo"]),
                    useSvgLogo = Convert.ToBoolean(configuration["SiteLogoUseSvgLogo"]),
                    width = Convert.ToInt32(configuration["SiteLogoWidth"])
                };
            });
        }
    }
}
