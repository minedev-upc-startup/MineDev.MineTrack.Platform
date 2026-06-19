using MineDev.MineTrack.Platform.Machinery.Domain.Model.Commands;
using MineDev.MineTrack.Platform.Machinery.Domain.Model.ValueObjects;
using MineDev.MineTrack.Platform.Shared.Domain.Model.Entities;

namespace MineDev.MineTrack.Platform.Machinery.Domain.Model.Aggregates;

public partial class Machine : IAuditableEntity
{
    protected Machine()
    {
        Name = string.Empty;
        Type = string.Empty;
        Brand = string.Empty;
        Model = string.Empty;
        Status = MachineStatus.Available;
        Photos = [];
        Specs = new Dictionary<string, object>();
    }

    public Machine(CreateMachineCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        OwnerId = command.OwnerId;
        Name = command.Name;
        Type = command.Type;
        Brand = command.Brand;
        Model = command.Model;
        Year = command.Year;
        HourlyRate = command.HourlyRate;
        DailyMinimumHours = command.DailyMinimumHours;
        Status = MachineStatus.Available;
        Photos = command.Photos;
        Specs = command.Specs;
    }

    public int Id { get; private set; }
    public int OwnerId { get; private set; }
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public int? Year { get; private set; }
    public decimal HourlyRate { get; private set; }
    public int? DailyMinimumHours { get; private set; }
    public string Status { get; private set; }
    public List<string> Photos { get; private set; }
    public Dictionary<string, object> Specs { get; private set; }
    public double? CurrentLat { get; private set; }
    public double? CurrentLng { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void UpdateStatus(string status)
    {
        Status = status;
    }
}