public class Order
{
    public Guid Id { get; set; }
    public required string CustomerName { get; set; }
    public required string MailAddress { get; set; }
    public required string PhoneNumber { get; set; }


    public required Location PickUpLocation { get; set; }
    public required Location DestinationLocation { get; set; }

    public required ICollection<OrderItem> OrderItems { get; set; }

    public OrderStatus Status { get; private set; }

    public void Confirm()
    {
        if (Status == OrderStatus.Pending)
        {
            Status = OrderStatus.Confirmed; // Confirmed ? 
        }
    }
}
