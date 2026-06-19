using MineDev.MineTrack.Platform.Machinery.Domain.Model.Commands;

namespace MineDev.MineTrack.Platform.Machinery.Domain.Model.Aggregates;

public partial class Machine
{
    public static Machine Create(CreateMachineCommand command) => new(command);

    public Machine UpdateStatus(UpdateMachineStatusCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        UpdateStatus(command.Status);
        return this;
    }
}