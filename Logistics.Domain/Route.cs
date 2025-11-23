namespace Logistics.Domain;

using Logistics.Domain.common;
public class Route
{
    public Guid Id { get; set; }
    public required Vehicle AssignVehicle { get; set; }
    public required List<Location> Stops { get; set; }
    public required RouteStatus Status { get; set; }
}
