using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MineDev.MineTrack.Platform.Iam.Application.CommandServices;
using MineDev.MineTrack.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using MineDev.MineTrack.Platform.Iam.Interfaces.Rest.Resources;
using MineDev.MineTrack.Platform.Iam.Interfaces.Rest.Transform;
using MineDev.MineTrack.Platform.Iam.Resources;
using MineDev.MineTrack.Platform.Shared.Interfaces.Rest.ProblemDetails;
using MineDev.MineTrack.Platform.Shared.Resources.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace MineDev.MineTrack.Platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(
    IUserCommandService userCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IStringLocalizer<IamMessages> iamLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Sign in", Description = "Sign in a user", OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid username or password")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource, CancellationToken cancellationToken)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var result = await userCommandService.Handle(signInCommand, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromSignInResult(
            this,
            result,
            errorLocalizer,
            problemDetailsFactory,
            userAndToken => Ok(AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(userAndToken.user, userAndToken.token))
        );
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Sign-up", Description = "Sign up a new user", OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was created successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The user was not created")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource, CancellationToken cancellationToken)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
        var result = await userCommandService.Handle(signUpCommand, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromSignUpResult(
            this,
            result,
            errorLocalizer,
            problemDetailsFactory,
            () => Ok(new { message = iamLocalizer["UserCreatedSuccessfully"] })
        );
    }
}