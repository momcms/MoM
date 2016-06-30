using Microsoft.Extensions.Options;
using MoM.Module.Config;
using MoM.Module.Interfaces;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MoM.Module.Models;
using MoM.Module.Dtos;

namespace MoM.Module.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        IOptions<SiteSettingDto> SiteSetting;
        public AuthMessageSender(IOptions<SiteSettingDto> siteSetting)
        {
            SiteSetting = siteSetting;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MimeMessage();
            mailMessage.To.Add(new MailboxAddress("Rolf", email));
            mailMessage.From.Add(new MailboxAddress("", SiteSetting.Value.email.senderEmailAdress));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("html") { Text = message };
            using (var client = new SmtpClient())
            {
                var useSSL = SiteSetting.Value.email.useSSL ? MailKit.Security.SecureSocketOptions.SslOnConnect : MailKit.Security.SecureSocketOptions.None;
                    await client.ConnectAsync(SiteSetting.Value.email.hostName, SiteSetting.Value.email.port, useSSL);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    if (SiteSetting.Value.email.requireCredentials)
                    {
                        await client.AuthenticateAsync(SiteSetting.Value.email.userName, SiteSetting.Value.email.password);
                    }                    
                    await client.SendAsync(mailMessage);
                    await client.DisconnectAsync(true);
            }
            
            // Plug in your email service here to send an email.
            //return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
