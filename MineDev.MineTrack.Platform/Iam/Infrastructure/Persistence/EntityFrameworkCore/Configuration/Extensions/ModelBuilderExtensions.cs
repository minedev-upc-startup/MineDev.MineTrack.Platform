using Microsoft.EntityFrameworkCore;
using MineDev.MineTrack.Platform.Iam.Domain.Model.Aggregates;

namespace MineDev.MineTrack.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.Email).IsRequired();
        builder.Entity<User>().Property(u => u.FullName).IsRequired();
        builder.Entity<User>().Property(u => u.Phone).IsRequired();
        builder.Entity<User>().Property(u => u.Company).IsRequired();
        builder.Entity<User>().Property(u => u.Role).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
    }
}