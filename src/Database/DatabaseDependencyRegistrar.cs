using Database.Repositories;
using DomainModel.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class DatabaseDependencyRegistrar
    {
        public static void RegisterServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton(p =>
                new DbInitializer(p.GetRequiredService<IDbContextFactory<ShowroomContext>>()));

            serviceCollection.AddPooledDbContextFactory<ShowroomContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(DbHelper.ConnectionStringName)));

            serviceCollection.AddTransient<IModelRepository, ModelRepository>();
            serviceCollection.AddTransient<ICarRepository, CarRepository>();
            serviceCollection.AddTransient<IEmailSubscriberRepository, EmailSubscriberRepository>();
        }
    }
}