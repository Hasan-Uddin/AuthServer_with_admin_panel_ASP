using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        using OpenIddictDbContext dbContext2 =
            scope.ServiceProvider.GetRequiredService<OpenIddictDbContext>();

        dbContext.Database.Migrate();

        dbContext2.Database.Migrate();
    }
}
