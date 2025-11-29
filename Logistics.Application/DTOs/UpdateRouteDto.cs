namespace Logistics.Application.DTOs;

using Logistics.Domain.common;

public class UpdateRouteDto
{
    public Guid Id { get; set; }
    public Guid AssignedVehicleId { get; set; }
    public required List<LocationDto> Stops { get; set; }
    public RouteStatus Status { get; set; }
}
