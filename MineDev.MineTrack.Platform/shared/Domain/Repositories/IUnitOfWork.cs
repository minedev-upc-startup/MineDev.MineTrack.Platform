namespace MineDev.MineTrack.Platform.Shared.Domain.Repositories;

/// <summary>
///     Unit of Work pattern. Ensures all repository operations within a
///     business transaction are committed or rolled back atomically.
/// </summary>
public interface IUnitOfWork
{
    Task CompleteAsync(CancellationToken cancellationToken = default);
}