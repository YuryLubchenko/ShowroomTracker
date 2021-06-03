using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Entities;
using DomainModel.Entities;
using DomainModel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    internal class CarRepository: ICarRepository
    {
        private readonly IDbContextFactory<ShowroomContext> _contextFactory;

        public CarRepository(IDbContextFactory<ShowroomContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<ICar> GetByExternalId(int externalId)
        {
            return await _contextFactory.CreateDbContext().Cars.FirstOrDefaultAsync(x => x.ExternalId == externalId);
        }

        public async Task<ICar> Create(ICar car)
        {
            var context = _contextFactory.CreateDbContext();

            var c = new Car(car);

            context.Cars.Add(c);

            await context.SaveChangesAsync();

            return c;

        }

        public async Task<ICar> Update(ICar car)
        {
            var context = _contextFactory.CreateDbContext();

            var c = await context.Cars.FirstOrDefaultAsync(x => x.Id == car.Id);

            if (c == null)
                return null;

            c.CopyProperties(car);

            await context.SaveChangesAsync();

            return c;
        }

        public async Task<IReadOnlyCollection<ICar>> GetAll()
        {
            return await _contextFactory.CreateDbContext().Cars.AsNoTracking().ToListAsync();
        }

        public async Task MarkAsDeleted(IReadOnlyCollection<ICar> missing)
        {
            var context = _contextFactory.CreateDbContext();

            var missingIds = missing.Select(x => x.Id).ToList();

            foreach (var c in await context.Cars.Where(x => missingIds.Contains(x.Id)).ToListAsync())
            {
                c.Deleted = true;
            }

            await context.SaveChangesAsync();
        }
    }
}