namespace Logistics.Application.DTOs;

using Logistics.Domain.common;

public class WarehouseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int MaxCapacity { get; set; }
    public StorageType Type { get; set; }
    public required LocationDto Address { get; set; }
}
