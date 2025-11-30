namespace Logistics.Application.DTOs;

public class OrderItemDto
{
    public required string Description { get; set; }
    public required double WeightInKg { get; set; }
    public required int Quantity { get; set; }
}
