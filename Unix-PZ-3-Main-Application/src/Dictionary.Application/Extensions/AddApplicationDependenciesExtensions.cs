using Dictionary.Application.Interfaces.UseCases;
using Dictionary.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary.Application.Extensions;

public static class AddApplicationDependenciesExtensions
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUpdateCacheUseCase, UpdateCacheUseCase>();
        serviceCollection.AddScoped<IGetItemFromRedisUseCase, GetItemFromRedisUseCase>();
        serviceCollection.AddScoped<IUpdateInvenItemUseCase, UpdateInvenItemUseCase>();
        
        return serviceCollection;
    }
}