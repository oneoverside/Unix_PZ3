using LogAnalizer.Controllers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.Configure<LogsController.FileSystemOptions>(config.GetSection("FileSystem"));
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.MapControllers();
app.UseRouting();

app.Run();