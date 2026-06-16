namespace MineDev.MineTrack.Platform.Rental.Domain.Model.Commands;

public record RejectRentalRequestCommand(int RentalRequestId, string RejectionReason);