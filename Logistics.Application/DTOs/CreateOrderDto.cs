namespace Logistics.Application.DTOs;

using System.ComponentModel.DataAnnotations;

public class CreateOrderDto
{
    [Required] public required string CustomerName { get; set; }
    [Required] public required string MailAddress { get; set; }
    [Required] public required string PhoneNumber { get; set; }
    [Required] public required LocationDto PickUpLocation { get; set; }
    [Required] public required LocationDto DestinationLocation { get; set; }
    [Required] public required List<CreateOrderItemDto> Items { get; set; }
}
