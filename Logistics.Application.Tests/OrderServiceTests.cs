using Logistics.Application.DTOs;
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

    [Fact]
    public async Task GetOrderByIdAsync_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        var mockRepo = new Mock<IOrderRepository>();

        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Order?)null);

        var orderService = new OrderService(mockRepo.Object);

        var result = await orderService.GetOrderByIdAsync(It.IsAny<Guid>());

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllOrderAsync_ShouldReturnListOfOrdersDtos_WhenOrdersExist()
    {
        var orderId1 = Guid.NewGuid();

        var fakeOrder1 = new Order
        {
            Id = orderId1,
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

        var orderId2 = Guid.NewGuid();

        var fakeOrder2 = new Order
        {
            Id = orderId2,
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

        var orderId3 = Guid.NewGuid();

        var fakeOrder3 = new Order
        {
            Id = orderId3,
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

        var fakeOrderList = new List<Order> { fakeOrder1, fakeOrder2, fakeOrder3 };

        var mockRepo = new Mock<IOrderRepository>();

        mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeOrderList);

        var orderService = new OrderService(mockRepo.Object);

        var result = await orderService.GetAllOrderAsync();

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IReadOnlyList<Logistics.Application.DTOs.OrderDto>>(result);
        Assert.Equal(fakeOrderList.Count, result.Count);
    }

    [Fact]
    public async Task GetAllOrdersAsync_ShouldReturnEmptyListOfOrderDtos_WhenNoOrderExist()
    {
        var list = new List<Order>();

        var mockRepo = new Mock<IOrderRepository>();

        mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(list);

        var orderService = new OrderService(mockRepo.Object);

        var result = await orderService.GetAllOrderAsync();

        Assert.NotNull(result);
        Assert.Empty(result);

    }

    [Fact]
    public async Task CreateOrderAsync_ShouldCallRepositoryCreate_AndReturnOrderDto()
    {

        var createOrderDto = new CreateOrderDto
        {
            CustomerName = "Test Customer",
            MailAddress = "test@example.com",
            PhoneNumber = "555-232134",
            PickUpLocation = new LocationDto
            {
                StreetAddress = "123 test streert",
                City = "Test City",
                PostalCode = 11111,
                Country = "Test Country",
            },
            DestinationLocation = new LocationDto
            {
                StreetAddress = "123 test streert",
                City = "Test City",
                PostalCode = 11111,
                Country = "Test Country",
            },
            Items = new List<CreateOrderItemDto>()
        };

        var mockRepo = new Mock<IOrderRepository>();

        mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Order>())).ReturnsAsync((Order order) => order);

        var orderService = new OrderService(mockRepo.Object);

        var result = await orderService.CreateOrderAsync(createOrderDto);

        Assert.NotNull(result);
        Assert.IsType<OrderDto>(result);
        Assert.Equal(createOrderDto.CustomerName, result.CustomerName);
        mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Order>()), Times.Once());
    }

    [Fact]
    public async Task UpdateOrderAsync_ShouldUpdateOrder_WhenOrderExists()
    {
        var orderId = Guid.NewGuid();

        var updateOrderDto = new UpdateOrderDto
        {
            OrderId = orderId,
            CustomerName = "Test Customer",
            MailAddress = "test@example.com",
            PhoneNumber = "555-232134",
            PickUpLocation = new LocationDto
            {
                StreetAddress = "123 test streert",
                City = "Test City",
                PostalCode = 11111,
                Country = "Test Country",
            },
            DestinationLocation = new LocationDto
            {
                StreetAddress = "123 test streert",
                City = "Test City",
                PostalCode = 11111,
                Country = "Test Country",
            },
            Items = new List<CreateOrderItemDto>()
        };

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

        mockRepo.Setup(repo => repo.GetByIdAsync(updateOrderDto.OrderId)).ReturnsAsync(fakeOrder);
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);

        var orderService = new OrderService(mockRepo.Object);

        await orderService.UpdateOrderAsync(updateOrderDto);

        mockRepo.Verify(repo => repo.GetByIdAsync(updateOrderDto.OrderId), Times.Once());
        mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Order>()), Times.Once());

        Assert.Equal(updateOrderDto.CustomerName, fakeOrder.CustomerName);
        Assert.Equal(updateOrderDto.MailAddress, fakeOrder.MailAddress);
        Assert.Equal(updateOrderDto.Items.Count, fakeOrder.OrderItems.Count);
    }// wierd


    [Fact]
    public async Task DeleteOrderAsync_ShouldCallRepositoryDelete_WhenOrderExists()
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

        mockRepo.Setup(repo => repo.DeleteAsync(orderId)).Returns(Task.CompletedTask);

        var orderService = new OrderService(mockRepo.Object);

        await orderService.DeleteOrderAsync(orderId);

        mockRepo.Verify(repo => repo.DeleteAsync(orderId), Times.Once());
    }
}
