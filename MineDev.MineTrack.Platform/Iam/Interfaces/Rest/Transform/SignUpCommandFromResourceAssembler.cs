using MineDev.MineTrack.Platform.Iam.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Iam.Interfaces.Rest.Resources;

namespace MineDev.MineTrack.Platform.Iam.Interfaces.Rest.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        if (resource == null) throw new ArgumentNullException(nameof(resource), "SignUpResource cannot be null when converting to command.");
        
        return new SignUpCommand(resource.Username, resource.Password);
    }
}