namespace MineDev.MineTrack.Platform.Iam.Domain.Model.Commands;

public record SignUpCommand(string Username, string Email, string FullName, string Phone, string Company, string Role, string Password);