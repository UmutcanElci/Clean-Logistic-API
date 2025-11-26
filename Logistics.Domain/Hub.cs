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
    public Route CreateRouteForOrder(Order order)
    {
        return null;
    }
}
