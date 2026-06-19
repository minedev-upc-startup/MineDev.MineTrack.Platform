using Microsoft.EntityFrameworkCore;
using MineDev.MineTrack.Platform.Iam.Application.CommandServices;
using MineDev.MineTrack.Platform.Iam.Application.Internal.CommandServices;
using MineDev.MineTrack.Platform.Iam.Application.Internal.OutboundServices;
using MineDev.MineTrack.Platform.Iam.Domain.Repositories;
using MineDev.MineTrack.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using MineDev.MineTrack.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using MineDev.MineTrack.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using MineDev.MineTrack.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using MineDev.MineTrack.Platform.Shared.Domain.Repositories;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using MineDev.MineTrack.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using MineDev.MineTrack.Platform.Rental.Application.CommandServices;
using MineDev.MineTrack.Platform.Rental.Application.Internal.CommandServices;
using MineDev.MineTrack.Platform.Rental.Application.Internal.QueryServices;
using MineDev.MineTrack.Platform.Rental.Application.QueryServices;
using MineDev.MineTrack.Platform.Rental.Domain.Repositories;
using MineDev.MineTrack.Platform.Rental.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using MineDev.MineTrack.Platform.Machinery.Application.CommandServices;
using MineDev.MineTrack.Platform.Machinery.Application.Internal.CommandServices;
using MineDev.MineTrack.Platform.Machinery.Application.Internal.QueryServices;
using MineDev.MineTrack.Platform.Machinery.Application.QueryServices;
using MineDev.MineTrack.Platform.Machinery.Domain.Repositories;
using MineDev.MineTrack.Platform.Machinery.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using MineDev.MineTrack.Platform.Shared.Interfaces.Rest.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

//  Configure JWT
builder.Services.Configure<TokenSettings>(
    builder.Configuration.GetSection("TokenSettings")
);

// Configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Localization Configuration
builder.Services.AddLocalization();
    
// Configure Kebab Case Route Naming Convention
builder.Services.AddControllers(options =>
    options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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
builder.Services.AddScoped<ProblemDetailsFactory>();

// IAM Bounded Context Injection 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

// Rentals Bounded Context Injection
builder.Services.AddScoped<IRentalRequestRepository, RentalRequestRepository>();
builder.Services.AddScoped<IRentalRequestCommandService, RentalRequestCommandService>();
builder.Services.AddScoped<IRentalRequestQueryService, RentalRequestQueryService>();

// Machinery Bounded Context Injection
builder.Services.AddScoped<IMachineRepository, MachineRepository>();
builder.Services.AddScoped<IMachineCommandService, MachineCommandService>();
builder.Services.AddScoped<IMachineQueryService, MachineQueryService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "https://minetrack-upc-2026.web.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseCors("FrontendPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();