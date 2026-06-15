namespace MineDev.MineTrack.Platform.Shared.Domain.Repositories;

/// <summary>
///     Base repository interface. All bounded context repositories extend this.
/// </summary>
public interface IBaseRepository<TEntity>
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken = default);
}