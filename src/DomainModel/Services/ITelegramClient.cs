using System.Threading.Tasks;

namespace DomainModel.Services
{
    public interface ITelegramClient
    {
        Task SendTextMessage(long chatId, string message);
    }
}