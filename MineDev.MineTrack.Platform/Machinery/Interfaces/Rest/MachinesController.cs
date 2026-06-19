using Microsoft.AspNetCore.Mvc;
using MineDev.MineTrack.Platform.Machinery.Application.CommandServices;
using MineDev.MineTrack.Platform.Machinery.Application.QueryServices;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.Queries;
using MineDev.MineTrack.Platform.Machinery.Interfaces.Rest.Resources;
using MineDev.MineTrack.Platform.Machinery.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace MineDev.MineTrack.Platform.Machinery.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag("Machinery Management")]
public class MachinesController(
    IMachineCommandService machineCommandService,
    IMachineQueryService machineQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Machine", "Publishes a new machine to the catalog.")]
    [SwaggerResponse(201, "Machine created successfully.")]
    [SwaggerResponse(400, "Bad request.")]
    public async Task<IActionResult> CreateMachine(
        [FromBody] CreateMachineResource resource,
        CancellationToken cancellationToken = default)
    {
        var command = CreateMachineCommandFromResourceAssembler.ToCommandFrom(resource);
        var result = await machineCommandService.Handle(command, cancellationToken);
        if (result.IsFailure)
            return MachinesActionResultAssembler.ToActionResultFrom(result);
        var machineResource = MachineResourceFromEntityAssembler.ToResourceFrom(result.Value!);
        return CreatedAtAction(nameof(GetMachineById),
            new { id = result.Value!.Id }, machineResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Machines", "Returns all machines in the catalog.")]
    [SwaggerResponse(200, "Machines retrieved successfully.")]
    public async Task<IActionResult> GetAllMachines(
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllMachinesQuery();
        var machines = await machineQueryService.Handle(query, cancellationToken);
        var resources = machines.Select(MachineResourceFromEntityAssembler.ToResourceFrom);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Machine By Id", "Returns a machine by id.")]
    [SwaggerResponse(200, "Machine retrieved successfully.")]
    [SwaggerResponse(404, "Machine not found.")]
    public async Task<IActionResult> GetMachineById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetMachineByIdQuery(id);
        var machine = await machineQueryService.Handle(query, cancellationToken);
        if (machine is null) return NotFound();
        return Ok(MachineResourceFromEntityAssembler.ToResourceFrom(machine));
    }

    [HttpGet("~/api/v1/owners/{ownerId:int}/machines")]
    [SwaggerOperation("Get Machines By Owner", "Returns the fleet registered by an owner.")]
    [SwaggerResponse(200, "Machines retrieved successfully.")]
    public async Task<IActionResult> GetMachinesByOwnerId(
        int ownerId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetMachinesByOwnerIdQuery(ownerId);
        var machines = await machineQueryService.Handle(query, cancellationToken);
        var resources = machines.Select(MachineResourceFromEntityAssembler.ToResourceFrom);
        return Ok(resources);
    }

    [HttpPatch("{id:int}")]
    [SwaggerOperation("Update Machine Status", "Updates the status of a machine (Available, Rented, Under Maintenance).")]
    [SwaggerResponse(200, "Machine status updated successfully.")]
    [SwaggerResponse(400, "Invalid status.")]
    [SwaggerResponse(404, "Machine not found.")]
    public async Task<IActionResult> UpdateMachineStatus(
        int id,
        [FromBody] UpdateMachineStatusResource resource,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateMachineStatusCommand(id, resource.Status);
        var result = await machineCommandService.Handle(command, cancellationToken);
        if (result.IsFailure)
            return MachinesActionResultAssembler.ToActionResultFrom(result);
        return Ok(MachineResourceFromEntityAssembler.ToResourceFrom(result.Value!));
    }
}
