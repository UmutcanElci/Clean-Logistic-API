namespace Logistics.Application.DTOs;

using Logistics.Domain.common;

public class UpdateVehicleDto
{
    public Guid Id { get; set; }
    public required string LicensePlate { get; set; }
    public VehicleType Type { get; set; }
    public int MaxWeightInKg { get; set; }
    public int MaxVolumeInCubicMeters { get; set; }
    public bool CanGoAbroad { get; set; }
    public int MaxSpeedInKph { get; set; }
    public required LocationDto CurrentLocation { get; set; }

    public VehicleStatus Status { get; set; }
}
