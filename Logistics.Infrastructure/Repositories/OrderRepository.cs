using Microsoft.EntityFrameworkCore;
using Logistics.Application.Interfaces;
using Logistics.Domain;
namespace Logistics.Infrastructure.Repositories;


public class OrderRepository : IOrderRepository
{
    private readonly LogisticsDbContext _context;
    public OrderRepository(LogisticsDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateAsync(Order order)
    {
        await _context.AddAsync(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task DeleteAsync(Guid orderId)
    {
        var orderToDelete = await GetByIdAsync(orderId);
        _context.Orders.Remove(orderToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Order>> GetAllAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid orderId)
    {
        return await _context.Orders
          .Include(o => o.OrderItems)
          .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Update(order);
        await _context.SaveChangesAsync();
    }
}
