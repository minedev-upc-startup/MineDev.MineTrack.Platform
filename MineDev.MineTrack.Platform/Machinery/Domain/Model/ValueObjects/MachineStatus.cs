namespace MineDev.MineTrack.Platform.Machinery.Domain.Model.ValueObjects;

/// <summary>
///     Allowed values for Machine.Status. Modeled as string constants (not an enum)
///     because "Under Maintenance" contains a space and must round-trip exactly
///     with the frontend, which already uses these literal values.
/// </summary>
public static class MachineStatus
{
    public const string Available = "Available";
    public const string Rented = "Rented";
    public const string UnderMaintenance = "Under Maintenance";

    private static readonly HashSet<string> AllValues = [Available, Rented, UnderMaintenance];

    public static bool IsValid(string? status) => status is not null && AllValues.Contains(status);
}