namespace Logistics.Application.Tests;

using Moq;
using Xunit;
using Logistics.Application.Services;
using Logistics.Application.Interfaces;
using Logistics.Domain;
using Logistics.Domain.common;
using Logistics.Application.DTOs;

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

        var fakeVehicle = new CreateVehicleDto
        {
            LicensePlate = "plate",
            Type = VehicleType.Truck,
            MaxWeightInKg = 300,
            MaxVolumeInCubicMeters = 20,
            CanGoAbroad = true,
            MaxSpeedInKph = 120,

        };

        var mockRepo = new Mock<IVehicleRepository>();
        var mockHubRepo = new Mock<IHubRepository>();


        mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Vehicle>())).ReturnsAsync((Vehicle vehicle) => vehicle);
        var fakeHub = new Hub { Id = Guid.NewGuid() };
        mockHubRepo.Setup(repo => repo.GetHubAsync()).ReturnsAsync(fakeHub);

        var vehicleService = new VehicleService(mockRepo.Object, mockHubRepo.Object);

        var result = await vehicleService.CreateVehicleAsync(fakeVehicle);

        Assert.NotNull(result);
        Assert.IsType<VehicleDto>(result);
        Assert.Equal(fakeVehicle.LicensePlate, result.LicensePlate);
        mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Vehicle>()), Times.Once());
    }

    [Fact]
    public async Task UpdateVehicleAsync_ShouldUpdateVehicle_WhenVehicleExists()
    {
        var vehicleId1 = Guid.NewGuid();

        var fakeVehicle1 = new UpdateVehicleDto
        {
            Id = vehicleId1,
            LicensePlate = "plate",
            Type = VehicleType.Truck,
            MaxWeightInKg = 300,
            MaxVolumeInCubicMeters = 20,
            CanGoAbroad = true,
            MaxSpeedInKph = 120,
            CurrentLocation = new LocationDto
            {
                StreetAddress = "Street Address",
                City = "Some City",
                PostalCode = 245222,
                Country = "Test Country",
            },
        };


        var fakeVehicle2 = new Vehicle
        {
            Id = vehicleId1,
            HubId = Guid.NewGuid(),
            LicensePlate = "plate111",
            Type = VehicleType.Truck,
            MaxWeightInKg = 444,
            MaxVolumeInCubicMeters = 20,
            CanGoAbroad = true,
            MaxSpeedInKph = 165,
            CurrentLocation = new Location
            {
                StreetAddress = "Street Address",
                City = "Some City",
                PostalCode = 245222,
                Country = "Test Country",
                GpsCoordinates = new Coordinates(14.0, 24.0)
            },
        };

        var mockRepo = new Mock<IVehicleRepository>();
        var mockhubRepo = new Mock<IHubRepository>();


        mockRepo.Setup(repo => repo.GetByIdAsync(fakeVehicle1.Id)).ReturnsAsync(fakeVehicle2);
        mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Vehicle>())).Returns(Task.CompletedTask);
        var vehicleService = new VehicleService(mockRepo.Object, mockhubRepo.Object);

        await vehicleService.UpdateVehicleAsync(fakeVehicle1);

        mockRepo.Verify(repo => repo.GetByIdAsync(fakeVehicle1.Id), Times.Once());
        mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Vehicle>()), Times.Once());

        Assert.Equal(fakeVehicle1.LicensePlate, fakeVehicle2.LicensePlate);
        Assert.Equal(fakeVehicle1.MaxSpeedInKph, fakeVehicle2.MaxSpeedInKph);

    }

    [Fact]
    public async Task DeleteVehicleAsync_ShouldCallRepositoryDelete_WhenVehicleExists()
    {
        var vehicleId = Guid.NewGuid();

        var mockRepo = new Mock<IVehicleRepository>();
        var mockhubRepo = new Mock<IHubRepository>();

        mockRepo.Setup(repo => repo.DeleteAsync(vehicleId)).Returns(Task.CompletedTask);


        var vehicleService = new VehicleService(mockRepo.Object, mockhubRepo.Object);

        await vehicleService.DeleteVehicleAsync(vehicleId);

        mockRepo.Verify(repo => repo.DeleteAsync(vehicleId), Times.Once());
    }
}
