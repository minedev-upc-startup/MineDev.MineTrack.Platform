using MineDev.MineTrack.Platform.Rental.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Shared.Domain.Repositories;

namespace MineDev.MineTrack.Platform.Rental.Domain.Repositories;

public interface IRentalRequestRepository : IBaseRepository<RentalRequest>
{
    Task<IEnumerable<RentalRequest>> FindByClientIdAsync(int clientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<RentalRequest>> FindByOwnerIdAsync(int ownerId, CancellationToken cancellationToken = default);
}