using MineDev.MineTrack.Platform.Iam.Application.QueryServices;
using MineDev.MineTrack.Platform.Iam.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Iam.Domain.Model.Queries;
using MineDev.MineTrack.Platform.Iam.Domain.Repositories;

namespace MineDev.MineTrack.Platform.Iam.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.ListAsync(cancellationToken);
    }

    public async Task<User?> Handle(GetUserByUsernameQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.FindByUsernameAsync(query.Username, cancellationToken);
    }
}