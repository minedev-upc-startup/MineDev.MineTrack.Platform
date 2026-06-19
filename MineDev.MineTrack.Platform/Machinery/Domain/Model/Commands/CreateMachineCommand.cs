namespace MineDev.MineTrack.Platform.Machinery.Domain.Model.Commands;

public record CreateMachineCommand(
    int OwnerId,
    string Name,
    string Type,
    string Brand,
    string Model,
    int? Year,
    decimal HourlyRate,
    int? DailyMinimumHours,
    List<string> Photos,
    Dictionary<string, object> Specs
);