namespace Logistics.Application.Services;

using Logistics.Application.Interfaces;
using Logistics.Domain;
using Logistics.Application.DTOs;

public class HubService
{
    private readonly IHubRepository _hubRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRouteRepository _routeRepository;

    public HubService(IHubRepository hubRepository, IOrderRepository orderRepository, IVehicleRepository vehicleRepository, IRouteRepository routeRepository)
    {
        _hubRepository = hubRepository;
        _orderRepository = orderRepository;
        _vehicleRepository = vehicleRepository;
        _routeRepository = routeRepository;
    }

    public async Task<RouteDto> GenerateRouteForOrderAsync(Guid orderId)
    {
        throw new Exception();
    }

    private RouteDto MapToRouteDto(Route route)
    {
        return new RouteDto
        {
            Id = route.Id,
            AssignVehicle = route.AssignVehicle,
            Stops = route.Stops,
            Status = route.Status
        };
    }
}
