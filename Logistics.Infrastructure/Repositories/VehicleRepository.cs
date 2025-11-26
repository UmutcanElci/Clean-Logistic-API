namespace Logistics.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logistics.Application.Interfaces;
using Logistics.Domain;
using Microsoft.EntityFrameworkCore;

public class VehicleRepository : IVehicleRepository
{
    private readonly LogisticsDbContext _context;

    public VehicleRepository(LogisticsDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle> CreateAsync(Vehicle vehicle)
    {
        await _context.AddAsync(vehicle);
        await _context.SaveChangesAsync();

        return vehicle;
    }

    public async Task DeleteAsync(Guid vehicleId)
    {
        var vehicleToDelete = await GetByIdAsync(vehicleId);
        if (vehicleToDelete != null)
        {
            _context.Vehicles.Remove(vehicleToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IReadOnlyList<Vehicle>> GetAllAsync()
    {
        return await _context.Vehicles
          .Include(v => v.CurrentLocation)
          .ToListAsync();
    }

    public async Task<Vehicle?> GetByIdAsync(Guid vehicleId)
    {
        return await _context.Vehicles
          .Include(v => v.CurrentLocation)
          .FirstOrDefaultAsync(o => o.Id == vehicleId);
    }

    public async Task UpdateAsync(Vehicle vehicle)
    {
        _context.Update(vehicle);
        await _context.SaveChangesAsync();
    }
}
