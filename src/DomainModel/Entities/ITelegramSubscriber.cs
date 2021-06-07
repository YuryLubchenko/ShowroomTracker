using System;

namespace DomainModel.Entities
{
    public interface ITelegramSubscriber
    {
        int Id { get; }

        DateTime CreatedOnUtc { get; }

        long ChatId { get; }

        bool Disabled { get; }
    }
}