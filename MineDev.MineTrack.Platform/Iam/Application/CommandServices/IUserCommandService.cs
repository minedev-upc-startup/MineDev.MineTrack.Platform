using MineDev.MineTrack.Platform.Iam.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Iam.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Shared.Application.Model;

namespace MineDev.MineTrack.Platform.Iam.Application.CommandServices;

public interface IUserCommandService
{
    Task<Result<(User user, string token)>> Handle(SignInCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken);
}