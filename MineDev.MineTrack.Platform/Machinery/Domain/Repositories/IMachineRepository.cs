using MineDev.MineTrack.Platform.Machinery.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Shared.Domain.Repositories;

namespace MineDev.MineTrack.Platform.Machinery.Domain.Repositories;

public interface IMachineRepository : IBaseRepository<Machine>
{
    Task<IEnumerable<Machine>> FindByOwnerIdAsync(int ownerId, CancellationToken cancellationToken = default);
}