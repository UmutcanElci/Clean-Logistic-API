namespace Logistics.Application.Interfaces;

using Logistics.Domain;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order);
    Task<Order?> GetByIdAsync(Guid orderId);
    Task<IReadOnlyList<Order>> GetAllAsync();
    Task UpdateAsync(Order order);
    Task DeleteAsync(Guid orderId);
}
