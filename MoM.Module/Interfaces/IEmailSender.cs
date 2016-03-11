using System.Threading.Tasks;

namespace MoM.Module.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
