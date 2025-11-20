public class Vehicle
{
    public Guid Id { get; set; }
    public required string LicensePlate { get; set; }
    public enum Type { Truck, Van, BoxTruck, TrailerTruck }
    public int MaxWeightInKg { get; set; }
    public int MaxVolumeInCubicMeters { get; set; }
    public Boolean CanGoAbroad { get; set; }

    public required Location CurrentLocation { get; set; }

    public Guid HubId { get; set; }
    public required Hub Hub { get; set; }
}
