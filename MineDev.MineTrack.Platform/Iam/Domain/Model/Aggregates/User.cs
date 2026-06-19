using System.Text.Json.Serialization;

namespace MineDev.MineTrack.Platform.Iam.Domain.Model.Aggregates;

public partial class User(string username, string email, string fullName, string phone, string company, string role, string passwordHash)
{
    public User() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) { }

    public int Id { get; }
    public string Username { get; private set; } = username;
    public string Email { get; private set; } = email;
    public string FullName { get; private set; } = fullName;
    public string Phone { get; private set; } = phone;
    public string Company { get; private set; } = company;
    public string Role { get; private set; } = role;
    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;
    
    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }

    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
}