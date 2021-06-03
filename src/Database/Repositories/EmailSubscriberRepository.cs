using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    internal class EmailSubscriberRepository: IEmailSubscriberRepository
    {
        private readonly IDbContextFactory<ShowroomContext> _contextFactory;

        public EmailSubscriberRepository(IDbContextFactory<ShowroomContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IReadOnlyCollection<IEmailSubscriber>> GetAll()
        {
            return await _contextFactory.CreateDbContext().EmailSubscribers.ToListAsync();
        }

        public async Task<IReadOnlyCollection<IEmailSubscriber>> GetEnabled()
        {
            return await _contextFactory.CreateDbContext().EmailSubscribers.Where(x => !x.Disabled).ToListAsync();
        }
    }
}