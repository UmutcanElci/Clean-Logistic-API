namespace Logistics.Application.Services;

using Logistics.Application.Interfaces;
using Logistics.Domain;

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

    public async Task<Route> GenerateRouteForOrderAsync(Guid orderId)
    {
        throw new Exception();
    }
}
