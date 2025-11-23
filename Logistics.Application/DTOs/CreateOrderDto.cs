namespace Logistics.Application.DTOs;

public class CreateOrderDto
{
    public required string CustomerName { get; set; }
    public required string MailAddress { get; set; }
    public required string PhoneNumber { get; set; }
    public required LocationDto PickUpLocation { get; set; }
    public required LocationDto DestinationLocation { get; set; }
    public required List<CreateOrderItemDto> Items { get; set; }
}
