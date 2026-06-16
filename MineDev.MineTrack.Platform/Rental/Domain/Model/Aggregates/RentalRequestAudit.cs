using MineDev.MineTrack.Platform.Rental.Domain.Model.Commands;

namespace MineDev.MineTrack.Platform.Rental.Domain.Model.Aggregates;

public partial class RentalRequest
{
    public static RentalRequest Create(CreateRentalRequestCommand command) => new(command);

    public RentalRequest Approve(ApproveRentalRequestCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        Approve();
        return this;
    }

    public RentalRequest Reject(RejectRentalRequestCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        Reject(command.RejectionReason);
        return this;
    }
}