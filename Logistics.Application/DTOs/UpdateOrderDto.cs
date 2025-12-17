namespace Logistics.Application.DTOs;

public class UpdateOrderDto
{
    public required Guid OrderId { get; set; }
    public required string CustomerName { get; set; }
    public required string MailAddress { get; set; }
    public required string PhoneNumber { get; set; }
    public required LocationDto PickUpLocation { get; set; }
    public required LocationDto DestinationLocation { get; set; }
    public required List<UpdateOrderItemDto> Items { get; set; }
}
