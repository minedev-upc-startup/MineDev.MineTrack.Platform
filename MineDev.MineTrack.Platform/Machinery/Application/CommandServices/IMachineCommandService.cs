using MineDev.MineTrack.Platform.Machinery.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Shared.Application.Model;

namespace MineDev.MineTrack.Platform.Machinery.Application.CommandServices;

public interface IMachineCommandService
{
    Task<Result<Machine>> Handle(CreateMachineCommand command, CancellationToken cancellationToken = default);
    Task<Result<Machine>> Handle(UpdateMachineStatusCommand command, CancellationToken cancellationToken = default);
}