using Microsoft.EntityFrameworkCore;
using MineDev.MineTrack.Platform.Rental.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Rental.Domain.Model.ValueObjects;

namespace MineDev.MineTrack.Platform.Rental.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyRentalRequestConfiguration(this ModelBuilder builder)
    {
        builder.Entity<RentalRequest>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.MachineId).IsRequired();
            entity.Property(r => r.ClientId).IsRequired();
            entity.Property(r => r.OwnerId).IsRequired();
            entity.Property(r => r.StartDate)
                .IsRequired()
                .HasConversion(
                    v => v.ToDateTime(TimeOnly.MinValue),
                    v => DateOnly.FromDateTime(v));
            entity.Property(r => r.EndDate)
                .IsRequired()
                .HasConversion(
                    v => v.ToDateTime(TimeOnly.MinValue),
                    v => DateOnly.FromDateTime(v));
            entity.Property(r => r.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (ERentalRequestStatus)Enum.Parse(typeof(ERentalRequestStatus), v))
                .IsRequired();
            entity.Property(r => r.SubmittedAt).IsRequired();
            entity.Property(r => r.EstimatedTotalCost).HasPrecision(18, 2);
            entity.Property(r => r.RejectionReason);
            entity.Property(r => r.ResolvedAt);
        });
    }
}