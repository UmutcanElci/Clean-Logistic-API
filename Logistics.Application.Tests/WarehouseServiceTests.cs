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
    public async Task GetWarehouseByIdAsync_ShouldReturnWarehouseDto_WhenWarehouseExists()
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

    [Fact]
    public async Task GetWarehouseByIdAsync_ShouldReturnNull_WhenWarehouseDoesNotExist()
    {
        var mockRepo = new Mock<IWarehouseRepository>();

        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Warehouse?)null);

        var warehouseService = new WarehouseService(mockRepo.Object);

        var result = await warehouseService.GetWarehouseByIdAsync(It.IsAny<Guid>());

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllWarehousesAsync_ShouldReturnListOfWarehouseDtos_WhenWarehousesExist()
    {

        var warehouseId1 = Guid.NewGuid();

        var warehouse1 = new Warehouse
        {
            Id = warehouseId1,
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

        var warehouseId2 = Guid.NewGuid();

        var warehouse2 = new Warehouse
        {
            Id = warehouseId2,
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

        var warehousesList = new List<Warehouse> { warehouse1, warehouse2 };


        var mockRepo = new Mock<IWarehouseRepository>();

        mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(warehousesList);

        var warehouseService = new WarehouseService(mockRepo.Object);

        var result = await warehouseService.GetAllWarehousesAsync();

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IReadOnlyList<Logistics.Application.DTOs.WarehouseDto>>(result);
        Assert.Equal(warehousesList.Count, result.Count);
    }

    [Fact]
    public async Task GetAllWarehousesAsync_ShouldReturnEmptyListOfWarehouseDtos_WhenNoWarehouseExist()
    {
        var warehousesList = new List<Warehouse>();


        var mockRepo = new Mock<IWarehouseRepository>();

        mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(warehousesList);

        var warehouseService = new WarehouseService(mockRepo.Object);

        var result = await warehouseService.GetAllWarehousesAsync();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task CreateWarehouseAsync_ShouldCallRepositoryCreate_AndReturnWarehouseDto()
    {
        var warehouseDto = new CreateWarehouseDto
        {
            Name = "Random name",
            MaxCapacity = 223,
            Type = StorageType.Refrigerated,
            Address = new LocationDto
            {
                StreetAddress = "StreetAddress",
                Country = " Some Country",
                PostalCode = 213421,
                City = "ACity",
            },
        };

        var mockRepo = new Mock<IWarehouseRepository>();

        mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Warehouse>())).ReturnsAsync((Warehouse warehouse) => warehouse);

        var warehouseService = new WarehouseService(mockRepo.Object);

        var result = await warehouseService.CreateWarehouseAsync(warehouseDto);

        Assert.NotNull(result);
        Assert.IsType<WarehouseDto>(result);
        Assert.Equal(warehouseDto.Name, result.Name);
        mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Warehouse>()), Times.Once());
    }

    [Fact]
    public async Task UpdateWarehouseAsync_ShouldUpdateWarehouse_WhenWarehouseExist()
    {
        var updateWarehouseDto = new UpdateWarehouseDto
        {
            Id = Guid.NewGuid(),
            Name = "RT",
            MaxCapacity = 545,
            Type = StorageType.Materials,
            Address = new LocationDto
            {
                StreetAddress = "Ahh",
                Country = "Cont",
                PostalCode = 21413,
                City = "CCotry"
            },
        };


        var warehouse = new Warehouse
        {
            Id = Guid.NewGuid(),
            Name = "RT",
            MaxCapacity = 545,
            Type = StorageType.Materials,
            Address = new Location
            {
                StreetAddress = "Ahh",
                Country = "Cont",
                PostalCode = 21413,
                City = "CCotry",
                GpsCoordinates = new Coordinates(0.0, 0.0)
            },
        };

        var mockRepo = new Mock<IWarehouseRepository>();

        mockRepo.Setup(repo => repo.GetByIdAsync(updateWarehouseDto.Id)).ReturnsAsync(warehouse);
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Warehouse>())).Returns(Task.CompletedTask);
        var warehouseService = new WarehouseService(mockRepo.Object);

        await warehouseService.UpdateWarehouseAsync(updateWarehouseDto);

        mockRepo.Verify(repo => repo.GetByIdAsync(updateWarehouseDto.Id), Times.Once());
        mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Warehouse>()), Times.Once());

        Assert.Equal(updateWarehouseDto.Name, warehouse.Name);
        Assert.Equal(updateWarehouseDto.MaxCapacity, warehouse.MaxCapacity);
        Assert.Equal(updateWarehouseDto.Type, warehouse.Type);
        Assert.Equal(updateWarehouseDto.Address.StreetAddress, warehouse.Address.StreetAddress);
        Assert.Equal(updateWarehouseDto.Address.Country, warehouse.Address.Country);

    }

    [Fact]
    public async Task DeleteWarehouseAsync_ShouldCallRepositoryDelete_WhenWarehouseExists()
    {
        var warehouseId = Guid.NewGuid();

        var mockRepo = new Mock<IWarehouseRepository>();

        mockRepo.Setup(repo => repo.DeleteAsync(warehouseId)).Returns(Task.CompletedTask);


        var vehicleService = new WarehouseService(mockRepo.Object);

        await vehicleService.DeleteWarehouseAsync(warehouseId);

        mockRepo.Verify(repo => repo.DeleteAsync(warehouseId), Times.Once());


    }
}
