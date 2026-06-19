namespace MineDev.MineTrack.Platform.Iam.Interfaces.Rest.Resources;

public record SignUpResource(string Username, string Email, string FullName, string Phone, string Company, string Role, string Password);