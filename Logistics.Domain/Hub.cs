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

        var bestVehicle = availableVehicles.FirstOrDefault(v =>
            v.Status == VehicleStatus.Idle &&
            v.MaxWeightInKg >= totalWeight);

        if (bestVehicle == null)
        {
            throw new InvalidOperationException("No suitable vehicle available for this order.");
        }


        var stopsList = new List<Location>
    {
        new Location
        {
            StreetAddress = order.PickUpLocation.StreetAddress,
            City = order.PickUpLocation.City,
            PostalCode = order.PickUpLocation.PostalCode,
            Country = order.PickUpLocation.Country,
            GpsCoordinates = new Coordinates(order.PickUpLocation.GpsCoordinates.Latitude, order.PickUpLocation.GpsCoordinates.Longitude)
        },
        new Location
        {
            StreetAddress = order.DestinationLocation.StreetAddress,
            City = order.DestinationLocation.City,
            PostalCode = order.DestinationLocation.PostalCode,
            Country = order.DestinationLocation.Country,
            GpsCoordinates = new Coordinates(order.DestinationLocation.GpsCoordinates.Latitude, order.DestinationLocation.GpsCoordinates.Longitude)
        }
    };

        var newRoute = new Route
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            Order = order,
            AssignVehicle = bestVehicle,
            Status = RouteStatus.Planned,
            Stops = stopsList
        };

        return newRoute;
    }
}
