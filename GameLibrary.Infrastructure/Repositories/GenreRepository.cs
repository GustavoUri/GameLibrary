using GameLibrary.Domain.Entities;
using GameLibrary.Domain.Interfaces.Repositories;
using GameLibrary.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Infrastructure.Repositories;

public class GenreRepository : IRepository<Genre>
{
    private readonly AppDbContext _db;

    public GenreRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Genre>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Genres.Include(genre => genre.Games).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Genre obj, CancellationToken cancellationToken)
    {
        await _db.Genres.AddAsync(obj, cancellationToken);
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _db.SaveChangesAsync(cancellationToken);
    }

    public void Delete(Genre obj)
    {
        _db.Genres.Remove(obj);
    }

    public async Task<Genre?> GetByIdOrDefaultAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Genres.Include(genre => genre.Games)
            .FirstOrDefaultAsync(genre => genre.Id == id, cancellationToken);
    }
}