using MineDev.MineTrack.Platform.Iam.Application.Internal.OutboundServices;
using MineDev.MineTrack.Platform.Iam.Application.QueryServices;
using MineDev.MineTrack.Platform.Iam.Domain.Model.Queries;
using MineDev.MineTrack.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace MineDev.MineTrack.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    
    public async Task InvokeAsync(HttpContext context, IUserQueryService userQueryService, ITokenService tokenService)
    {
        Console.WriteLine("Entering InvokeAsync");
        var cancellationToken = context.RequestAborted;

        var allowAnonymous = context.Request.HttpContext.GetEndpoint()!.Metadata.Any(m => m.GetType() == typeof(AllowAnonymousAttribute));
        
        Console.WriteLine($"Allow Anonymous is {allowAnonymous}");
        
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");

            await next(context);
            return;
        }

        Console.WriteLine("Entering authorization");

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null) throw new Exception("Null or invalid token");

        var userId = await tokenService.ValidateToken(token);

        if (userId == null) throw new Exception("Invalid token");

        var getUserByIdQuery = new GetEmailByIdQuery(userId.Value);

        var user = await userQueryService.Handle(getUserByIdQuery, cancellationToken);
        
        Console.WriteLine("Successful authorization. Updating Context...");
        
        context.Items["User"] = user;
        
        Console.WriteLine("Continuing with Middleware Pipeline");

        await next(context);
    }
}