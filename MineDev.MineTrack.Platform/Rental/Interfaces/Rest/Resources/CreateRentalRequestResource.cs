namespace MineDev.MineTrack.Platform.Rental.Interfaces.Rest.Resources;

public record CreateRentalRequestResource(
    int MachineId,
    int ClientId,
    int OwnerId,
    DateOnly StartDate,
    DateOnly EndDate
);