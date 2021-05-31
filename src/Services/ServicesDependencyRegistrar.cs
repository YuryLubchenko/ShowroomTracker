using DomainModel.Services;
using Microsoft.Extensions.DependencyInjection;
using Services.Synchronizers;

namespace Services
{
    public static class ServicesDependencyRegistrar
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<IShowroomService, ShowroomService>();

            serviceCollection.AddTransient<IModelSynchronizer, ModelSynchronizer>();
        }
    }
}