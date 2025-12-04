namespace Logistics.Application.Services;

using Logistics.Application.Interfaces;
using Logistics.Application.DTOs;
using Logistics.Application.Mappers;


public class RouteService
{
    private readonly IRouteRepository _routeRepository;

    public RouteService(IRouteRepository routeRepository)
    {
        _routeRepository = routeRepository;
    }

    public async Task<RouteDto?> GetRouteByIdAsync(Guid id)
    {
        var route = await _routeRepository.GetByIdAsync(id);

        if (route == null)
        {
            return null;
        }

        return route.ToDto();
    }

    public async Task<IReadOnlyList<RouteDto>> GetAllRoutesAsync()
    {
        var routes = await _routeRepository.GetAllAsync();
        return routes.Select(route => route.ToDto()).ToList();

    }


    public async Task DeleteRouteAsync(Guid id)
    {
        await _routeRepository.DeleteAsync(id);
    }

}
