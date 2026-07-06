using MineDev.MineTrack.Platform.Rental.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Rental.Domain.Model.ValueObjects;
using MineDev.MineTrack.Platform.Shared.Domain.Model.Entities;

namespace MineDev.MineTrack.Platform.Rental.Domain.Model.Aggregates;

public partial class RentalRequest : IAuditableEntity
{
    protected RentalRequest()
    {
        RejectionReason = string.Empty;
    }

    public RentalRequest(CreateRentalRequestCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        MachineId = command.MachineId;
        ClientId = command.ClientId;
        OwnerId = command.OwnerId;
        StartDate = command.StartDate;
        EndDate = command.EndDate;
        Status = ERentalRequestStatus.Pending;
        SubmittedAt = DateTime.UtcNow;
        RejectionReason = string.Empty;
    }

    public int Id { get; private set; }
    public int MachineId { get; private set; }
    public int ClientId { get; private set; }
    public int OwnerId { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public ERentalRequestStatus Status { get; private set; }
    public DateTime SubmittedAt { get; private set; }
    public decimal EstimatedTotalCost { get; private set; }
    public string? RejectionReason { get; private set; }
    public DateTime? ResolvedAt { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void Approve()
    {
        Status = ERentalRequestStatus.Approved;
        ResolvedAt = DateTime.UtcNow;
    }

    public void Reject(string rejectionReason)
    {
        Status = ERentalRequestStatus.Rejected;
        RejectionReason = rejectionReason;
        ResolvedAt = DateTime.UtcNow;
    }
    
    public void Complete()
    {
        Status = ERentalRequestStatus.Completed;
        ResolvedAt = DateTime.UtcNow;
    }
}