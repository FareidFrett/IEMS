using IEMS_API.Data;
using Microsoft.EntityFrameworkCore;

namespace IEMS_API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        // EF Core (SQLite)
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            var cs = builder.Configuration.GetConnectionString("Default");
            options.UseSqlite(cs);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            // Adds the Swagger UI Dashboard
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });

            // Apply migrations + seed
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DbInitializer");
            await DbInitializer.InitializeAsync(db, logger);
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
