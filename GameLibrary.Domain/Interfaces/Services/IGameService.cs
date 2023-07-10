using GameLibrary.Domain.Entities;

namespace GameLibrary.Domain.Interfaces.Services;

public interface IGameService
{
    Task<Game> AddGameAsync(Game game, CancellationToken cancellationToken);
    Task<Game> UpdateGameAsync(Guid id, Game game, CancellationToken cancellationToken);
    Task<Game> DeleteGameAsync(Guid gameId, CancellationToken cancellationToken);
    Task<IEnumerable<Game>> GetGames(CancellationToken cancellationToken, GameFilter? filter = null);
}