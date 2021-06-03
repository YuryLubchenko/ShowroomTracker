using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Services;
using Telegram.Bot;

namespace Services.Notifiers
{
    internal class TelegramNotifier: INotifier
    {
        public Task Notify(IReadOnlyCollection<ICar> newCars)
        {
            var bot = new TelegramBotClient()
        }
    }
}