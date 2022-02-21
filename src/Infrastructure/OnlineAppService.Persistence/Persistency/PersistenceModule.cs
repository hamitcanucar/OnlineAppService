using Microsoft.Extensions.DependencyInjection;
using OnlineAppService.Application.Configuration;
using OnlineAppService.Application.Interfaces.Repository;
using OnlineAppService.Domain.Core;
using OnlineAppService.Persistence.Context;
using OnlineAppService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace OnlineAppService.Persistence.Persistency
{
    public static class PersistenceModule
    {
        private static string _dbConnectionString;
        public static IServiceCollection AddPersistency(this IServiceCollection collection, IConfiguration configuration)
        {
            _dbConnectionString = configuration["ConnectionStrings:ConnectionString"];
            collection.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>(_ => new SqlConnectionFactory(configuration["ConnectionString"]));
            collection.AddScoped<IUnitOfWork, UnitOfWork>();
            collection.AddTransient<IUserRepository, UserRepository>();
            collection.AddDbContext<ApplicationDbContext>((_, builder) =>
            {
                builder.UseSqlServer(_dbConnectionString, optionsBuilder => optionsBuilder.MigrationsAssembly("OnlineAppService.Infrastructure"));
            });
            return collection;
        }          
    }
}