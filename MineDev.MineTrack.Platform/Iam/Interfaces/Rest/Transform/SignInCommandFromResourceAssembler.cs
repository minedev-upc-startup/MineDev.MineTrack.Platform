using MineDev.MineTrack.Platform.Iam.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Iam.Interfaces.Rest.Resources;

namespace MineDev.MineTrack.Platform.Iam.Interfaces.Rest.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        if (resource == null) throw new ArgumentNullException(nameof(resource), "SignInResource cannot be null when converting to command.");
        
        return new SignInCommand(resource.Email, resource.Password);
    }
}