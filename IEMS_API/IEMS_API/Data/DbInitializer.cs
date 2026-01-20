using IEMS_API.Data;
using Microsoft.EntityFrameworkCore;

namespace IEMS_API.Data;
public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext db, ILogger logger)
    {
        // 1) Apply migrations (creates DB if missing)
        await db.Database.MigrateAsync();

        // 2) Seed AssetTypes (idempotent-ish)
        
        logger.LogInformation("Database migration + seeding complete.");
    }
}