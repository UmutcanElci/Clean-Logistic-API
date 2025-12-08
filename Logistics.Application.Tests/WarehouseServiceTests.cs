namespace Logistics.Application.Tests;

using Moq;
using Xunit;
using Logistics.Application.Interfaces;
using Logistics.Application.Services;
using Logistics.Domain;
using Logistics.Domain.common;
using Logistics.Application.DTOs;

public class WarehouseServiceTests
{

    [Fact]
    public async Task GetWarehouseByIdAsync_ShouldReturnWarehouseDto_WhenWarehousrExists()
    {
        var warehouseId = Guid.NewGuid();

        var warehouse = new Warehouse
        {
            Id = warehouseId,
            Name = "Random name",
            MaxCapacity = 600,
            Type = StorageType.Materials,
            Address = new Location
            {
                StreetAddress = "Random StreetAddress",
                City = "Cityyy",
                PostalCode = 33429,
                Country = "Country",
                GpsCoordinates = new Coordinates(12.0, 29.0),

            },
        };

        var mockRepo = new Mock<IWarehouseRepository>();

        mockRepo.Setup(repo => repo.GetByIdAsync(warehouseId)).ReturnsAsync(warehouse);

        var warehouseService = new WarehouseService(mockRepo.Object);

        var result = await warehouseService.GetWarehouseByIdAsync(warehouseId);

        Assert.NotNull(result);
        Assert.IsType<WarehouseDto>(result);
        Assert.Equal(warehouseId, result.Id);
    }
}
