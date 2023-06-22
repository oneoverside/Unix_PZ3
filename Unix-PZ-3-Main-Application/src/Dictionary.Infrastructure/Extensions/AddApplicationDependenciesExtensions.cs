using Dictionary.Application.Interfaces.Stores;
using Dictionary.Infrastructure.Contexts;
using Dictionary.Infrastructure.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Dictionary.Infrastructure.Extensions;

public static class AddApplicationDependenciesExtensions
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection serviceCollection, IConfiguration config)
    {
        serviceCollection.AddSingleton<IDatabase>(_ =>
        {
            var redisConfiguration = ConfigurationOptions.Parse(config.GetConnectionString("RedisConnection")!);
            var connection = ConnectionMultiplexer.Connect(redisConfiguration);
            return connection.GetDatabase();
        });
        
        serviceCollection.AddDbContext<PostgresDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("PostgresConnection")!));

        serviceCollection.AddScoped<IRedisManager, RedisManager>();
        serviceCollection.AddScoped<IPostgresReadonlyStore, PostgresReadonlyStore>();
        
        return serviceCollection;
    }
}