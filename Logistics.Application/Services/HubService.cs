namespace Logistics.Application.Services;

using Logistics.Application.Interfaces;
using Logistics.Application.DTOs;
using Logistics.Application.Mappers;
using Logistics.Domain.common;
using System.Linq;

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
            throw new ArgumentException($"Order with ID {orderId} not found.");
        }

        var hub = await _hubRepository.GetHubAsync();
        if (hub == null)
        {
            throw new InvalidOperationException("No Hub found in the system to create a route.");
        }

        var vehicles = await _vehicleRepository.GetAllAsync();
        if (vehicles == null || !vehicles.Any())
        {
            throw new InvalidOperationException("No vehicles found in the system.");
        }


        var newRoute = hub.CreateRouteForOrder(order, vehicles);

        var createdRoute = await _routeRepository.CreateAsync(newRoute);
        return createdRoute.ToDto();
    }
}
