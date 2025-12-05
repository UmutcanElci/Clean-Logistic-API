using Logistics.Application.Interfaces;
using Logistics.Application.Services;
using Logistics.Domain;
using Logistics.Domain.common;
using Moq;

namespace Logistics.Application.Tests;

public class OrderServiceTests
{
    [Fact]
    public async Task GetOrderByIdAsync_ShouldReturnOrderDto_WhenOrderExists()
    {
        var orderId = Guid.NewGuid();

        var fakeOrder = new Order
        {
            Id = orderId,
            CustomerName = "Test Customer",
            MailAddress = "test@example.com",
            PhoneNumber = "555-232134",
            PickUpLocation = new Location
            {
                StreetAddress = "123 test streert",
                City = "Test City",
                PostalCode = 11111,
                Country = "Test Country",
                GpsCoordinates = new Coordinates(12.0, 2.0)
            },
            DestinationLocation = new Location
            {
                StreetAddress = "123 test streert",
                City = "Test City",
                PostalCode = 11111,
                Country = "Test Country",
                GpsCoordinates = new Coordinates(12.0, 2.0)
            },
            OrderItems = new List<OrderItem>()
        };

        var mockRepo = new Mock<IOrderRepository>();

        mockRepo.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(fakeOrder);

        var orderService = new OrderService(mockRepo.Object);

        var result = await orderService.GetOrderByIdAsync(orderId);

        Assert.NotNull(result);
        Assert.IsType<Logistics.Application.DTOs.OrderDto>(result);
        Assert.Equal(orderId, result.Id);
        Assert.Equal("Test Customer", result.CustomerName);
    }
}
