namespace MineDev.MineTrack.Platform.Rental.Interfaces.Rest.Resources;

public record RentalRequestResource(
    int Id,
    int MachineId,
    int ClientId,
    int OwnerId,
    DateOnly StartDate,
    DateOnly EndDate,
    string Status,
    DateTime SubmittedAt,
    decimal EstimatedTotalCost,
    string? RejectionReason,
    DateTime? ResolvedAt
);