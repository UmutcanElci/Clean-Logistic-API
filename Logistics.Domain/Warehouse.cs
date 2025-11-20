public class Warehouse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int MaxCapacity { get; set; }
    public StorageType Type { get; set; }

    public required Location Address { get; set; }
}
