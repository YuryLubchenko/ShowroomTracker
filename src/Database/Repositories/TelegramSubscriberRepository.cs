using System;
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

        public async Task<ITelegramSubscriber> Subscribe(long chatId)
        {
            var context = _contextFactory.CreateDbContext();

            var existingSubscription = await context.TelegramSubscribers.FirstOrDefaultAsync(x => x.ChatId == chatId);

            if (existingSubscription == null)
            {
                var s = new TelegramSubscriber
                {
                    ChatId = chatId,
                    CreatedOnUtc = DateTime.UtcNow
                };

                context.TelegramSubscribers.Add(s);

                await context.SaveChangesAsync();

                return s;
            }

            if (existingSubscription.Disabled)
            {
                existingSubscription.Disabled = false;

                await context.SaveChangesAsync();

                return existingSubscription;
            }

            return existingSubscription;
        }

        public async Task<ITelegramSubscriber> Unsubscribe(long chatId)
        {
            var context = _contextFactory.CreateDbContext();

            var existingSubscription = await context.TelegramSubscribers.FirstOrDefaultAsync(x => x.ChatId == chatId);

            if (existingSubscription == null)
                return null;

            existingSubscription.Disabled = true;

            await context.SaveChangesAsync();

            return existingSubscription;

        }
    }
}