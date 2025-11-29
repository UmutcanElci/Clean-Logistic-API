namespace Logistics.Application.Services;

using Logistics.Application.Interfaces;
using Logistics.Domain;
using Logistics.Application.DTOs;

public class RouteService
{
    private readonly IRouteRepository _repository;

    public RouteService(IRouteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Route> CreateRouteAsync(CreateRouteDto createRouteDto)
    {
        throw new Exception();
    }

    public async Task<Route?> GetRouteByIdAsync(Guid id)
    {
        throw new Exception();
    }

    public async Task<IReadOnlyList<Route>> GetAllRoutesAsync()
    {
        throw new Exception();
    }

    public async Task UpdateRouteAsync(UpdateRouteDto routeDto)
    {
        throw new Exception();
    }

    public async Task DeleteRouteAsync(Guid id)
    {
        throw new Exception();
    }
}
