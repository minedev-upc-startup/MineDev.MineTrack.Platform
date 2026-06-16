using MineDev.MineTrack.Platform.Rental.Application.QueryServices;
using MineDev.MineTrack.Platform.Rental.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Rental.Domain.Model.Queries;
using MineDev.MineTrack.Platform.Rental.Domain.Repositories;

namespace MineDev.MineTrack.Platform.Rental.Application.Internal.QueryServices;

public class RentalRequestQueryService(
    IRentalRequestRepository rentalRequestRepository) : IRentalRequestQueryService
{
    public async Task<IEnumerable<RentalRequest>> Handle(
        GetAllRentalRequestsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await rentalRequestRepository.ListAsync(cancellationToken);
    }

    public async Task<RentalRequest?> Handle(
        GetRentalRequestByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await rentalRequestRepository.FindByIdAsync(query.RentalRequestId, cancellationToken);
    }

    public async Task<IEnumerable<RentalRequest>> Handle(
        GetRentalRequestsByClientIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await rentalRequestRepository.FindByClientIdAsync(query.ClientId, cancellationToken);
    }

    public async Task<IEnumerable<RentalRequest>> Handle(
        GetRentalRequestsByOwnerIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await rentalRequestRepository.FindByOwnerIdAsync(query.OwnerId, cancellationToken);
    }
}