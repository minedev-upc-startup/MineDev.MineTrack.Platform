using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MineDev.MineTrack.Platform.Iam.Application.QueryServices;
using MineDev.MineTrack.Platform.Iam.Domain.Model.Queries;
using MineDev.MineTrack.Platform.Iam.Interfaces.Rest.Resources;
using MineDev.MineTrack.Platform.Iam.Interfaces.Rest.Transform;
using MineDev.MineTrack.Platform.Shared.Interfaces.Rest.ProblemDetails;
using MineDev.MineTrack.Platform.Shared.Resources.Errors;
using Swashbuckle.AspNetCore.Annotations;
using MineDev.MineTrack.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace MineDev.MineTrack.Platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User endpoints")]
public class UsersController(
    IUserQueryService userQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory
) : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a user by its id", Description = "Get a user by its id", OperationId = "GetUserById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was found", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The user was not found")]
    public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
    {
        var getUserByIdQuery = new GetEmailByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromGetUserByIdResult(
            this,
            user,
            _errorLocalizer,
            _problemDetailsFactory,
            foundUser => Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(foundUser))
        );
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all users", Description = "Get all users", OperationId = "GetAllUsers")]
    [SwaggerResponse(StatusCodes.Status200OK, "The users were found", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery, cancellationToken);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        
        return Ok(userResources);
    }
}