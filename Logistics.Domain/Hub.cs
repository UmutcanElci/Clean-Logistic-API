public class Hub
{
    public Guid Id { get; set; }
    public ICollection<Vehicle>? Vehicles { get; set; }
    public ICollection<Warehouse>? Warehouses { get; set; }
}
