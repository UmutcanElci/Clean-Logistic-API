namespace Logistics.Domain;

using Logistics.Domain.common;
public class Route
{
    public Guid Id { get; set; }
    public required Vehicle AssignVehicle { get; set; }
    public required List<Location> Stops { get; set; }
    public RouteStatus Status { get; set; }
    public Order? Order { get; set; }
    public required Guid OrderId { get; set; }
}
