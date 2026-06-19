using Microsoft.EntityFrameworkCore;
using MineDev.MineTrack.Platform.Iam.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Iam.Domain.Repositories;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace MineDev.MineTrack.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Email.Equals(email), cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Context.Set<User>().AnyAsync(user => user.Email.Equals(email), cancellationToken);
    }
}