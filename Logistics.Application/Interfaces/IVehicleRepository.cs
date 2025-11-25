using Logistics.Domain;

namespace Logistics.Application.Interfaces;

public interface IVehicleRepository
{
    Task<Vehicle> CreateAsync(Vehicle vehicle);
    Task DeleteAsync(Guid vehicleId);
    Task UpdateAsync(Vehicle vehicle);
    Task<Vehicle?> GetByIdAsync(Guid vehicleId);
    Task<IReadOnlyList<Vehicle>> GetAllAsync();
}
