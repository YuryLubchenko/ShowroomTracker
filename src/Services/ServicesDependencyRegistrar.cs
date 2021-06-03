using DomainModel.Events;
using DomainModel.Services;
using Microsoft.Extensions.DependencyInjection;
using Services.Events;
using Services.Notifiers;
using Services.Synchronizers;

namespace Services
{
    public static class ServicesDependencyRegistrar
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<IShowroomService, ShowroomService>();
            serviceCollection.AddTransient<IModelSynchronizer, ModelSynchronizer>();
            serviceCollection.AddTransient<ICarSynchronizer, CarSynchronizer>();
            serviceCollection.AddTransient<INotifier, EmailNotifier>();
            serviceCollection.AddTransient<IEmailNotifier, EmailNotifier>();
            serviceCollection.AddTransient<IEventPublisher, EventPublisher>();
        }
    }
}