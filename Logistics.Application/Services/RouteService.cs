namespace Logistics.Application.Services;

using Logistics.Application.Interfaces;
using Logistics.Domain;

public class RouteService
{
    private readonly IRouteRepository _routeRepository;

    public RouteService(IRouteRepository routeRepository)
    {
        _routeRepository = routeRepository;
    }

    public async Task<Route?> GetRouteByIdAsync(Guid id)
    {
        return await _routeRepository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<Route>> GetAllRoutesAsync()
    {
        return await _routeRepository.GetAllAsync();
    }


    public async Task DeleteRouteAsync(Guid id)
    {
        await _routeRepository.DeleteAsync(id);
    }

}
