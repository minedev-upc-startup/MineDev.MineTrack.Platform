using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MineDev.MineTrack.Platform.Shared.Domain.Repositories;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using MineDev.MineTrack.Platform.Shared.Resources;

var builder = WebApplication.CreateBuilder(args);

// Configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Localization Configuration
builder.Services.AddLocalization();

// Configure Kebab Case Route Naming Convention
builder.Services.AddControllers(options => 
    options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

// Register RFC 7807 ProblemDetails
builder.Services.AddProblemDetails();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

// Database Connection
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration
        .GetConnectionString("DefaultConnection");
    
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException(
            "Database connection string is not set in the configuration.");

    var connectionString = Environment
        .ExpandEnvironmentVariables(connectionStringTemplate);
    
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException(
            "Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Shared Bounded Context Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM Bounded Context Injection (uncomment when ready)
// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<IUserCommandService, UserCommandService>();
// builder.Services.AddScoped<IUserQueryService, UserQueryService>();

// Rentals Bounded Context Injection (uncomment when ready)
// builder.Services.AddScoped<IRentalRequestRepository, RentalRequestRepository>();
// builder.Services.AddScoped<IRentalRequestCommandService, RentalRequestCommandService>();

var app = builder.Build();

// Apply pending EF Core migrations on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Exception Handler
app.UseExceptionHandler();

// Swagger (enabled in all environments for academic purposes)
app.UseSwagger();
app.UseSwaggerUI();

// Localization
string[] supportedCultures = ["en", "es"];
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();