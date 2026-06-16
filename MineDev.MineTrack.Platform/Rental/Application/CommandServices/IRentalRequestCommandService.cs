using MineDev.MineTrack.Platform.Rental.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Rental.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Shared.Application.Model;

namespace MineDev.MineTrack.Platform.Rental.Application.CommandServices;

public interface IRentalRequestCommandService
{
    Task<Result<RentalRequest>> Handle(CreateRentalRequestCommand command, CancellationToken cancellationToken = default);
    Task<Result<RentalRequest>> Handle(ApproveRentalRequestCommand command, CancellationToken cancellationToken = default);
    Task<Result<RentalRequest>> Handle(RejectRentalRequestCommand command, CancellationToken cancellationToken = default);
}