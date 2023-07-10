using GameLibrary.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Extensions;

public static class DbMigrationExtension
{
    public static void AddMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetService<AppDbContext>();
        
        if (context == null) throw new Exception("There is no database context");
        
        context.Database.Migrate();
    }
}