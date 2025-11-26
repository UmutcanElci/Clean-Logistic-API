namespace Logistics.Application.Services;

using Logistics.Application.Interfaces;
using Logistics.Application.DTOs;
using Logistics.Domain;
using Logistics.Domain.common;

public class VehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IHubRepository _hubRepository;
    public VehicleService(IVehicleRepository vehicleRepository, IHubRepository hubRepository)
    {
        _vehicleRepository = vehicleRepository;
        _hubRepository = hubRepository;
    }

    public async Task<Vehicle> CreateVehicleAsync(CreateVehicleDto vehicleDto)
    {
        var hub = await _hubRepository.GetHubAsync();


        if (hub == null)
        {
            throw new InvalidOperationException("No Hub found in the system to assign the cehicle to. Please create a hub first!");
        }

        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            LicensePlate = vehicleDto.LicensePlate,
            Hub = hub,
            Type = vehicleDto.Type,
            MaxWeightInKg = vehicleDto.MaxWeightInKg,
            MaxVolumeInCubicMeters = vehicleDto.MaxVolumeInCubicMeters,
            CanGoAbroad = vehicleDto.CanGoAbroad,
            MaxSpeedInKph = vehicleDto.MaxSpeedInKph,
            CurrentLocation = new Location
            {
                StreetAddress = "Default StreetAddress",
                City = "Default City",
                PostalCode = 00000,
                Country = "Country",
                GpsCoordinates = new Coordinates(0.0, 0.0)
            },
        };

        var createdVehicle = await _vehicleRepository.CreateAsync(vehicle);
        return createdVehicle;

    }

    public async Task<Vehicle?> GetVehicleByIdAsync(Guid id)
    {
        return await _vehicleRepository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<Vehicle>> GetAllVehiclesAsync()
    {
        return await _vehicleRepository.GetAllAsync();
    }

    public async Task DeleteVehicleAsync(Guid id)
    {
        await _vehicleRepository.DeleteAsync(id);
    }

    public async Task UpdateVehicleAsync(UpdateVehicleDto vehicleDto)
    {
        var existingVehicle = await _vehicleRepository.GetByIdAsync(vehicleDto.Id);

        if (existingVehicle == null)
        {
            throw new ArgumentException($"Vehicle with this ID {vehicleDto.Id} not found.");
        }
        existingVehicle.LicensePlate = vehicleDto.LicensePlate;
        existingVehicle.Type = vehicleDto.Type;
        existingVehicle.MaxWeightInKg = vehicleDto.MaxWeightInKg;
        existingVehicle.MaxVolumeInCubicMeters = vehicleDto.MaxVolumeInCubicMeters;
        existingVehicle.CanGoAbroad = vehicleDto.CanGoAbroad;
        existingVehicle.MaxSpeedInKph = vehicleDto.MaxSpeedInKph;
        existingVehicle.CurrentLocation = new Location
        {
            StreetAddress = vehicleDto.CurrentLocation.StreetAddress,
            City = vehicleDto.CurrentLocation.City,
            PostalCode = vehicleDto.CurrentLocation.PostalCode,
            Country = vehicleDto.CurrentLocation.Country,
            GpsCoordinates = new Coordinates(0.0, 0.0)
        };


        await _vehicleRepository.UpdateAsync(existingVehicle);
    }
}
