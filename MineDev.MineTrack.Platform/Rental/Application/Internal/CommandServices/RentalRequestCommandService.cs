using MineDev.MineTrack.Platform.Rental.Application.CommandServices;
using MineDev.MineTrack.Platform.Rental.Application.Errors;
using MineDev.MineTrack.Platform.Rental.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Rental.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Rental.Domain.Repositories;
using MineDev.MineTrack.Platform.Shared.Application.Model;
using MineDev.MineTrack.Platform.Shared.Domain.Repositories;

namespace MineDev.MineTrack.Platform.Rental.Application.Internal.CommandServices;

public class RentalRequestCommandService(
    IRentalRequestRepository rentalRequestRepository,
    IUnitOfWork unitOfWork) : IRentalRequestCommandService
{
    public async Task<Result<RentalRequest>> Handle(
        CreateRentalRequestCommand command,
        CancellationToken cancellationToken = default)
    {
        var rentalRequest = new RentalRequest(command);
        await rentalRequestRepository.AddAsync(rentalRequest, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<RentalRequest>.Success(rentalRequest);
    }

    public async Task<Result<RentalRequest>> Handle(
        ApproveRentalRequestCommand command,
        CancellationToken cancellationToken = default)
    {
        var rentalRequest = await rentalRequestRepository.FindByIdAsync(command.RentalRequestId, cancellationToken);
        if (rentalRequest is null)
            return Result<RentalRequest>.Failure(
                UpdateRentalRequestError.RentalRequestNotFound,
                "Rental request not found.");
        rentalRequest.Approve(command);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<RentalRequest>.Success(rentalRequest);
    }

    public async Task<Result<RentalRequest>> Handle(
        RejectRentalRequestCommand command,
        CancellationToken cancellationToken = default)
    {
        var rentalRequest = await rentalRequestRepository.FindByIdAsync(command.RentalRequestId, cancellationToken);
        if (rentalRequest is null)
            return Result<RentalRequest>.Failure(
                UpdateRentalRequestError.RentalRequestNotFound,
                "Rental request not found.");
        rentalRequest.Reject(command);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<RentalRequest>.Success(rentalRequest);
    }
}