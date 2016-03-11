using System.Threading.Tasks;

namespace MoM.Module.Interfaces
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
