using MineDev.MineTrack.Platform.Machinery.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.Queries;

namespace MineDev.MineTrack.Platform.Machinery.Application.QueryServices;

public interface IMachineQueryService
{
    Task<IEnumerable<Machine>> Handle(GetAllMachinesQuery query, CancellationToken cancellationToken = default);
    Task<Machine?> Handle(GetMachineByIdQuery query, CancellationToken cancellationToken = default);
    Task<IEnumerable<Machine>> Handle(GetMachinesByOwnerIdQuery query, CancellationToken cancellationToken = default);
}