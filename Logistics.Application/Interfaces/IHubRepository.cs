namespace Logistics.Application.Interfaces;

using Logistics.Domain;

public interface IHubRepository
{
    Task<Hub?> GetHubAsync();
}
