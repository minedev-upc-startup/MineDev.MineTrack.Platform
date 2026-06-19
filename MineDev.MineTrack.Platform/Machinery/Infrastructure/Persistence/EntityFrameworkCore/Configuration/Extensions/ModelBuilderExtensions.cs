using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.ValueObjects;

namespace MineDev.MineTrack.Platform.Machinery.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyMachineConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Machine>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.OwnerId).IsRequired();
            entity.Property(m => m.Name).IsRequired().HasMaxLength(150);
            entity.Property(m => m.Type).IsRequired().HasMaxLength(80);
            entity.Property(m => m.Brand).IsRequired().HasMaxLength(80);
            entity.Property(m => m.Model).IsRequired().HasMaxLength(80);
            entity.Property(m => m.Year);
            entity.Property(m => m.HourlyRate).HasPrecision(18, 2);
            entity.Property(m => m.DailyMinimumHours);
            entity.Property(m => m.Status)
                .IsRequired()
                .HasMaxLength(40)
                .HasDefaultValue(MachineStatus.Available);

            // Photos: stored as a JSON-encoded text column (List<string> <-> string).
            entity.Property(m => m.Photos)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>())
                .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                    (a, b) => (a ?? new List<string>()).SequenceEqual(b ?? new List<string>()),
                    v => v.Aggregate(0, (hash, s) => HashCode.Combine(hash, s.GetHashCode())),
                    v => v.ToList()));

            // Specs: free-form key/value technical spec sheet, stored as JSON-encoded text.
            entity.Property(m => m.Specs)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, object>>(v, (JsonSerializerOptions?)null) ?? new Dictionary<string, object>())
                .Metadata.SetValueComparer(new ValueComparer<Dictionary<string, object>>(
                    (a, b) => JsonSerializer.Serialize(a, (JsonSerializerOptions?)null) == JsonSerializer.Serialize(b, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null).GetHashCode(),
                    v => new Dictionary<string, object>(v)));

            entity.Property(m => m.CurrentLat);
            entity.Property(m => m.CurrentLng);
        });
    }
}
