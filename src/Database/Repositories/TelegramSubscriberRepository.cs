using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Entities;
using DomainModel.Entities;
using DomainModel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    internal class TelegramSubscriberRepository: ITelegramSubscriberRepository
    {
        private readonly IDbContextFactory<ShowroomContext> _contextFactory;

        public TelegramSubscriberRepository(IDbContextFactory<ShowroomContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IReadOnlyCollection<ITelegramSubscriber>> GetAll()
        {
            return await _contextFactory.CreateDbContext().TelegramSubscribers.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyCollection<ITelegramSubscriber>> GetEnabled()
        {
            return await _contextFactory.CreateDbContext().TelegramSubscribers.AsNoTracking().Where(x => !x.Disabled)
                .ToListAsync();
        }

        public async Task<ITelegramSubscriber> Add(ITelegramSubscriber subscriber)
        {
            var s = new TelegramSubscriber(subscriber);

            var context = _contextFactory.CreateDbContext();

            context.TelegramSubscribers.Add(s);

            await context.SaveChangesAsync();

            return s;
        }

        public Task<ITelegramSubscriber> Subscribe(long charId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ITelegramSubscriber> Unsubscribe(long chatId)
        {
            throw new System.NotImplementedException();
        }
    }
}