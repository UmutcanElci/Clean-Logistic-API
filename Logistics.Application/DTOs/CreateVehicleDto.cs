namespace Logistics.Application.DTOs;

using Logistics.Domain;
using Logistics.Domain.common;

public class CreateVehicleDto
{
    public required string LicensePlate { get; set; }
    public VehicleType Type { get; set; }
    public int MaxWeightInKg { get; set; }
    public int MaxVolumeInCubicMeters { get; set; }
    public bool CanGoAbroad { get; set; }
    public int MaxSpeedInKph { get; set; }

    public required Guid HubId { get; set; }

}
