using System.Threading.Tasks;

namespace DomainModel.Services
{
    public interface IEmailNotifier
    {
        Task SendTestEmail(string to);
    }
}