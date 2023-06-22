using Dictionary.Application.Extensions;
using Dictionary.Application.Models;
using Dictionary.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;

namespace Dictionary.API.Extensions;

public static class AddServicesExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection
            .AddInfrastructureDependencies(configuration)
            .AddApplicationDependencies();
        
        return serviceCollection;
    }
    
    public static IServiceCollection AddControllersEndpoints(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllers();
        return serviceCollection;
    }
    
    public static IServiceCollection AddSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Dictionary API",
                Version = "v1"
            });
        });
        return serviceCollection;
    }

    public static IServiceCollection AddFileLoggerService(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // Logging__LogFolderPath
        serviceCollection.Configure<FileSystemLogger.FileLoggerOptions>(configuration.GetSection("Logging"));
        serviceCollection.AddScoped<FileSystemLogger>();

        return serviceCollection;
    }
}