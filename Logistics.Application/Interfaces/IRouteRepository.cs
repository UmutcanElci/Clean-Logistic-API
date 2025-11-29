namespace Logistics.Application.Interfaces;

using Logistics.Domain;

public interface IRouteRepository
{
    Task<Route> CreateAsync(Route route);
    Task<Route?> GetByIdAsync(Guid routeId);
    Task<IReadOnlyList<Route>> GetAllAsync();
    Task DeleteAsync(Guid routeId);
    Task UpdateAsync(Route route);
}
