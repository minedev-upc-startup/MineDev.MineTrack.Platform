using Microsoft.AspNetCore.Mvc;
using MineDev.MineTrack.Platform.Rental.Application.CommandServices;
using MineDev.MineTrack.Platform.Rental.Application.QueryServices;
using MineDev.MineTrack.Platform.Rental.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Rental.Domain.Model.Queries;
using MineDev.MineTrack.Platform.Rental.Interfaces.Rest.Resources;
using MineDev.MineTrack.Platform.Rental.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace MineDev.MineTrack.Platform.Rental.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag("Rentals Management")]
public class RentalsController(
    IRentalRequestCommandService rentalRequestCommandService,
    IRentalRequestQueryService rentalRequestQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Rental", "Creates a new rental request.")]
    [SwaggerResponse(201, "Rental request created successfully.")]
    [SwaggerResponse(400, "Bad request.")]
    public async Task<IActionResult> CreateRentalRequest(
        [FromBody] CreateRentalRequestResource resource,
        CancellationToken cancellationToken = default)
    {
        var command = CreateRentalRequestCommandFromResourceAssembler.ToCommandFrom(resource);
        var result = await rentalRequestCommandService.Handle(command, cancellationToken);
        if (result.IsFailure)
            return RentalRequestsActionResultAssembler.ToActionResultFrom(result);
        var rentalRequestResource = RentalRequestResourceFromEntityAssembler.ToResourceFrom(result.Value!);
        return CreatedAtAction(nameof(GetRentalById),
            new { id = result.Value!.Id }, rentalRequestResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Rentals", "Returns all rental requests.")]
    [SwaggerResponse(200, "Rental requests retrieved successfully.")]
    public async Task<IActionResult> GetAllRentals(
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllRentalRequestsQuery();
        var rentalRequests = await rentalRequestQueryService.Handle(query, cancellationToken);
        var resources = rentalRequests.Select(RentalRequestResourceFromEntityAssembler.ToResourceFrom);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Rental By Id", "Returns a rental request by id.")]
    [SwaggerResponse(200, "Rental request retrieved successfully.")]
    [SwaggerResponse(404, "Rental request not found.")]
    public async Task<IActionResult> GetRentalById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetRentalRequestByIdQuery(id);
        var rentalRequest = await rentalRequestQueryService.Handle(query, cancellationToken);
        if (rentalRequest is null) return NotFound();
        return Ok(RentalRequestResourceFromEntityAssembler.ToResourceFrom(rentalRequest));
    }

    [HttpGet("~/api/v1/clients/{clientId:int}/rentals")]
    [SwaggerOperation("Get Rentals By Client", "Returns rental requests by client id.")]
    [SwaggerResponse(200, "Rental requests retrieved successfully.")]
    public async Task<IActionResult> GetRentalsByClientId(
        int clientId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetRentalRequestsByClientIdQuery(clientId);
        var rentalRequests = await rentalRequestQueryService.Handle(query, cancellationToken);
        var resources = rentalRequests.Select(RentalRequestResourceFromEntityAssembler.ToResourceFrom);
        return Ok(resources);
    }

    [HttpGet("~/api/v1/owners/{ownerId:int}/rentals")]
    [SwaggerOperation("Get Rentals By Owner", "Returns rental requests by owner id.")]
    [SwaggerResponse(200, "Rental requests retrieved successfully.")]
    public async Task<IActionResult> GetRentalsByOwnerId(
        int ownerId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetRentalRequestsByOwnerIdQuery(ownerId);
        var rentalRequests = await rentalRequestQueryService.Handle(query, cancellationToken);
        var resources = rentalRequests.Select(RentalRequestResourceFromEntityAssembler.ToResourceFrom);
        return Ok(resources);
    }

    [HttpPatch("{id:int}/approve")]
    [SwaggerOperation("Approve Rental", "Approves a rental request.")]
    [SwaggerResponse(200, "Rental request approved successfully.")]
    [SwaggerResponse(404, "Rental request not found.")]
    public async Task<IActionResult> ApproveRental(
        int id,
        CancellationToken cancellationToken = default)
    {
        var command = new ApproveRentalRequestCommand(id);
        var result = await rentalRequestCommandService.Handle(command, cancellationToken);
        if (result.IsFailure)
            return RentalRequestsActionResultAssembler.ToActionResultFrom(result);
        return Ok(RentalRequestResourceFromEntityAssembler.ToResourceFrom(result.Value!));
    }

    [HttpPatch("{id:int}/reject")]
    [SwaggerOperation("Reject Rental", "Rejects a rental request.")]
    [SwaggerResponse(200, "Rental request rejected successfully.")]
    [SwaggerResponse(404, "Rental request not found.")]
    public async Task<IActionResult> RejectRental(
        int id,
        [FromBody] RejectRentalRequestCommand command,
        CancellationToken cancellationToken = default)
    {
        var rejectCommand = new RejectRentalRequestCommand(id, command.RejectionReason);
        var result = await rentalRequestCommandService.Handle(rejectCommand, cancellationToken);
        if (result.IsFailure)
            return RentalRequestsActionResultAssembler.ToActionResultFrom(result);
        return Ok(RentalRequestResourceFromEntityAssembler.ToResourceFrom(result.Value!));
    }
}