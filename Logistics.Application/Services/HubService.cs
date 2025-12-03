namespace Logistics.Application.Services;

using Logistics.Application.Interfaces;
using Logistics.Application.DTOs;
using Logistics.Application.Mappers;

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
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
        {
            throw new ArgumentException($"Order with ID {orderId} mor found.");
        }

        var hub = await _hubRepository.GetHubAsync();
        if (hub == null)
        {
            throw new InvalidOperationException("No Hub found in the system to create a route.");
        }

        var vehicles = await _vehicleRepository.GetAllAsync();
        if (vehicles == null)
        {
            throw new InvalidOperationException("No available vehicles found to assign to the route.");
        }

        var assignVehicle = vehicles.First();

        var newRoute = hub.CreateRouteForOrder(order, assignVehicle);

        var createdRoute = await _routeRepository.CreateAsync(newRoute);

        return createdRoute.ToDto();
    }

}
