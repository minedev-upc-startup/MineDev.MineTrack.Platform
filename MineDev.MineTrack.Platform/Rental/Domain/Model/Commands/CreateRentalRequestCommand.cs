namespace MineDev.MineTrack.Platform.Rental.Domain.Model.Commands;

public record CreateRentalRequestCommand(
    int MachineId,
    int ClientId,
    int OwnerId,
    DateOnly StartDate,
    DateOnly EndDate
);