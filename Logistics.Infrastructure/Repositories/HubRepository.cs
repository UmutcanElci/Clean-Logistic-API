namespace Logistics.Infrastructure.Repositories;

using System.Threading.Tasks;
using Logistics.Application.Interfaces;
using Logistics.Domain;
using Microsoft.EntityFrameworkCore;

public class HubRepository : IHubRepository
{
    private readonly LogisticsDbContext _context;

    public HubRepository(LogisticsDbContext context)
    {
        _context = context;
    }

    public async Task<Hub?> GetHubAsync()
    {
        return await _context.Hub.FirstOrDefaultAsync();
    }
}
