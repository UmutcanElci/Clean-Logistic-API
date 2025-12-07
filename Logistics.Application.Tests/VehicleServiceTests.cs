namespace Logistics.Application.Tests;

using Moq;
using Xunit;
using Logistics.Application.Services;
using Logistics.Application.Interfaces;
using Logistics.Domain;
using Logistics.Domain.common;

public class VehicleServiceTests
{

    [Fact]
    public async Task GetVehicleByIdAsync_ShouldReturnVehicleDto_WhenVehicleExists()
    {
        var vehicleId = Guid.NewGuid();

        var fakeVehicle = new Vehicle
        {
            Id = vehicleId,
            HubId = Guid.NewGuid(),
            LicensePlate = "plate",
            Type = VehicleType.Truck,
            MaxWeightInKg = 300,
            MaxVolumeInCubicMeters = 20,
            CanGoAbroad = true,
            MaxSpeedInKph = 120,
            CurrentLocation = new Location
            {
                StreetAddress = "Street Address",
                City = "Some City",
                PostalCode = 245222,
                Country = "Test Country",
                GpsCoordinates = new Coordinates(10.0, 20.0)
            },
        };

        var mockRepo = new Mock<IVehicleRepository>();

        mockRepo.Setup(repo => repo.GetByIdAsync(fakeVehicle.Id)).ReturnsAsync(fakeVehicle);

        var mockhubRepo = new Mock<IHubRepository>();

        var vehicleService = new VehicleService(mockRepo.Object, mockhubRepo.Object);

        var result = await vehicleService.GetVehicleByIdAsync(fakeVehicle.Id);

        Assert.NotNull(result);
        Assert.IsType<Logistics.Application.DTOs.VehicleDto>(result);
        Assert.Equal(result.Id, fakeVehicle.Id);
        Assert.Equal(result.LicensePlate, fakeVehicle.LicensePlate);
    }

    [Fact]
    public async Task GetVehicleByIdAsync_ShouldReturnNull_WhenVehicleDoesNotExist()
    {
        var mockRepo = new Mock<IVehicleRepository>();
        var mockhubRepo = new Mock<IHubRepository>();

        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Vehicle?)null);

        var vehicleService = new VehicleService(mockRepo.Object, mockhubRepo.Object);

        var result = await vehicleService.GetVehicleByIdAsync(It.IsAny<Guid>());

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllVehiclesAsync_ShouldReturnListOfVehicleDtos_WhenVehiclesExist()
    {
        var vehicleId1 = Guid.NewGuid();
        var vehicleId2 = Guid.NewGuid();


        var fakeVehicle1 = new Vehicle
        {
            Id = vehicleId1,
            HubId = Guid.NewGuid(),
            LicensePlate = "plate",
            Type = VehicleType.Truck,
            MaxWeightInKg = 300,
            MaxVolumeInCubicMeters = 20,
            CanGoAbroad = true,
            MaxSpeedInKph = 120,
            CurrentLocation = new Location
            {
                StreetAddress = "Street Address",
                City = "Some City",
                PostalCode = 245222,
                Country = "Test Country",
                GpsCoordinates = new Coordinates(10.0, 20.0)
            },
        };


        var fakeVehicle2 = new Vehicle
        {
            Id = vehicleId2,
            HubId = Guid.NewGuid(),
            LicensePlate = "plate",
            Type = VehicleType.Truck,
            MaxWeightInKg = 300,
            MaxVolumeInCubicMeters = 20,
            CanGoAbroad = true,
            MaxSpeedInKph = 120,
            CurrentLocation = new Location
            {
                StreetAddress = "Street Address",
                City = "Some City",
                PostalCode = 245222,
                Country = "Test Country",
                GpsCoordinates = new Coordinates(10.0, 20.0)
            },
        };


        var vehicleList = new List<Vehicle> { fakeVehicle1, fakeVehicle2 };

        var mockRepo = new Mock<IVehicleRepository>();
        var mockhubRepo = new Mock<IHubRepository>();

        mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(vehicleList);

        var vehicleService = new VehicleService(mockRepo.Object, mockhubRepo.Object);

        var result = await vehicleService.GetAllVehiclesAsync();

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IReadOnlyList<Logistics.Application.DTOs.VehicleDto>>(result);
        Assert.Equal(vehicleList.Count, result.Count);

    }

    [Fact]
    public async Task GetAllVehiclesAsync_ShouldReturnEmptyListOfVehicleDtos_WhenNoVehiclesExist()
    {
        var vehicleList = new List<Vehicle>();

        var mockRepo = new Mock<IVehicleRepository>();
        var mockhubRepo = new Mock<IHubRepository>();

        mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(vehicleList);

        var vehicleService = new VehicleService(mockRepo.Object, mockhubRepo.Object);

        var result = await vehicleService.GetAllVehiclesAsync();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task CreateVehicleAsync_ShouldCallRepositoryCreate_AndReturnVehicleDto()
    {

    }

    [Fact]
    public async Task UpdateVehicleAsync_ShouldUpdateVehicle_WhenVehicleExists()
    {

    }

    [Fact]
    public async Task DeleteVehicleAsync_ShouldCallRepositoryDelete_WhenVehicleExists()
    {

    }
}
