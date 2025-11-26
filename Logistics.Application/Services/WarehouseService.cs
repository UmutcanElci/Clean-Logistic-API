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

    public async Task<Warehouse> CreateWarehouseAsync(CreateWarehouseDto warehouseDto)
    {
        throw new ArgumentException();
    }

    public async Task<Warehouse?> GetWarehousebyIdAsync(Guid id)
    {
        throw new ArgumentException();
    }

    public async Task<IReadOnlyList<Warehouse>> GetAllWarehousesAsync()
    {
        throw new ArgumentException();
    }

    public async Task UpdateWarehouseAsync(UpdateWarehouseDto warehouseDto)
    {
        throw new ArgumentException();
    }

    public async Task DeleteWarehouseAsync(Guid id)
    {
        throw new ArgumentException();
    }
}
