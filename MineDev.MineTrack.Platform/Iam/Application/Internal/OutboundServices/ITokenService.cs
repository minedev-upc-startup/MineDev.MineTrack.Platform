using MineDev.MineTrack.Platform.Iam.Domain.Model.Aggregates;

namespace MineDev.MineTrack.Platform.Iam.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}