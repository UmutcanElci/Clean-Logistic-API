namespace Logistics.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logistics.Application.Interfaces;
using Logistics.Domain;
using Microsoft.EntityFrameworkCore;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly LogisticsDbContext _context;

    public WarehouseRepository(LogisticsDbContext context)
    {
        _context = context;
    }

    public async Task<Warehouse> CreateAsync(Warehouse warehouse)
    {
        await _context.AddAsync(warehouse);
        await _context.SaveChangesAsync();

        return warehouse;
    }

    public async Task DeleteAsync(Guid warehouseId)
    {
        var deletedWarehouse = await GetByIdAsync(warehouseId);
        if (deletedWarehouse != null)
        {
            _context.Remove(deletedWarehouse);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IReadOnlyList<Warehouse>> GetAllAsync()
    {
        return await _context.Warehouses
          .Include(v => v.Address)
          .ToListAsync();
    }

    public async Task<Warehouse?> GetByIdAsync(Guid warehouseId)
    {
        return await _context.Warehouses
          .Include(v => v.Address)
          .FirstOrDefaultAsync(o => o.Id == warehouseId);
    }

    public async Task UpdateAsync(Warehouse warehouse)
    {
        _context.Update(warehouse);
        await _context.SaveChangesAsync();
    }
}
