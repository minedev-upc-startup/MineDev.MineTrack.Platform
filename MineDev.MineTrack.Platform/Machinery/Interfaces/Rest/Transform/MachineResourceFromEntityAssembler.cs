using MineDev.MineTrack.Platform.Machinery.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Machinery.Interfaces.Rest.Resources;

namespace MineDev.MineTrack.Platform.Machinery.Interfaces.Rest.Transform;

public static class MachineResourceFromEntityAssembler
{
    public static MachineResource ToResourceFrom(Machine machine)
    {
        var location = machine.CurrentLat.HasValue && machine.CurrentLng.HasValue
            ? new MachineLocationResource(machine.CurrentLat.Value, machine.CurrentLng.Value)
            : null;

        return new MachineResource(
            machine.Id,
            machine.OwnerId,
            machine.Name,
            machine.Type,
            machine.Brand,
            machine.Model,
            machine.Year,
            machine.HourlyRate,
            machine.DailyMinimumHours,
            machine.Status,
            machine.Photos,
            machine.Specs,
            location
        );
    }
}