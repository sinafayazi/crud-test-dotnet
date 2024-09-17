using Microsoft.EntityFrameworkCore;

namespace CustomerService.Persistence;

internal static class MigrationRunner
{
    public static void RunMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scoped = app.ApplicationServices.CreateScope();
        scoped.ServiceProvider.GetRequiredService<AppDbContext>()
            .Database.Migrate();
    }
}