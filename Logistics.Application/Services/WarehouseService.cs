namespace Logistics.Application.Services;

using Logistics.Application.Interfaces;
using Logistics.Application.DTOs;
using Logistics.Domain.common;
using Logistics.Domain;

public class WarehouseService
{
    private readonly IWarehouseRepository _repository;

    public WarehouseService(IWarehouseRepository repository)
    {
        _repository = repository;
    }

    public async Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto warehouseDto)
    {
        var createWarehouse = new Warehouse
        {
            Id = Guid.NewGuid(),
            Name = warehouseDto.Name,
            MaxCapacity = warehouseDto.MaxCapacity,
            Type = warehouseDto.Type,
            Address = new Location
            {
                StreetAddress = warehouseDto.Address.StreetAddress,
                City = warehouseDto.Address.City,
                PostalCode = warehouseDto.Address.PostalCode,
                Country = warehouseDto.Address.Country,
                GpsCoordinates = new Coordinates(0.0, 0.0)
            }
        };

        var createdWarehouse = await _repository.CreateAsync(createWarehouse);

        return MapToWarehouseDto(createdWarehouse);
    }

    public async Task<WarehouseDto?> GetWarehouseByIdAsync(Guid id)
    {
        var warehouse = await _repository.GetByIdAsync(id);
        if (warehouse == null)
        {
            return null;
        }
        return MapToWarehouseDto(warehouse);
    }

    public async Task<IReadOnlyList<WarehouseDto>> GetAllWarehousesAsync()
    {
        var warehouses = await _repository.GetAllAsync();

        var warehousesDto = warehouses.Select(warehouse => MapToWarehouseDto(warehouse)).ToList();

        return warehousesDto;
    }

    public async Task UpdateWarehouseAsync(UpdateWarehouseDto warehouseDto)
    {
        var existingWarehouse = await _repository.GetByIdAsync(warehouseDto.Id);

        if (existingWarehouse == null)
        {
            throw new Exception();
        }
        existingWarehouse.Name = warehouseDto.Name;
        existingWarehouse.MaxCapacity = warehouseDto.MaxCapacity;
        existingWarehouse.Type = warehouseDto.Type;
        existingWarehouse.Address = new Location
        {
            StreetAddress = warehouseDto.Address.StreetAddress,
            City = warehouseDto.Address.City,
            PostalCode = warehouseDto.Address.PostalCode,
            Country = warehouseDto.Address.Country,
            GpsCoordinates = new Coordinates(0.0, 0.0)
        };

        await _repository.UpdateAsync(existingWarehouse);
    }

    public async Task DeleteWarehouseAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    private WarehouseDto MapToWarehouseDto(Warehouse warehouse)
    {
        return new WarehouseDto
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            MaxCapacity = warehouse.MaxCapacity,
            Type = warehouse.Type,
            Address = new LocationDto
            {
                StreetAddress = warehouse.Address.StreetAddress,
                City = warehouse.Address.City,
                PostalCode = warehouse.Address.PostalCode,
                Country = warehouse.Address.Country,
            }
        };
    }
}
