namespace MineDev.MineTrack.Platform.Machinery.Interfaces.Rest.Resources;

public record MachineResource(
    int Id,
    int OwnerId,
    string Name,
    string Type,
    string Brand,
    string Model,
    int? Year,
    decimal HourlyRate,
    int? DailyMinimumHours,
    string Status,
    List<string> Photos,
    Dictionary<string, object> Specs,
    MachineLocationResource? CurrentLocation
);

public record MachineLocationResource(double Lat, double Lng);