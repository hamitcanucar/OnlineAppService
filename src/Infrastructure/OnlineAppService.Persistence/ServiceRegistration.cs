using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineAppService.Persistence.Persistency;

namespace OnlineAppService.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
           serviceCollection.AddPersistency(configuration);
            return serviceCollection;
        }
    }
}
