namespace Logistics.Application.DTOs;

using System.ComponentModel.DataAnnotations;
public class CreateOrderItemDto
{
    [Required] public required string Description { get; set; }
    [Required] public required double WeightInKg { get; set; }
    [Required] public required int Quantity { get; set; }
}
