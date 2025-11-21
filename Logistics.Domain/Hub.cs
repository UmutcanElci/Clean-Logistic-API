public class Hub
{
    public Guid Id { get; set; }

    // fields just to have here for no reason
    public int TotalOrders { get; set; }


    // Not sure will ask
    public Order? Orders { get; set; }

    public ICollection<Vehicle>? Vehicles { get; set; }
    public ICollection<Warehouse>? Warehouses { get; set; }
}
