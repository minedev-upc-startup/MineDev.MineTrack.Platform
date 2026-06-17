using MineDev.MineTrack.Platform.Iam.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Iam.Interfaces.Rest.Resources;

namespace MineDev.MineTrack.Platform.Iam.Interfaces.Rest.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "User aggregate cannot be null when converting to resource.");
        
        return new UserResource(user.Id, user.Username);
    }
}