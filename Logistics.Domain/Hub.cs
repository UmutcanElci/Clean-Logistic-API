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
    public Route CreateRouteForOrder(Order order, IReadOnlyList<Vehicle> availableVehicles)
    {
        var totalWeight = order.OrderItems.Sum(item => item.WeightInKg * item.Quantity);

        var suitableVehicles = availableVehicles.Where(v =>
            v.Status == VehicleStatus.Idle &&
            v.MaxWeightInKg >= totalWeight
        ).ToList();

        var bestVehicle = suitableVehicles.FirstOrDefault();

        if (bestVehicle == null)
        {
            throw new InvalidOperationException("No suitable vehicle available for this order.");
        }

        var stops = new List<Location>
            {
                order.PickUpLocation,
                order.DestinationLocation
            };

        var newRoute = new Route
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            Order = order,
            AssignVehicle = bestVehicle,
            Status = RouteStatus.Planned,
            Stops = stops
        };

        return newRoute;
    }

}
