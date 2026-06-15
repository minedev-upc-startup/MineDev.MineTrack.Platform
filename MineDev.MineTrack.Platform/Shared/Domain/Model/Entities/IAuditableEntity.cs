namespace MineDev.MineTrack.Platform.Shared.Domain.Model.Entities;

/// <summary>
///     Marks an entity as carrying audit timestamps managed by the persistence layer.
///     Any entity that implements this interface will automatically have
///     CreatedAt set once on first persistence and UpdatedAt refreshed on every save.
/// </summary>
public interface IAuditableEntity
{
    DateTimeOffset? CreatedAt { get; set; }
    DateTimeOffset? UpdatedAt { get; set; }
}