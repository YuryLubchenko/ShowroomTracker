using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;
using DomainModel.Services;
using Microsoft.Extensions.Logging;

namespace Services.Notifiers
{
    internal class TelegramNotifier: INotifier
    {
        private readonly ITelegramClient _telegramClient;
        private readonly ITelegramSubscriberRepository _telegramSubscriberRepository;
        private readonly ILogger<TelegramNotifier> _log;

        public TelegramNotifier(ITelegramClient telegramClient,
            ITelegramSubscriberRepository telegramSubscriberRepository,
            ILogger<TelegramNotifier> logger)
        {
            _telegramClient = telegramClient;
            _telegramSubscriberRepository = telegramSubscriberRepository;
            _log = logger;
        }

        public async Task Notify(IReadOnlyCollection<ICar> newCars)
        {
            var subscribers = await _telegramSubscriberRepository.GetEnabled();

            var sb = new StringBuilder();

            sb.AppendLine("New Hyundai cars are available!");

            foreach (var newCar in newCars)
            {
                sb.AppendLine($"Model: <a href=\"https://showroom.hyundai.ru/model/{newCar.ExternalId}\">{newCar.ModelName}</a>, price: {newCar.Price}");
            }

            foreach (var subscriber in subscribers)
            {
                try
                {
                    await _telegramClient.SendTextMessage(subscriber.ChatId, sb.ToString());
                }
                catch (Exception e)
                {
                    _log.LogError($"TelegramNotifier send message exception: {e}");
                }
            }
        }
    }
}