namespace Logistics.Application.Services;

using Logistics.Application.Interfaces;
using Logistics.Domain;
using Logistics.Application.DTOs;
using Logistics.Domain.common;

public class RouteService
{
    private readonly IRouteRepository _routeRepository;
    private readonly IHubRepository _hubRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IOrderRepository _orderRepository;

    public RouteService(IRouteRepository routeRepository, IHubRepository hubRepository, IVehicleRepository vehicleRepository, IOrderRepository orderRepository)
    {
        _routeRepository = routeRepository;
        _hubRepository = hubRepository;
        _vehicleRepository = vehicleRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Route> CreateRouteAsync(CreateRouteDto createRouteDto)
    {
        var order = await _orderRepository.GetByIdAsync(createRouteDto.OrderId);

        var hub = await _hubRepository.GetHubAsync();

        var vehicle = await _vehicleRepository.GetAllAsync();

        var assignedVehicle = vehicle.First();

        var newRoute = new Route
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            Order = order,
            AssignVehicle = assignedVehicle,
            Status = RouteStatus.Planned,
            Stops = new List<Location>()
        };

        var createdRoute = await _routeRepository.CreateAsync(newRoute);

        return createdRoute;
    }

    public async Task<Route?> GetRouteByIdAsync(Guid id)
    {
        return await _routeRepository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<Route>> GetAllRoutesAsync()
    {
        return await _routeRepository.GetAllAsync();
    }

    public async Task UpdateRouteAsync(UpdateRouteDto routeDto)
    {
        var existingRoute = await _routeRepository.GetByIdAsync(routeDto.Id);
        existingRoute.Status = routeDto.Status;
        var newAssignedVehicle = await _vehicleRepository.GetByIdAsync(routeDto.AssignedVehicleId);
        existingRoute.AssignVehicle = newAssignedVehicle;
        existingRoute.Stops.Clear();

        foreach (var stopDto in routeDto.Stops)
        {
            existingRoute.Stops.Add(new Location
            {
                StreetAddress = stopDto.StreetAddress,
                City = stopDto.City,
                PostalCode = stopDto.PostalCode,
                Country = stopDto.Country,
                GpsCoordinates = new Coordinates(0.0, 0.0)
            });
        }
        await _routeRepository.UpdateAsync(existingRoute);
    }

    public async Task DeleteRouteAsync(Guid id)
    {
        await _routeRepository.DeleteAsync(id);
    }
}
