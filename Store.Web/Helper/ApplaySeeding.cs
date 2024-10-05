using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Data.Entities.IdentityEntities;
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
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();

                    await context.Database.MigrateAsync();

                    await StoreContextSeed.SeedAsync(context, loggerFactory);
                    await StoreIdentityContextSeed.SeedUserAsync(userManager);
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
