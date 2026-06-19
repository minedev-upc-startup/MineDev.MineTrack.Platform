namespace MineDev.MineTrack.Platform.Machinery.Domain.Model.Commands;

public record UpdateMachineStatusCommand(int MachineId, string Status);