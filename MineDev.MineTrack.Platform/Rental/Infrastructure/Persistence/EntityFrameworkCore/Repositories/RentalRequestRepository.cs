using Microsoft.EntityFrameworkCore;
using MineDev.MineTrack.Platform.Rental.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Rental.Domain.Repositories;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace MineDev.MineTrack.Platform.Rental.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class RentalRequestRepository(AppDbContext context)
    : BaseRepository<RentalRequest>(context), IRentalRequestRepository
{
    public async Task<IEnumerable<RentalRequest>> FindByClientIdAsync(
        int clientId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<RentalRequest>()
            .Where(r => r.ClientId == clientId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<RentalRequest>> FindByOwnerIdAsync(
        int ownerId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<RentalRequest>()
            .Where(r => r.OwnerId == ownerId)
            .ToListAsync(cancellationToken);
    }
}