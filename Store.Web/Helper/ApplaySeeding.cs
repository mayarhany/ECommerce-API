using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Repository;

namespace Store.Web.Helper
{
    public class ApplaySeeding
    {
        public static async Task ApplaySeedingAsync(WebApplication app)
        {
            using (var scop = app.Services.CreateScope())
            {
                var services = scop.ServiceProvider;

                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<StoreDbContext>();

                    await context.Database.MigrateAsync();

                    await StoreContextSeed.SeedAsync(context, loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<ApplaySeeding>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
