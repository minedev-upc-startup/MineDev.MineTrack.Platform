using MineDev.MineTrack.Platform.Machinery.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Machinery.Interfaces.Rest.Resources;

namespace MineDev.MineTrack.Platform.Machinery.Interfaces.Rest.Transform;

public static class CreateMachineCommandFromResourceAssembler
{
    public static CreateMachineCommand ToCommandFrom(CreateMachineResource resource)
    {
        return new CreateMachineCommand(
            resource.OwnerId,
            resource.Name,
            resource.Type,
            resource.Brand,
            resource.Model,
            resource.Year,
            resource.HourlyRate,
            resource.DailyMinimumHours,
            resource.Photos ?? [],
            resource.Specs ?? []
        );
    }
}