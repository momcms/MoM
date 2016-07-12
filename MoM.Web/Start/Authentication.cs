using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

namespace MoM.Web.Start
{
    public class Authentication
    {
        public static void AddSocialLogins(IApplicationBuilder applicationBuilder, IConfiguration configuration)
        {
            //If they are configured then add social login services
            //https://developers.facebook.com/apps
            if (Convert.ToBoolean(configuration["SiteFacebookEnabled"]))
            {
                applicationBuilder.UseFacebookAuthentication(new FacebookOptions()
                {
                    AppId = configuration["SiteFacebookAppId"],
                    AppSecret = configuration["SiteFacebookAppSecret"]
                });
            }
            //https://console.developers.google.com/
            if (Convert.ToBoolean(configuration["SiteGoogleEnabled"]))
            {
                applicationBuilder.UseGoogleAuthentication(new GoogleOptions()
                {
                    ClientId = configuration["SiteGoogleClientAppId"],
                    ClientSecret = configuration["SiteGoogleClientSecret"]
                });
            }
            //https://msdn.microsoft.com/en-us/library/bb676626.aspx
            if (Convert.ToBoolean(configuration["SiteMicrosoftEnabled"]))
            {
                applicationBuilder.UseMicrosoftAccountAuthentication(new MicrosoftAccountOptions()
                {
                    ClientId = configuration["SiteMicrosoftClientId"],
                    ClientSecret = configuration["SiteMicrosoftClientSecret"]
                });
            }

            if (Convert.ToBoolean(configuration["SiteTwitterEnabled"]))
            {
                applicationBuilder.UseTwitterAuthentication(new TwitterOptions()
                {
                    ConsumerKey = configuration["SiteTwitterConsumerKey"],
                    ConsumerSecret = configuration["SiteTwitterConsumerSecret"]
                });
            }
        }
    }
}
