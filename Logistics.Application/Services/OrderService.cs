using Logistics.Domain;
using Logistics.Domain.common;
using Logistics.Application.DTOs;
using Logistics.Application.Interfaces;
using Logistics.Application.Mappers;

namespace Logistics.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly IGeocodingService _geocodingService;

    public OrderService(IOrderRepository repository, IGeocodingService geocodingService)
    {
        _repository = repository;
        _geocodingService = geocodingService;
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        var pickupCoords = await _geocodingService.GetCoordinatesFromAddressAsync(
            createOrderDto.PickUpLocation.StreetAddress,
            createOrderDto.PickUpLocation.City,
            createOrderDto.PickUpLocation.Country,
            createOrderDto.PickUpLocation.PostalCode.ToString());

        var destCoords = await _geocodingService.GetCoordinatesFromAddressAsync(
            createOrderDto.DestinationLocation.StreetAddress,
            createOrderDto.DestinationLocation.City,
            createOrderDto.DestinationLocation.Country,
            createOrderDto.DestinationLocation.PostalCode.ToString());

        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerName = createOrderDto.CustomerName,
            MailAddress = createOrderDto.MailAddress,
            PhoneNumber = createOrderDto.PhoneNumber,
            PickUpLocation = new Location
            {
                StreetAddress = createOrderDto.PickUpLocation.StreetAddress,
                City = createOrderDto.PickUpLocation.City,
                PostalCode = createOrderDto.PickUpLocation.PostalCode,
                Country = createOrderDto.PickUpLocation.Country,
                GpsCoordinates = pickupCoords
            },
            DestinationLocation = new Location
            {
                StreetAddress = createOrderDto.DestinationLocation.StreetAddress,
                City = createOrderDto.DestinationLocation.City,
                PostalCode = createOrderDto.DestinationLocation.PostalCode,
                Country = createOrderDto.DestinationLocation.Country,
                GpsCoordinates = destCoords
            },
            OrderItems = createOrderDto.Items.Select(item => new OrderItem
            {
                Description = item.Description,
                WeightInKg = item.WeightInKg,
                Quantity = item.Quantity
            }).ToList()
        };

        var createdOrder = await _repository.CreateAsync(order);
        return createdOrder.ToDto();
    }

    public async Task<OrderDto?> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _repository.GetByIdAsync(orderId);

        if (order == null)
        {
            return null;
        }

        return order.ToDto();
    }

    public async Task<IReadOnlyList<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _repository.GetAllAsync();

        var orderDtos = orders.Select(order => order.ToDto()).ToList();
        return orderDtos;
    }

    public async Task UpdateOrderAsync(UpdateOrderDto updateOrderDto)
    {
        var existingOrder = await _repository.GetByIdAsync(updateOrderDto.OrderId);

        if (existingOrder == null)
        {
            throw new ArgumentException($"Order with this ID {updateOrderDto.OrderId} not found.");
        }

        existingOrder.CustomerName = updateOrderDto.CustomerName;
        existingOrder.MailAddress = updateOrderDto.MailAddress;
        existingOrder.PhoneNumber = updateOrderDto.PhoneNumber;

        existingOrder.PickUpLocation = new Location
        {
            StreetAddress = updateOrderDto.PickUpLocation.StreetAddress,
            City = updateOrderDto.PickUpLocation.City,
            PostalCode = updateOrderDto.PickUpLocation.PostalCode,
            Country = updateOrderDto.PickUpLocation.Country,
            GpsCoordinates = new Coordinates(0.0, 0.0)
        };

        existingOrder.DestinationLocation = new Location
        {
            StreetAddress = updateOrderDto.DestinationLocation.StreetAddress,
            City = updateOrderDto.DestinationLocation.City,
            PostalCode = updateOrderDto.DestinationLocation.PostalCode,
            Country = updateOrderDto.DestinationLocation.Country,
            GpsCoordinates = new Coordinates(0.0, 0.0)
        };

        existingOrder.OrderItems.Clear();
        foreach (var itemDto in updateOrderDto.Items)
        {
            existingOrder.OrderItems.Add(new OrderItem
            {
                Description = itemDto.Description,
                WeightInKg = itemDto.WeightInKg,
                Quantity = itemDto.Quantity
            });
        }

        await _repository.UpdateAsync(existingOrder);
    }

    public async Task DeleteOrderAsync(Guid orderId)
    {
        await _repository.DeleteAsync(orderId);
    }
}
