using Microsoft.AspNetCore.Mvc;
using MineDev.MineTrack.Platform.Machinery.Application.Errors;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.Aggregates;
using MineDev.MineTrack.Platform.Shared.Application.Model;

namespace MineDev.MineTrack.Platform.Machinery.Interfaces.Rest.Transform;

public static class MachinesActionResultAssembler
{
    public static IActionResult ToActionResultFrom(Result<Machine> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return result.Error switch
        {
            CreateMachineError.InvalidName => new BadRequestObjectResult(result.Message),
            CreateMachineError.InvalidHourlyRate => new BadRequestObjectResult(result.Message),
            UpdateMachineError.MachineNotFound => new NotFoundObjectResult(result.Message),
            UpdateMachineError.InvalidStatus => new BadRequestObjectResult(result.Message),
            _ => new StatusCodeResult(500)
        };
    }
}