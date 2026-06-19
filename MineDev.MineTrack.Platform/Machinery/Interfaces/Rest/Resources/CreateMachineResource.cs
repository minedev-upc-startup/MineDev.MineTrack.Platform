namespace MineDev.MineTrack.Platform.Machinery.Interfaces.Rest.Resources;

public record CreateMachineResource(
    int OwnerId,
    string Name,
    string Type,
    string Brand,
    string Model,
    int? Year,
    decimal HourlyRate,
    int? DailyMinimumHours,
    List<string>? Photos,
    Dictionary<string, object>? Specs
);