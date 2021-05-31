using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class DbHelper
    {
        public const string ConnectionStringName = "ShowroomTracker";

        public static async Task MigrateDbToLatestVersionAsync(this IServiceProvider serviceProvider)
        {
            var dbInitializer = serviceProvider.GetRequiredService<DbInitializer>();

            await dbInitializer.MigrateToLatestVersion();
        }
    }
}