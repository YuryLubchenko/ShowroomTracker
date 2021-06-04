using DomainModel.Settings;

namespace WebApp.Settings
{
    public class TelegramNotifierSettings: ITelegramNotifierSettings
    {
        public string BotToken { get; set; }
    }
}