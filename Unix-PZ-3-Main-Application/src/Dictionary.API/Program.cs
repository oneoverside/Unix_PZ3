using Dictionary.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

var x = builder.Logging;
builder.Services
    .AddDependencies(builder.Configuration)
    .AddFileLoggerService(builder.Configuration)
    .AddControllersEndpoints()
    .AddEndpointsApiExplorer()
    .AddSwagger();

builder.Build()
    .UseSwaggerInterface()
    .ConfigureRouting()
    .Run();
    
    