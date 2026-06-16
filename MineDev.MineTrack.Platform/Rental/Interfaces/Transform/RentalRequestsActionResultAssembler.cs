using Microsoft.AspNetCore.Mvc;
using MineDev.MineTrack.Platform.Rental.Application.Errors;
using MineDev.MineTrack.Platform.Rental.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Shared.Application.Model;

namespace MineDev.MineTrack.Platform.Rental.Interfaces.Rest.Transform;

public static class RentalRequestsActionResultAssembler
{
    public static IActionResult ToActionResultFrom(Result<RentalRequest> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return result.Error switch
        {
            CreateRentalRequestError.MachineNotAvailable =>
                new BadRequestObjectResult(result.Message),
            CreateRentalRequestError.InvalidDateRange =>
                new BadRequestObjectResult(result.Message),
            UpdateRentalRequestError.RentalRequestNotFound =>
                new NotFoundObjectResult(result.Message),
            UpdateRentalRequestError.InvalidStateTransition =>
                new BadRequestObjectResult(result.Message),
            UpdateRentalRequestError.Unauthorized =>
                new UnauthorizedObjectResult(result.Message),
            _ => new StatusCodeResult(500)
        };
    }
}