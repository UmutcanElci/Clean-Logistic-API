namespace Logistics.Application.DTOs;

using Logistics.Domain.common;

public class OrderDto
{

    public Guid Id { get; set; }
    public required string CustomerName { get; set; }
    public required string MailAddress { get; set; }
    public required string PhoneNumber { get; set; }


    public required LocationDto PickUpLocation { get; set; }
    public required LocationDto DestinationLocation { get; set; }

    public required ICollection<OrderItemDto> OrderItems { get; set; }

    public OrderStatus Status { get; set; }

}
