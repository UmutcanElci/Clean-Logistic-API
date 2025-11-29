namespace Logistics.Application.DTOs;

using Logistics.Domain.common;

public class RouteDto
{
    public Guid Id { get; set; }
    public required VehicleDto AssignVehicle { get; set; }
    public required List<LocationDto> Stops { get; set; }
    public RouteStatus Status { get; set; }
    public Guid OrderId { get; set; }
}
