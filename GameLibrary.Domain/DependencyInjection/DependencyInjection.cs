using GameLibrary.Domain.Interfaces.Services;
using GameLibrary.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameLibrary.Domain.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IGameService, GameService>();
        return services;
    }
}