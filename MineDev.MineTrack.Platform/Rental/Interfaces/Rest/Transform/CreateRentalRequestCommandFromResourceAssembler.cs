using MineDev.MineTrack.Platform.Rental.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Rental.Interfaces.Rest.Resources;

namespace MineDev.MineTrack.Platform.Rental.Interfaces.Rest.Transform;

public static class CreateRentalRequestCommandFromResourceAssembler
{
    public static CreateRentalRequestCommand ToCommandFrom(CreateRentalRequestResource resource)
    {
        return new CreateRentalRequestCommand(
            resource.MachineId,
            resource.ClientId,
            resource.OwnerId,
            resource.StartDate,
            resource.EndDate
        );
    }
}