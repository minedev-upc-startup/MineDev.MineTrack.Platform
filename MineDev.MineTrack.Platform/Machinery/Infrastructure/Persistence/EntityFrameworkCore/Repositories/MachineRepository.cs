using Microsoft.EntityFrameworkCore;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Machinery.Domain.Repositories;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace MineDev.MineTrack.Platform.Machinery.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MachineRepository(AppDbContext context)
    : BaseRepository<Machine>(context), IMachineRepository
{
    public async Task<IEnumerable<Machine>> FindByOwnerIdAsync(
        int ownerId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Machine>()
            .Where(m => m.OwnerId == ownerId)
            .ToListAsync(cancellationToken);
    }
}