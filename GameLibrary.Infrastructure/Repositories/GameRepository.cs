using GameLibrary.Domain.Entities;
using GameLibrary.Domain.Interfaces.Repositories;
using GameLibrary.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Infrastructure.Repositories;

public class GameRepository : IRepository<Game>
{
    private readonly AppDbContext _db;

    public GameRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Games
            .Include(x => x.Studio)
            .Include(x => x.Genres)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Game game, CancellationToken cancellationToken)
    {
        await _db.Games.AddAsync(game, cancellationToken);
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _db.SaveChangesAsync(cancellationToken);
    }

    public void Delete(Game game)
    {
        _db.Games.Remove(game);
    }

    public async Task<Game?> GetByIdOrDefaultAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Games
            .Include(x => x.Genres).Include(x => x.Studio)
            .FirstOrDefaultAsync(game => game.Id == id, cancellationToken);
    }
}