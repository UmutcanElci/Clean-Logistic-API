namespace Logistics.Application.Interfaces;

using Logistics.Domain;

public interface IWarehouseRepository
{
    Task<Warehouse> CreateAsync(Warehouse warehouse);
    Task<Warehouse?> GetByIdAsync(Guid warehouseId);
    Task<IReadOnlyList<Warehouse>> GetAllAsync();
    Task UpdateAsync(Warehouse warehouse);
    Task DeleteAsync(Guid warehouseId);
}
