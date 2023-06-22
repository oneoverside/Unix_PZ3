namespace Dictionary.API.Extensions;

public static class ApplicationExtensions
{
    public static WebApplication UseSwaggerInterface(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dictionary API V1");
        });
        return app;
    }
    
    public static WebApplication ConfigureRouting(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.MapControllers();
        return app;
    }
}