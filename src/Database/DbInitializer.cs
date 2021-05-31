using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    internal class DbInitializer
    {
        private readonly IDbContextFactory<ShowroomContext> _contextFactory;

        public DbInitializer(IDbContextFactory<ShowroomContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task MigrateToLatestVersion()
        {
            var context = _contextFactory.CreateDbContext();

            await context.Database.MigrateAsync();
        }
    }
}