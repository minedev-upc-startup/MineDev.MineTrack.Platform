using MineDev.MineTrack.Platform.Shared.Domain.Model.Entities;

namespace MineDev.MineTrack.Platform.Iam.Domain.Model.Aggregates;

public partial class User : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}