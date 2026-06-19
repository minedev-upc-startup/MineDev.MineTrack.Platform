using MineDev.MineTrack.Platform.Shared.Domain.Model;

namespace MineDev.MineTrack.Platform.Iam.Domain.Model.Errors;

public static class IamErrors
{
    public static readonly Error InvalidCredentials = new("Iam.InvalidCredentials", "Invalid email, username or password.");

    public static readonly Error UsernameAlreadyTaken =
        new("Iam.UsernameAlreadyTaken", "The specified username is already taken.");
    
    public static readonly Error EmailAlreadyTaken =
        new("Iam.EmailAlreadyTaken", "The specified email is already taken.");

    public static readonly Error UserCreationFailed =
        new("Iam.UserCreationFailed", "An error occurred while creating the user.");
}