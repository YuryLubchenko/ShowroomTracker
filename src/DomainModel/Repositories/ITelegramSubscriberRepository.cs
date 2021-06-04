using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Entities;

namespace DomainModel.Repositories
{
    public interface ITelegramSubscriberRepository
    {
        Task<IReadOnlyCollection<ITelegramSubscriber>> GetAll();

        Task<IReadOnlyCollection<ITelegramSubscriber>> GetEnabled();

        Task<ITelegramSubscriber> Add(ITelegramSubscriber subscriber);

        Task<ITelegramSubscriber> Subscribe(long charId);

        Task<ITelegramSubscriber> Unsubscribe(long chatId);
    }
}