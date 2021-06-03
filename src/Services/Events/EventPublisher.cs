using System;
using System.Collections.Generic;
using DomainModel.Entities;
using DomainModel.Events;
using DomainModel.Services;
using Microsoft.Extensions.Logging;

namespace Services.Events
{
    internal class EventPublisher: IEventPublisher
    {
        private readonly IEnumerable<INotifier> _notifiers;
        private readonly ILogger<EventPublisher> _log;

        public EventPublisher(IEnumerable<INotifier> notifiers,
            ILogger<EventPublisher> log)
        {
            _notifiers = notifiers;
            _log = log;
        }

        public void NewCarsAvailable(IReadOnlyCollection<ICar> cars)
        {
            foreach (var notifier in _notifiers)
            {
                try
                {
                    notifier.Notify(cars);
                }
                catch (Exception e)
                {
                    _log.LogError($"Notifier {notifier.GetType()} exception: {e}");
                }
            }
        }
    }
}