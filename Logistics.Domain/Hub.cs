namespace Logistics.Domain;

using Logistics.Domain.common;

public class Hub
{
    public Guid Id { get; set; }
    public ICollection<Vehicle>? Vehicles { get; set; }
    public ICollection<Warehouse>? Warehouses { get; set; }

    public void RegisterNewVehicle(Vehicle vehicle)
    {

    }
    public Route CreateRouteForOrder(Order order, Vehicle assignedvehicle)
    {
        return new Route
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            Order = order,
            AssignVehicle = assignedvehicle,
            Status = RouteStatus.Planned,
            Stops = new List<Location>()
        };
    }


}
