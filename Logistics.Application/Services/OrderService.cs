using Logistics.Domain;
using Logistics.Domain.common;
using Logistics.Application.DTOs;
using Logistics.Application.Interfaces;


namespace Logistics.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerName = createOrderDto.CustomerName,
            MailAddress = createOrderDto.MailAddress,
            PhoneNumber = createOrderDto.PhoneNumber,
            OrderItems = new List<OrderItem>(),

            PickUpLocation = new Location
            {
                StreetAddress = createOrderDto.PickUpLocation.StreetAddress,
                City = createOrderDto.PickUpLocation.City,
                PostalCode = createOrderDto.PickUpLocation.PostalCode,
                Country = createOrderDto.PickUpLocation.Country,
                GpsCoordinates = new Coordinates(0.0, 0.0)
            },

            DestinationLocation = new Location
            {
                StreetAddress = createOrderDto.DestinationLocation.StreetAddress,
                City = createOrderDto.DestinationLocation.City,
                PostalCode = createOrderDto.DestinationLocation.PostalCode,
                Country = createOrderDto.DestinationLocation.Country,
                GpsCoordinates = new Coordinates(0.0, 0.0)
            }
        };



        order.OrderItems = new List<OrderItem>();
        foreach (var itemDto in createOrderDto.Items)
        {
            order.OrderItems.Add(new OrderItem
            {
                Description = itemDto.Description,
                WeightInKg = itemDto.WeightInKg,
                Quantity = itemDto.Quantity
            });
        }

        var createdOrder = await _repository.CreateAsync(order);
        return createdOrder;
    }
}
