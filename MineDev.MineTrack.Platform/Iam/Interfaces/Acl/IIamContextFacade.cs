namespace MineDev.MineTrack.Platform.Iam.Interfaces.Acl;

public interface IIamContextFacade
{
    Task<int> CreateUser(string username, string email, string fullName, string phone, string company, string role, string password, CancellationToken cancellationToken);
    Task<int> FetchUserIdByEmail(string email, CancellationToken cancellationToken);
    Task<string> FetchEmailByUserId(int userId, CancellationToken cancellationToken);
}