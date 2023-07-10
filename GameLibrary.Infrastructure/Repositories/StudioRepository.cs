using GameLibrary.Domain.Entities;
using GameLibrary.Domain.Interfaces.Repositories;
using GameLibrary.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Infrastructure.Repositories;

public class StudioRepository : IRepository<Studio>
{
    private readonly AppDbContext _db;

    public StudioRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Studio>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.GameStudios.Include(studio => studio.Games).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Studio studio, CancellationToken cancellationToken)
    {
        await _db.GameStudios.AddAsync(studio, cancellationToken);
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _db.SaveChangesAsync(cancellationToken);
    }

    public void Delete(Studio studio)
    {
        _db.GameStudios.Remove(studio);
    }

    public async Task<Studio?> GetByIdOrDefaultAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.GameStudios.Include(studio => studio.Games)
            .FirstOrDefaultAsync(gameStudio => gameStudio.Id == id, cancellationToken);
    }
}