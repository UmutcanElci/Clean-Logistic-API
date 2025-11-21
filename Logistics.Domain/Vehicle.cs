public class Vehicle
{
    public Guid Id { get; set; }
    public required string LicensePlate { get; set; }
    public VehicleType Type { get; set; }
    public int MaxWeightInKg { get; set; }
    public int MaxVolumeInCubicMeters { get; set; }
    public bool CanGoAbroad { get; set; }
    public int MaxSpeedInKph { get; set; }
    public required Location CurrentLocation { get; set; }

    public VehicleStatus Status { get; private set; }

    public Guid HubId { get; set; }
    public required Hub Hub { get; set; }

    public void SendForMaintenance()
    {
        if (Status == VehicleStatus.Idle)
        {
            Status = VehicleStatus.InMaintenance;
        }
    }
}
