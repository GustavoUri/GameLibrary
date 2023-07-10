using GameLibrary.Domain.Entities;
using GameLibrary.Domain.Interfaces.Repositories;
using GameLibrary.Infrastructure.DbContext;
using GameLibrary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameLibrary.Infrastructure.DependencyInjection;

public static class DbDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("GameLibraryDb");
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IRepository<Game>, GameRepository>();
        services.AddScoped<IRepository<Studio>, StudioRepository>();
        services.AddScoped<IRepository<Genre>, GenreRepository>();
        return services;
    }
}