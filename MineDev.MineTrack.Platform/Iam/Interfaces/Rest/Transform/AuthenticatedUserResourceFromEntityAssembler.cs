using MineDev.MineTrack.Platform.Iam.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Iam.Interfaces.Rest.Resources;

namespace MineDev.MineTrack.Platform.Iam.Interfaces.Rest.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "User aggregate cannot be null when creating authenticated user resource.");
        if (string.IsNullOrEmpty(token)) throw new ArgumentException("Token cannot be null or empty when creating authenticated user resource.", nameof(token));
        
        return new AuthenticatedUserResource(user.Id, user.Username, token);
    }
}