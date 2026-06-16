using MineDev.MineTrack.Platform.Rental.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Rental.Domain.Model.Queries;

namespace MineDev.MineTrack.Platform.Rental.Application.QueryServices;

public interface IRentalRequestQueryService
{
    Task<IEnumerable<RentalRequest>> Handle(GetAllRentalRequestsQuery query, CancellationToken cancellationToken = default);
    Task<RentalRequest?> Handle(GetRentalRequestByIdQuery query, CancellationToken cancellationToken = default);
    Task<IEnumerable<RentalRequest>> Handle(GetRentalRequestsByClientIdQuery query, CancellationToken cancellationToken = default);
    Task<IEnumerable<RentalRequest>> Handle(GetRentalRequestsByOwnerIdQuery query, CancellationToken cancellationToken = default);
}