namespace GameLibrary.Domain.Interfaces.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(T obj, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
    void Delete(T obj);
    Task<T?> GetByIdOrDefaultAsync(Guid id, CancellationToken cancellationToken);
}