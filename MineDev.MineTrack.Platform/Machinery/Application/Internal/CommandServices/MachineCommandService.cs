using MineDev.MineTrack.Platform.Machinery.Application.CommandServices;
using MineDev.MineTrack.Platform.Machinery.Application.Errors;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.ValueObjects;
using MineDev.MineTrack.Platform.Machinery.Domain.Repositories;
using MineDev.MineTrack.Platform.Shared.Application.Model;
using MineDev.MineTrack.Platform.Shared.Domain.Repositories;

namespace MineDev.MineTrack.Platform.Machinery.Application.Internal.CommandServices;

public class MachineCommandService(
    IMachineRepository machineRepository,
    IUnitOfWork unitOfWork) : IMachineCommandService
{
    public async Task<Result<Machine>> Handle(
        CreateMachineCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            return Result<Machine>.Failure(
                CreateMachineError.InvalidName,
                "Machine name is required.");

        if (command.HourlyRate < 0)
            return Result<Machine>.Failure(
                CreateMachineError.InvalidHourlyRate,
                "Hourly rate cannot be negative.");

        var machine = Machine.Create(command);
        await machineRepository.AddAsync(machine, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Machine>.Success(machine);
    }

    public async Task<Result<Machine>> Handle(
        UpdateMachineStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        if (!MachineStatus.IsValid(command.Status))
            return Result<Machine>.Failure(
                UpdateMachineError.InvalidStatus,
                "Invalid machine status.");

        var machine = await machineRepository.FindByIdAsync(command.MachineId, cancellationToken);
        if (machine is null)
            return Result<Machine>.Failure(
                UpdateMachineError.MachineNotFound,
                "Machine not found.");

        machine.UpdateStatus(command);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Machine>.Success(machine);
    }
}
