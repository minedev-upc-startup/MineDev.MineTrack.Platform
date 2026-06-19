using MineDev.MineTrack.Platform.Machinery.Application.QueryServices;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.Queries;
using MineDev.MineTrack.Platform.Machinery.Domain.Repositories;

namespace MineDev.MineTrack.Platform.Machinery.Application.Internal.QueryServices;

public class MachineQueryService(
    IMachineRepository machineRepository) : IMachineQueryService
{
    public async Task<IEnumerable<Machine>> Handle(
        GetAllMachinesQuery query,
        CancellationToken cancellationToken = default)
    {
        return await machineRepository.ListAsync(cancellationToken);
    }

    public async Task<Machine?> Handle(
        GetMachineByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await machineRepository.FindByIdAsync(query.MachineId, cancellationToken);
    }

    public async Task<IEnumerable<Machine>> Handle(
        GetMachinesByOwnerIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await machineRepository.FindByOwnerIdAsync(query.OwnerId, cancellationToken);
    }
}