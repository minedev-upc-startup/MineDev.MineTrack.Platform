using MineDev.MineTrack.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using MineDev.MineTrack.Platform.Rental.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MineDev.MineTrack.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     Application database context for the Learning Center Platform
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Apply audit timestamp interceptor for all IAuditableEntity implementations
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <summary>
    ///     On creating the database model
    /// </summary>
    /// <remarks>
    ///     This method is used to create the database model for the application.
    /// </remarks>
    /// <param name="builder">
    ///     The model builder for the database context
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        base.OnModelCreating(builder);

        // Add builder.ApplyPublishingConfiguration for our bounded contexts
        
        // Rental Bounded Context
        builder.ApplyRentalRequestConfiguration();

        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
        
    }
}