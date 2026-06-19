using MineDev.MineTrack.Platform.Iam.Application.CommandServices;
using MineDev.MineTrack.Platform.Iam.Application.QueryServices;
using MineDev.MineTrack.Platform.Iam.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Iam.Domain.Model.Queries;
using MineDev.MineTrack.Platform.Iam.Interfaces.Acl;

namespace MineDev.MineTrack.Platform.Iam.Application.Acl;

public class IamContextFacade(IUserCommandService userCommandService, IUserQueryService userQueryService) : IIamContextFacade
{
    public async Task<int> CreateUser(string username, string email, string fullName, string phone, string company, string role, string password, CancellationToken cancellationToken)
    {
        var signUpCommand = new SignUpCommand(username, email, fullName, phone, company, role, password);
        var signUpResult = await userCommandService.Handle(signUpCommand, cancellationToken);
        
        if (signUpResult.IsFailure) return 0;
        
        var getUserByEmailQuery = new GetUserByEmailQuery(username);
        var result = await userQueryService.Handle(getUserByEmailQuery, cancellationToken);
        
        return result?.Id ?? 0;
    }

    public async Task<int> FetchUserIdByEmail(string email, CancellationToken cancellationToken)
    {
        var getUserByEmailQuery = new GetUserByEmailQuery(email);
        var result = await userQueryService.Handle(getUserByEmailQuery, cancellationToken);
        
        return result?.Id ?? 0;
    }

    public async Task<string> FetchEmailByUserId(int userId, CancellationToken cancellationToken)
    {
        var getEmailByIdQuery = new GetEmailByIdQuery(userId);
        var result = await userQueryService.Handle(getEmailByIdQuery, cancellationToken);
        
        return result?.Email ?? string.Empty;
    }
}