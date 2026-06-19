using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MineDev.MineTrack.Platform.Iam.Application.CommandServices;
using MineDev.MineTrack.Platform.Iam.Application.Internal.OutboundServices;
using MineDev.MineTrack.Platform.Iam.Domain.Model;
using MineDev.MineTrack.Platform.Iam.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Iam.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Iam.Domain.Repositories;
using MineDev.MineTrack.Platform.Shared.Application.Model;
using MineDev.MineTrack.Platform.Shared.Domain.Repositories;
using MineDev.MineTrack.Platform.Shared.Resources.Errors;

namespace MineDev.MineTrack.Platform.Iam.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer
) : IUserCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;

    public async Task<Result<(User user, string token)>> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByEmailAsync(command.Email, cancellationToken);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return Result<(User user, string token)>.Failure(IamError.InvalidCredentials, _localizer[nameof(IamError.InvalidCredentials)]);

        var token = tokenService.GenerateToken(user);

        return Result<(User user, string token)>.Success((user, token));
    }

    public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByEmailAsync(command.Username, cancellationToken))
            return Result.Failure(IamError.UsernameAlreadyTaken, _localizer[nameof(IamError.UsernameAlreadyTaken), command.Username]);

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, command.Email, command.FullName, command.Phone, command.Company, command.Role, hashedPassword);
        
        try
        {
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
        
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            return Result.Failure(IamError.OperationCancelled, _localizer[nameof(IamError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(IamError.DatabaseError, _localizer[nameof(IamError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result.Failure(IamError.InternalServerError, _localizer[nameof(IamError.InternalServerError)]);
        }
    }
}