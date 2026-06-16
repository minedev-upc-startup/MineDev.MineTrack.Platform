using MineDev.MineTrack.Platform.Rental.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Rental.Interfaces.Rest.Resources;

namespace MineDev.MineTrack.Platform.Rental.Interfaces.Rest.Transform;

public static class RentalRequestResourceFromEntityAssembler
{
    public static RentalRequestResource ToResourceFrom(RentalRequest rentalRequest)
    {
        return new RentalRequestResource(
            rentalRequest.Id,
            rentalRequest.MachineId,
            rentalRequest.ClientId,
            rentalRequest.OwnerId,
            rentalRequest.StartDate,
            rentalRequest.EndDate,
            rentalRequest.Status.ToString(),
            rentalRequest.SubmittedAt,
            rentalRequest.EstimatedTotalCost,
            rentalRequest.RejectionReason,
            rentalRequest.ResolvedAt
        );
    }
}