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

    }

    [Fact]
    public async Task GetAllVehiclesAsync_ShouldReturnListOfVehicleDtos_WhenVehiclesExist()
    {

    }

    [Fact]
    public async Task GetAllVehiclesAsync_ShouldReturnEmptyListOfVehicleDtos_WhenNoVehiclesExist()
    {

    }
}
