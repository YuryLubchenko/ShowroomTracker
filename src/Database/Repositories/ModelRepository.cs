using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Entities;
using DomainModel.Entities;
using DomainModel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    internal class ModelRepository: IModelRepository
    {
        private readonly IDbContextFactory<ShowroomContext> _contextFactory;

        public ModelRepository(IDbContextFactory<ShowroomContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IModel> GetByExternalId(int externalId)
        {
            return await _contextFactory.CreateDbContext().Models.FirstOrDefaultAsync(x => x.ExternalId == externalId);
        }

        public async Task<IModel> Create(IModel model)
        {
            var context = _contextFactory.CreateDbContext();

            var m = new Model(model);

            context.Models.Add(m);

            await context.SaveChangesAsync();

            return m;
        }

        public async Task<IModel> Update(IModel model)
        {
            var context = _contextFactory.CreateDbContext();

            var m = await context.Models.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (m == null)
                return null;

            m.CopyProperties(model);

            await context.SaveChangesAsync();

            return m;
        }

        public async Task<IReadOnlyCollection<IModel>> GetAll()
        {
            return await _contextFactory.CreateDbContext().Models.AsNoTracking().ToListAsync();
        }

        public async Task MarkAsDeleted(IReadOnlyCollection<IModel> missing)
        {
            var context = _contextFactory.CreateDbContext();

            var missingIds = missing.Select(x => x.Id).ToList();

            foreach (var m in await context.Models.Where(x => missingIds.Contains(x.Id)).ToListAsync())
            {
                m.Deleted = true;
            }

            await context.SaveChangesAsync();
        }
    }
}