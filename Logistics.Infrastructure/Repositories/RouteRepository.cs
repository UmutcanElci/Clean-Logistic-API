namespace Logistics.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logistics.Application.Interfaces;
using Logistics.Domain;
using Microsoft.EntityFrameworkCore;

public class RouteRepository : IRouteRepository
{
    private readonly LogisticsDbContext _context;

    public RouteRepository(LogisticsDbContext context)
    {
        _context = context;
    }

    public async Task<Route> CreateAsync(Route route)
    {
        await _context.AddAsync(route);
        await _context.SaveChangesAsync();
        return route;
    }

    public async Task DeleteAsync(Guid routeId)
    {
        var deletedRoute = await GetByIdAsync(routeId);

        if (deletedRoute != null)
        {
            _context.Remove(deletedRoute);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IReadOnlyList<Route>> GetAllAsync()
    {
        return await _context.Routes
          .Include(v => v.AssignVehicle)
          .ThenInclude(v => v.CurrentLocation)
          .Include(v => v.Stops)
          .ThenInclude(v => v.GpsCoordinates)
          .ToListAsync();
    }

    public async Task<Route?> GetByIdAsync(Guid routeId)
    {
        return await _context.Routes
          .Include(v => v.AssignVehicle)
          .ThenInclude(v => v.CurrentLocation)
          .Include(v => v.Stops)
          .ThenInclude(v => v.GpsCoordinates)
          .FirstOrDefaultAsync(o => o.Id == routeId);

    }

    public async Task UpdateAsync(Route route)
    {
        _context.Update(route);
        await _context.SaveChangesAsync();
    }
}
